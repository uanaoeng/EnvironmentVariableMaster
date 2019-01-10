using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Security.Permissions;
using System.Security;

// 这个命名空间下总共定义了1个类
namespace EnvironmentVariablesManager
{

    public partial class FormMain : Form
    {
        // 关于这个类的说明：
        // 这个类定义了程序的主界面
        // 这个类的另一部分在文件Form1.Designer.cs中定义

        RegistryKey rkSysEnvVar;
        RegistryKey rkUserEnvVar;
        string[] valueNames;
        ArrayList listViewItems;
        ListViewItem item;

//=============================================================================
# region 开始 - 构造函数
//=============================================================================
        public FormMain()
        {
            // 从注册表中读取用户环境变量和系统环境变量
            // 将从注册表中读取到的系统环境变量的信息，通过listViewSystem向用户展现出来
            // 将从注册表中读取到的当前用户的环境变量的信息，通过listViewUser向用户展现出来
            // 为 listView 添加事件，当用户双击子项目时，启动Form2窗口进行相关操作

            // 调用在另另一个文件中定义的方法，对窗口组件的属性进行设置
            InitializeComponent();

//------------------------------------------------------------------------------

            #region 开始 - 对groupBoxUser 组件的操作
            // 更改groupBoxUser的标题
            this.groupBoxUser.Text = Environment.UserName + " 的用户变量(&U)";
            #endregion 结束 - 对groupBoxUser 组件的操作

//------------------------------------------------------------------------------

            #region 开始 - 向 listViewUser 组件中写入数据

            this.listViewUser.MultiSelect = false;
            //this.listViewUser.GridLines = true;
            this.listViewUser.View = View.Details;
            this.listViewUser.FullRowSelect = true;
            this.listViewUser.Columns.Add("变量名", 150);
            this.listViewUser.Columns.Add("变量值", 150);
            this.listViewUser.Columns.Add("类型", 150);
            // 对listView的item按升序进行排序


            // 对Environment子键进行访问之前，检查这个子键是否存在，如果不存在，就创建它
            // 这样就避免了rkUserEnvVar.GetValueNames()的异常

            rkUserEnvVar = Registry.CurrentUser.OpenSubKey("Environment");
            if(rkUserEnvVar == null)
            {
                rkUserEnvVar = Registry.CurrentUser.CreateSubKey("Environment");
            }

            valueNames = rkUserEnvVar.GetValueNames();
            listViewItems = new ArrayList();

            foreach (string name in valueNames)
            {
                // 从注册表中读取值时，只读取存储类型为REG_SZ和REG_EXPAND_SZ的值。 
                string rkValueType = rkUserEnvVar.GetValue(name).GetType().FullName;
                if (rkValueType == "System.String" )
                {
                    item = new ListViewItem(name, 0);
                    item.Checked = true;

                    switch (rkUserEnvVar.GetValueKind(name))
                    {
                        case RegistryValueKind.ExpandString:
                            item.SubItems.Add(rkUserEnvVar.GetValue(name, "", RegistryValueOptions.DoNotExpandEnvironmentNames).ToString());
                            item.SubItems.Add("REG_EXPAND_SZ");
                            break;
                        case RegistryValueKind.String:
                            item.SubItems.Add(rkUserEnvVar.GetValue(name).ToString());
                            item.SubItems.Add("REG_SZ");
                            break;
                    }

                    listViewItems.Add(item);
                }

            }

            this.listViewUser.Items.AddRange((ListViewItem[])listViewItems.ToArray(typeof(ListViewItem)));
            this.listViewUser.Sorting = SortOrder.Ascending;            
            if(this.listViewUser.Items.Count != 0)
            {
                this.listViewUser.Items[0].Selected = true;
            }

            // 添加事件
            this.listViewUser.DoubleClick += new System.EventHandler(Edit_listViewUserItem);
            this.listViewUser.KeyDown += new KeyEventHandler(listViewUser_KeyDown);

            #endregion 结束 - 向 listViewUser 组件中写入数据


//------------------------------------------------------------------------------

            #region 开始 - 向 listViewSystem 组件中写入数据

            this.listViewSystem.MultiSelect = false;
            //this.listViewSystem.GridLines = true;
            this.listViewSystem.View = View.Details;
            this.listViewSystem.FullRowSelect = true;
            this.listViewSystem.Columns.Add("变量名", 150);
            this.listViewSystem.Columns.Add("变量值", 150);
            this.listViewSystem.Columns.Add("类型", 150);
            // 对listView的item按升序进行排序
            this.listViewSystem.Sorting = SortOrder.Ascending;

            // 对注册表子键进行访问之前先检测该子键是否存在，如果不存在，就创建它
            // 以此避免了rkSysEnvVar.GetValueNames()异常
            rkSysEnvVar = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Session Manager\Environment");
            if (rkSysEnvVar == null)
            {
                rkSysEnvVar = Registry.LocalMachine.CreateSubKey(@"SYSTEM\CurrentControlSet\Control\Session Manager\Environment");
            }

            valueNames = rkSysEnvVar.GetValueNames();
            listViewItems = new ArrayList();

            foreach (string name in rkSysEnvVar.GetValueNames())
            {
                // 从注册表中读取值时，只读取存储类型为REG_SZ和REG_EXPAND_SZ的值。             
                string rkValueType = rkSysEnvVar.GetValue(name).GetType().ToString();
                if(rkValueType == "System.String")
                {
                    item = new ListViewItem(name, 0);
                    item.Checked = true;

                    switch (rkSysEnvVar.GetValueKind(name))
                    {
                        case RegistryValueKind.ExpandString:
                            item.SubItems.Add(rkSysEnvVar.GetValue(name, "", RegistryValueOptions.DoNotExpandEnvironmentNames).ToString());
                            item.SubItems.Add("REG_EXPAND_SZ");
                            break;
                        case RegistryValueKind.String:
                            item.SubItems.Add(rkSysEnvVar.GetValue(name).ToString());
                            item.SubItems.Add("REG_SZ");
                            break;
                    }

                    listViewItems.Add(item);
                }
            }

            this.listViewSystem.Items.AddRange((ListViewItem[])listViewItems.ToArray(typeof(ListViewItem)));

            if(this.listViewSystem.Items.Count != 0)
            {
                this.listViewSystem.Items[0].Selected = true;
            }

            // 添加事件
            this.listViewSystem.DoubleClick += new System.EventHandler(Edit_listViewSystemItem);
            this.listViewSystem.KeyDown += new KeyEventHandler(listViewSystem_KeyDown);

            #endregion 结束 - 向 listViewSystem 组件中写入数据

//------------------------------------------------------------------------------

            #region 开始 - 给按钮添加事件

            this.btnUserEdit.Click += new System.EventHandler(Edit_listViewUserItem);
            this.btnSysEdit.Click += new System.EventHandler(Edit_listViewSystemItem);

            this.btnUserNew.Click += new System.EventHandler(New_listViewUserItem);
            this.btnSysNew.Click += new System.EventHandler(New_listViewSystemItem);

            this.btnUserDel.Click += new System.EventHandler(Del_listViewUserItem);
            this.btnSysDel.Click += new System.EventHandler(Del_listViewSystemItem);

            this.btnSaveConfig.Click += new System.EventHandler(btnSaveConfig_Click);

            #endregion 结束 - 给按钮添加事件

        }

#endregion 结束 - 构造函数



//=============================================================================
#region 开始 - 事件委托
//=============================================================================

