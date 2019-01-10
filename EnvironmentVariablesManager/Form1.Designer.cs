namespace EnvironmentVariablesManager
{
    partial class FormMain
    {
        // 有关这个类的说明：
        // 这个类的另一部分在文件Form1.cs中

        #region 开始 - 字段声明

        #region 这个声明不用管它，看不懂没关系
        // 必需的设计器变量。
        private System.ComponentModel.IContainer components = null;
        #endregion

        // 声明GroupBox控件
        public System.Windows.Forms.GroupBox groupBoxUser;
        public System.Windows.Forms.GroupBox groupBoxSystem;

        // 声明ListView控件
        public System.Windows.Forms.ListView listViewUser;
        public System.Windows.Forms.ListView listViewSystem;

        // 声明Button控件
        public System.Windows.Forms.Button btnSaveConfig;
        private System.Windows.Forms.Button btnUserDel;
        private System.Windows.Forms.Button btnUserEdit;
        private System.Windows.Forms.Button btnUserNew;
        private System.Windows.Forms.Button btnSysDel;
        private System.Windows.Forms.Button btnSysNew;
        private System.Windows.Forms.Button btnSysEdit;

        #endregion 结束 - 字段声明

        #region 开始 - 方法声明


        #region 在这个方法中对窗体中的控件的属性进行设置
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.groupBoxUser = new System.Windows.Forms.GroupBox();
            this.listViewUser = new System.Windows.Forms.ListView();
            this.btnUserEdit = new System.Windows.Forms.Button();
            this.btnUserNew = new System.Windows.Forms.Button();
            this.btnUserDel = new System.Windows.Forms.Button();
            this.groupBoxSystem = new System.Windows.Forms.GroupBox();
            this.btnSysDel = new System.Windows.Forms.Button();
            this.btnSysNew = new System.Windows.Forms.Button();
            this.btnSysEdit = new System.Windows.Forms.Button();
            this.listViewSystem = new System.Windows.Forms.ListView();
            this.btnSaveConfig = new System.Windows.Forms.Button();
            this.buttonAbout = new System.Windows.Forms.Button();
            this.groupBoxUser.SuspendLayout();
            this.groupBoxSystem.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxUser
            // 
            this.groupBoxUser.Controls.Add(this.listViewUser);
            this.groupBoxUser.Controls.Add(this.btnUserEdit);
            this.groupBoxUser.Controls.Add(this.btnUserNew);
            this.groupBoxUser.Controls.Add(this.btnUserDel);
            this.groupBoxUser.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBoxUser.Location = new System.Drawing.Point(12, 12);
            this.groupBoxUser.Name = "groupBoxUser";
            this.groupBoxUser.Size = new System.Drawing.Size(658, 300);
            this.groupBoxUser.TabIndex = 1;
            this.groupBoxUser.TabStop = false;
            this.groupBoxUser.Text = "用户变量(&U)";
            // 
            // listViewUser
            // 
            this.listViewUser.Location = new System.Drawing.Point(6, 33);
            this.listViewUser.Name = "listViewUser";
            this.listViewUser.Size = new System.Drawing.Size(646, 220);
            this.listViewUser.TabIndex = 2;
            this.listViewUser.UseCompatibleStateImageBehavior = false;
            // 
            // btnUserEdit
            // 
            this.btnUserEdit.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnUserEdit.Location = new System.Drawing.Point(406, 259);
            this.btnUserEdit.Name = "btnUserEdit";
            this.btnUserEdit.Size = new System.Drawing.Size(120, 35);
            this.btnUserEdit.TabIndex = 4;
            this.btnUserEdit.Text = "编辑(&E)";
            this.btnUserEdit.UseVisualStyleBackColor = false;
            // 
            // btnUserNew
            // 
            this.btnUserNew.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnUserNew.Location = new System.Drawing.Point(280, 259);
            this.btnUserNew.Name = "btnUserNew";
            this.btnUserNew.Size = new System.Drawing.Size(120, 35);
            this.btnUserNew.TabIndex = 3;
            this.btnUserNew.Text = "新建(&N)";
            this.btnUserNew.UseVisualStyleBackColor = false;
            // 
            // btnUserDel
            // 
            this.btnUserDel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnUserDel.Location = new System.Drawing.Point(532, 259);
            this.btnUserDel.Name = "btnUserDel";
            this.btnUserDel.Size = new System.Drawing.Size(120, 35);
            this.btnUserDel.TabIndex = 5;
            this.btnUserDel.Text = "删除(&D)";
            this.btnUserDel.UseVisualStyleBackColor = false;
            // 
            // groupBoxSystem
            // 
            this.groupBoxSystem.Controls.Add(this.btnSysDel);
            this.groupBoxSystem.Controls.Add(this.btnSysNew);
            this.groupBoxSystem.Controls.Add(this.btnSysEdit);
            this.groupBoxSystem.Controls.Add(this.listViewSystem);
            this.groupBoxSystem.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBoxSystem.Location = new System.Drawing.Point(12, 338);
            this.groupBoxSystem.Name = "groupBoxSystem";
            this.groupBoxSystem.Size = new System.Drawing.Size(658, 300);
            this.groupBoxSystem.TabIndex = 6;
            this.groupBoxSystem.TabStop = false;
            this.groupBoxSystem.Text = "系统变量(&S)（需要以管理员身份运行此软件）";
            // 
            // btnSysDel
            // 
            this.btnSysDel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnSysDel.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSysDel.Location = new System.Drawing.Point(532, 259);
            this.btnSysDel.Name = "btnSysDel";
            this.btnSysDel.Size = new System.Drawing.Size(120, 35);
            this.btnSysDel.TabIndex = 10;
            this.btnSysDel.Text = "删除(&L)";
            this.btnSysDel.UseVisualStyleBackColor = false;
            // 
            // btnSysNew
            // 
            this.btnSysNew.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnSysNew.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSysNew.Location = new System.Drawing.Point(280, 259);
            this.btnSysNew.Name = "btnSysNew";
            this.btnSysNew.Size = new System.Drawing.Size(120, 35);
            this.btnSysNew.TabIndex = 8;
            this.btnSysNew.Text = "新建(&W)";
            this.btnSysNew.UseVisualStyleBackColor = false;
            // 
            // btnSysEdit
            // 
            this.btnSysEdit.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnSysEdit.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSysEdit.Location = new System.Drawing.Point(406, 259);
            this.btnSysEdit.Name = "btnSysEdit";
            this.btnSysEdit.Size = new System.Drawing.Size(120, 35);
            this.btnSysEdit.TabIndex = 9;
            this.btnSysEdit.Text = " 编辑(&I)";
            this.btnSysEdit.UseVisualStyleBackColor = false;
            // 
            // listViewSystem
            // 
            this.listViewSystem.Location = new System.Drawing.Point(6, 33);
            this.listViewSystem.Name = "listViewSystem";
            this.listViewSystem.Size = new System.Drawing.Size(646, 220);
            this.listViewSystem.TabIndex = 7;
            this.listViewSystem.UseCompatibleStateImageBehavior = false;
            this.listViewSystem.View = System.Windows.Forms.View.Details;
            // 
            // btnSaveConfig
            // 
            this.btnSaveConfig.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnSaveConfig.Enabled = false;
            this.btnSaveConfig.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSaveConfig.Location = new System.Drawing.Point(550, 656);
            this.btnSaveConfig.Name = "btnSaveConfig";
            this.btnSaveConfig.Size = new System.Drawing.Size(120, 35);
            this.btnSaveConfig.TabIndex = 11;
            this.btnSaveConfig.Text = "应用(&B)";
            this.btnSaveConfig.UseVisualStyleBackColor = false;
            // 
            // buttonAbout
            // 
            this.buttonAbout.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonAbout.Location = new System.Drawing.Point(12, 656);
            this.buttonAbout.Name = "buttonAbout";
            this.buttonAbout.Size = new System.Drawing.Size(120, 35);
            this.buttonAbout.TabIndex = 12;
            this.buttonAbout.Text = "关于(&A)";
            this.buttonAbout.UseVisualStyleBackColor = true;
            this.buttonAbout.Click += new System.EventHandler(this.buttonAbout_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(682, 703);
            this.Controls.Add(this.buttonAbout);
            this.Controls.Add(this.groupBoxSystem);
            this.Controls.Add(this.groupBoxUser);
            this.Controls.Add(this.btnSaveConfig);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "变量大湿";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.groupBoxUser.ResumeLayout(false);
            this.groupBoxSystem.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion


        #region 这个方法不用管它
        protected override void Dispose(bool disposing)
        {
            /// <summary>
            /// 清理所有正在使用的资源。
            /// </summary>
            /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion

        private System.Windows.Forms.Button buttonAbout;



        #endregion 结束 - 方法声明



    }
}

