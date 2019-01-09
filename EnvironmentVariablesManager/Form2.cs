using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace EnvironmentVariablesManager
{
    public partial class FormVariableEditor : Form
    {
        // 声明变量用于存储（传递给构造函数的）参数，使得在构造函数之外也能访问这些数据
        string formTitleData;
        string textBoxVarNameData;
        string textBoxVarValueData;
        FormMain parentFormObj;

        public FormVariableEditor(FormMain parentForm, string formTitle, string textBoxVarName, string textBoxVarValue)
        {

            InitializeComponent();

            this.Text = formTitle;
            this.textBox1.Text = textBoxVarName;
            this.textBox2.Text = textBoxVarValue;

            // 将通过构造函数的参数传递进来的数据保存在变量中，使得在构造函数之外也能访问这些数据
            formTitleData = formTitle;
            textBoxVarNameData = textBoxVarName;
            textBoxVarValueData = textBoxVarValue;
            parentFormObj = parentForm;

        }

        //
        // 删除（传入的字符串）中所有的换行符
        //
        private string formatString(string sourceString)
        {
            return Regex.Replace(sourceString, Environment.NewLine, string.Empty, RegexOptions.Multiline);
        }

        //
        // 变量编辑窗口被加载时执行此操作
        //
        private void FormVariableEditor_Load(object sender, EventArgs e)
        {
            // 根据不同的启动原因，赋予窗口不同的标题
            this.Text = formTitleData;
            // 根据不同的启动原因，赋予两个文本框不同的值
            this.textBox1.Text = textBoxVarNameData;
            this.textBox2.Text = textBoxVarValueData;

            if (this.textBox1.Text == "" || this.textBox2.Text == "")
            {
                this.buttonSaveChange.Enabled = false;
            }

        }

        //
        // 用户点击保存按钮时执行此操作,判断用户是如何启动Form2窗体的，根据不同的启动原因，执行不同的操作
        // 这里有一个让人疑惑的地方:
        // 新建（或编辑，编辑不确定）变量，如果变量名为空，点击保存按钮，会将值写入新建按钮所属listView的第一项
        // 尽管我设置了变量名和变量值同时不为空时保存按钮才可选，由此避免了这种现象的发生，但是我还是想知道原因
        private void buttonSaveChange_Click(object sender, EventArgs e)
        {

            ListView.SelectedIndexCollection sel;
            ListViewItem item;
            string valueKind;

            switch(GlobalData.whatCaseFormVarEditorStart)
            {

                case CaseFormVarEditorStart.editlistViewUserItem:

                    sel = parentFormObj.listViewUser.SelectedIndices;
                    // 点击保存按钮之前，先检测文本是否已经作出了更改

                    parentFormObj.listViewUser.Items[sel[0]].Text = this.textBox1.Text;
                    parentFormObj.listViewUser.Items[sel[0]].SubItems[1].Text = formatString(this.textBox2.Text);
                    // 判断变量值中是否存在%字符
                    // 根据判断结果决定listView中显示变量类型的值为REG_EXPAND_SZ还是REG_SZ
                    if (this.textBox2.Text.Contains('%'))
                    {
                        valueKind = "REG_EXPAND_SZ";
                        parentFormObj.listViewUser.Items[sel[0]].SubItems[2].Text = valueKind;
                    }
                    else
                    {
                        valueKind = "REG_SZ";
                        parentFormObj.listViewUser.Items[sel[0]].SubItems[2].Text = valueKind;
                    }
                    GlobalData.listViewUserChanged = true;
                    break;

                case CaseFormVarEditorStart.editlistViewSystemItem:

                    sel = parentFormObj.listViewSystem.SelectedIndices;
                    parentFormObj.listViewSystem.Items[sel[0]].Text = this.textBox1.Text;
                    parentFormObj.listViewSystem.Items[sel[0]].SubItems[1].Text = formatString(this.textBox2.Text);

                    // 判断变量值中是否存在%字符
                    // 根据判断结果决定listView中显示变量类型的值为REG_EXPAND_SZ还是REG_SZ
                    if (this.textBox2.Text.Contains('%'))
                    {
                        valueKind = "REG_EXPAND_SZ";
                        parentFormObj.listViewSystem.Items[sel[0]].SubItems[2].Text = valueKind;
                    }
                    else
                    {
                        valueKind = "REG_SZ";
                        parentFormObj.listViewSystem.Items[sel[0]].SubItems[2].Text = valueKind;
                    }

                    GlobalData.listViewSystemChanged = true;
                    break;

                case CaseFormVarEditorStart.addlistViewUserItem:

                    item = new ListViewItem(this.textBox1.Text);
                    item.SubItems.Add(formatString(this.textBox2.Text));

                    // 判断变量值中是否存在%字符
                    // 根据判断结果决定listView中显示变量类型的值为REG_EXPAND_SZ还是REG_SZ
                    if(this.textBox2.Text.Contains('%'))
                    {
                        valueKind = "REG_EXPAND_SZ";
                        item.SubItems.Add(valueKind);
                    }
                    else
                    {
                        valueKind = "REG_SZ";
                        item.SubItems.Add(valueKind);
                    }

                    // 新建listView子项时，检测listView中是否存在同名子项
                    // 如果存在，就将新的变量值和变量类型赋给已经存在的同名的子项，否则就添加新的子项
                    if (parentFormObj.listViewUser.FindItemWithText(item.Text) != null)
                    {
                        parentFormObj.listViewUser.FindItemWithText(item.Text).SubItems[1].Text = formatString(this.textBox2.Text);
                        parentFormObj.listViewUser.FindItemWithText(item.Text).SubItems[2].Text = valueKind;
                    }
                    else
                    {
                        parentFormObj.listViewUser.Items.Add(item);
                    }
                    GlobalData.listViewUserChanged = true;
                    break;

                case CaseFormVarEditorStart.addlistViewSystemItem:

                    item = new ListViewItem(this.textBox1.Text);
                    item.SubItems.Add(formatString(this.textBox2.Text));

                    // 判断变量值中是否存在%字符
                    // 根据判断结果决定listView中显示变量类型的值为REG_EXPAND_SZ还是REG_SZ
                    if (this.textBox2.Text.Contains('%'))
                    {
                        valueKind = "REG_EXPAND_SZ";
                        item.SubItems.Add(valueKind);
                    }
                    else
                    {
                        valueKind = "REG_SZ";
                        item.SubItems.Add(valueKind);
                    }

                    // 新建listView子项时，检测listView中是否存在同名子项
                    // 如果存在，就将新的变量值和变量类型赋给已经存在的同名的子项，否则就添加新的子项
                    if (parentFormObj.listViewSystem.FindItemWithText(item.Text) != null)
                    {
                        parentFormObj.listViewSystem.FindItemWithText(item.Text).SubItems[1].Text = formatString(this.textBox2.Text);
                        parentFormObj.listViewSystem.FindItemWithText(item.Text).SubItems[2].Text = valueKind;
                    }
                    else
                    {
                        parentFormObj.listViewSystem.Items.Add(item);
                    }

                    GlobalData.listViewSystemChanged = true;

                    break;

            }

            parentFormObj.btnSaveConfig.Enabled = true;
            parentFormObj.Enabled = true;
            this.Close();

        }

        //
        //用户点击取消按钮时执行此操作，检测变量contentChanged的值
        //如果为true，说明用户进行过输入，弹出对话框提示用户是否保存所作的修改
        //
        private void buttonCancelChange_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //
        // 变量编辑窗口关闭时执行此操作，检测变量sureToExit中储存的值，如果为false，则取消窗口关闭事件
        //
        private void FormVariableEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            parentFormObj.Enabled = true;
        }

        //
        // 当用户修改了变量名编辑框中的值时执行此操作
        //
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (this.textBox1.Text != "" && this.textBox2.Text != "")
            {
                this.buttonSaveChange.Enabled = true;
            }
            else
            {
                this.buttonSaveChange.Enabled = false;
            }
        }

        //
        // 当用户修改了变量值编辑框中的值时执行此操作
        //
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (this.textBox1.Text != "" && this.textBox2.Text != "")
            {
                this.buttonSaveChange.Enabled = true;
            }
            else
            {
                this.buttonSaveChange.Enabled = false;
            }
        }

        private void YourControl_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Alt) & (e.KeyCode == Keys.N))
            {
                this.textBox1.Focus();
            }
        }



    }
}