        //
        // 对 listViewUser的子项进行编辑
        // 单击 btnUserEdit 或双击 listViewUserItem 时触发
        //        
        private void Edit_listViewUserItem(object sender, EventArgs e)
        {

            FormVariableEditor formVarEditor;
            string formTitle;
            string textBoxVarName;
            string textBoxVarValue;

            ListView.SelectedIndexCollection sel = this.listViewUser.SelectedIndices;
            if (sel.Count != 0)
            {
                formTitle = "编辑用户变量";
                textBoxVarName = this.listViewUser.Items[sel[0]].Text;
                textBoxVarValue = this.listViewUser.Items[sel[0]].SubItems[1].Text.Replace(";", ";\r\n\r\n");
                formVarEditor = new FormVariableEditor(this, formTitle, textBoxVarName, textBoxVarValue);
                formVarEditor.Show();
                // 当子窗口打开时，禁用本窗口
                this.Enabled = false;
                formVarEditor.textBox2.Focus();
                GlobalData.whatCaseFormVarEditorStart = CaseFormVarEditorStart.editlistViewUserItem;
            }

        }

//------------------------------------------------------------------------------

        //
        // 用户对 listViewSystem的子项进行编辑时执行此操作
        // 单击 btnSysEdit 或双击 listViewSystemItem 时触发
        //
        private void Edit_listViewSystemItem(object sender, EventArgs e)
        {
            FormVariableEditor formVarEditor;
            string formTitle;
            string textBoxVarName;
            string textBoxVarValue;
            ListView.SelectedIndexCollection sel = this.listViewSystem.SelectedIndices;
            if (sel.Count != 0)
            {
                formTitle = "编辑系统变量";
                textBoxVarName = this.listViewSystem.Items[sel[0]].Text;
                textBoxVarValue = this.listViewSystem.Items[sel[0]].SubItems[1].Text.Replace(";", ";\r\n\r\n");

                formVarEditor = new FormVariableEditor(this, formTitle, textBoxVarName, textBoxVarValue);
                formVarEditor.Show();
                // 当子窗口打开时，禁用本窗口
                this.Enabled = false;
                formVarEditor.textBox2.Focus();
                GlobalData.whatCaseFormVarEditorStart = CaseFormVarEditorStart.editlistViewSystemItem;
            }

        }

//------------------------------------------------------------------------------

        // 当用户单击 btnUserNew 时触发
        // 新建 listUserItem 子项
        private void New_listViewUserItem(object sender, EventArgs e)
        {

            FormVariableEditor formVarEditor;
            string formTitle;
            string textBoxVarName;
            string textBoxVarValue;

            formTitle = "新建用户变量";
            textBoxVarName = null;
            textBoxVarValue = null;

            formVarEditor = new FormVariableEditor(this, formTitle, textBoxVarName, textBoxVarValue);
            formVarEditor.Show();
            this.Enabled = false;
            formVarEditor.textBox1.Focus();

            GlobalData.whatCaseFormVarEditorStart = CaseFormVarEditorStart.addlistViewUserItem;

        }

//------------------------------------------------------------------------------

        // 当用户单击 btnSysNew 时触发
        // 新建 listSystemItem 子项
        private void New_listViewSystemItem(object sender, EventArgs e)
        {

            FormVariableEditor formVarEditor;
            string formTitle;
            string textBoxVarName;
            string textBoxVarValue;

            formTitle = "新建系统变量";
            textBoxVarName = null;
            textBoxVarValue = null;

            formVarEditor = new FormVariableEditor(this, formTitle, textBoxVarName, textBoxVarValue);
            formVarEditor.Show();
            this.Enabled = false;
            formVarEditor.textBox1.Focus();

            GlobalData.whatCaseFormVarEditorStart =  CaseFormVarEditorStart.addlistViewSystemItem;

        }

//------------------------------------------------------------------------------

        // 单击 btnUserDel 按钮时触发
        // 这个方法还不够完善，应在删除完一项后，自动定位下一项
        // 如果该项不是第一项，就往前删，如果是第一项，就往后删除
        // 当所有项都已经删除完成，将编辑按钮和删除按钮设置为不可选中
        private void Del_listViewUserItem(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection sel = this.listViewUser.SelectedIndices;
            if (sel.Count != 0)
            {
                this.listViewUser.Items[sel[0]].Remove();

                GlobalData.listViewUserChanged = true;
                this.btnSaveConfig.Enabled = true;
            }
            
            if (this.listViewUser.Items.Count == 0)
            {
                this.btnUserDel.Enabled = false;
                this.btnUserEdit.Enabled = false;
            }

        }

//------------------------------------------------------------------------------

        // 单击 btnSysDel 按钮时触发
        // 这个方法还不够完善，应在删除完一项后，自动定位下一项或上一项
        // 如果该项不是第一项，就往前删，如果是第一项，就往后删除
        // 当所有项都已经删除完成，将编辑按钮和删除按钮设置为不可选中
        private void Del_listViewSystemItem(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection sel = this.listViewSystem.SelectedIndices;
            if(sel.Count != 0)
            {
                this.listViewSystem.Items[sel[0]].Remove();

                GlobalData.listViewSystemChanged = true;
                this.btnSaveConfig.Enabled = true;
            }

            if (this.listViewSystem.Items.Count == 0)
            {
                this.btnSysDel.Enabled = false;
                this.btnSysEdit.Enabled = false;
            }

        }

//------------------------------------------------------------------------------

        //
        // 将listView中的值写入注册表
        //
        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            if (GlobalData.listViewUserChanged == true)
            {
                Registry.CurrentUser.DeleteSubKey("Environment");
                RegistryKey rkUserEnvVar = Registry.CurrentUser.CreateSubKey("Environment");
                foreach (ListViewItem item in this.listViewUser.Items)
                {
                    if (item.SubItems[1].Text.Contains('%'))
                    {
                        rkUserEnvVar.SetValue(item.Text, item.SubItems[1].Text, RegistryValueKind.ExpandString);
                    }
                    else
                    {
                        rkUserEnvVar.SetValue(item.Text, item.SubItems[1].Text);
                    }
                }
                GlobalData.listViewUserChanged = false;
            }

            if(GlobalData.listViewSystemChanged == true)
            {
                try
                {
                    Registry.LocalMachine.DeleteSubKey(@"SYSTEM\CurrentControlSet\Control\Session Manager\Environment");
                    rkSysEnvVar = Registry.LocalMachine.CreateSubKey(@"SYSTEM\CurrentControlSet\Control\Session Manager\Environment");
                    foreach (ListViewItem item in this.listViewSystem.Items)
                    {
                        if (item.SubItems[1].Text.Contains('%'))
                        {
                            rkSysEnvVar.SetValue(item.Text, item.SubItems[1].Text, RegistryValueKind.ExpandString);
                        }
                        else
                        {
                            rkSysEnvVar.SetValue(item.Text, item.SubItems[1].Text);
                        }
                    }
                    GlobalData.listViewSystemChanged = false;
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show("抱歉，操作失败！\r\n修改系统环境变量需要管理员权限（但是修改用户变量不需要），请以管理员身份运行此软件！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex2)
                {
                    MessageBox.Show("出现未知错误，操作失败！\r\n以下是错误详情:\r\n｛0｝", ex2.Message);
                }
            }

            this.btnSaveConfig.Enabled = false;
        }

//------------------------------------------------------------------------------

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(GlobalData.listViewUserChanged == true || GlobalData.listViewSystemChanged == true)
            {
                DialogResult dialog = MessageBox.Show("确定离开吗？您刚才所作的修改将不会生效。", "退出提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dialog == DialogResult.Cancel)
                    e.Cancel = true;
            }
        }

//------------------------------------------------------------------------------

        private void buttonAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("欢迎使用：变量大湿(v2.4）。\r\n请将软件运行错误或改进意见反馈至开发者邮箱，帮助改进此软件：\r\nuanaoeng@outlook.com\r\n\r\n变量大湿是开源软件，项目地址为：\r\nhttp://github.com/uanaoeng/EnvironmentVariableMaster");
        }

//------------------------------------------------------------------------------
        
        // 用户选中listViewUser中的Item并按下键盘时触发
        private void listViewUser_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                FormVariableEditor formVarEditor;
                string formTitle;
                string textBoxVarName;
                string textBoxVarValue;

                ListView.SelectedIndexCollection sel = this.listViewUser.SelectedIndices;
                if (sel.Count != 0)
                {
                    formTitle = "编辑用户变量";
                    textBoxVarName = this.listViewUser.Items[sel[0]].Text;
                    textBoxVarValue = this.listViewUser.Items[sel[0]].SubItems[1].Text.Replace(";", ";\r\n\r\n");
                    formVarEditor = new FormVariableEditor(this, formTitle, textBoxVarName, textBoxVarValue);
                    formVarEditor.Show();
                    // 当子窗口打开时，禁用本窗口
                    this.Enabled = false;
                    formVarEditor.textBox2.Focus();
                    GlobalData.whatCaseFormVarEditorStart = CaseFormVarEditorStart.editlistViewUserItem;
                }
            }
            if(e.KeyCode == Keys.Delete)
            {
                // 这段代码就是上面btnUserDel按钮按下时触发的方法，写在这里感觉重复了
                // 不知道有没办法直接调用？
                ListView.SelectedIndexCollection sel = this.listViewUser.SelectedIndices;
                if (sel.Count != 0)
                {
                    this.listViewUser.Items[sel[0]].Remove();

                    GlobalData.listViewUserChanged = true;
                    this.btnSaveConfig.Enabled = true;
                }

                if (this.listViewUser.Items.Count == 0)
                {
                    this.btnUserDel.Enabled = false;
                    this.btnUserEdit.Enabled = false;
                }
            }

        }

//------------------------------------------------------------------------------
        
        // 用户选中listViewSystem中的Item并按下键盘时触发
        private void listViewSystem_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                FormVariableEditor formVarEditor;
                string formTitle;
                string textBoxVarName;
                string textBoxVarValue;

                ListView.SelectedIndexCollection sel = this.listViewSystem.SelectedIndices;
                if (sel.Count != 0)
                {
                    formTitle = "编辑系统变量";
                    textBoxVarName = this.listViewSystem.Items[sel[0]].Text;
                    textBoxVarValue = this.listViewSystem.Items[sel[0]].SubItems[1].Text.Replace(";", ";\r\n\r\n");
                    formVarEditor = new FormVariableEditor(this, formTitle, textBoxVarName, textBoxVarValue);
                    formVarEditor.Show();
                    // 当子窗口打开时，禁用本窗口
                    this.Enabled = false;
                    formVarEditor.textBox2.Focus();
                    GlobalData.whatCaseFormVarEditorStart = CaseFormVarEditorStart.editlistViewSystemItem;
                }
            }
            if (e.KeyCode == Keys.Delete)
            {
                ListView.SelectedIndexCollection sel = this.listViewSystem.SelectedIndices;
                if (sel.Count != 0)
                {
                    this.listViewSystem.Items[sel[0]].Remove();

                    GlobalData.listViewSystemChanged = true;
                    this.btnSaveConfig.Enabled = true;
                }

                if (this.listViewSystem.Items.Count == 0)
                {
                    this.btnSysDel.Enabled = false;
                    this.btnSysEdit.Enabled = false;
                }
            }
        }


#endregion 结束 - 事件委托

    }
}

