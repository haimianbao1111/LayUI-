namespace MyWinForm
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnJianCe = new System.Windows.Forms.Button();
            this.btnYunXing = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.LShuoMing = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.LYiShiYong = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnJianCe);
            this.panel1.Controls.Add(this.btnYunXing);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.LShuoMing);
            this.panel1.Controls.Add(this.listBox1);
            this.panel1.Controls.Add(this.LYiShiYong);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(485, 352);
            this.panel1.TabIndex = 5;
            // 
            // btnJianCe
            // 
            this.btnJianCe.Location = new System.Drawing.Point(151, 295);
            this.btnJianCe.Name = "btnJianCe";
            this.btnJianCe.Size = new System.Drawing.Size(75, 23);
            this.btnJianCe.TabIndex = 5;
            this.btnJianCe.Text = "检测";
            this.btnJianCe.UseVisualStyleBackColor = true;
            this.btnJianCe.Click += new System.EventHandler(this.btnJianCe_Click);
            // 
            // btnYunXing
            // 
            this.btnYunXing.Location = new System.Drawing.Point(15, 295);
            this.btnYunXing.Name = "btnYunXing";
            this.btnYunXing.Size = new System.Drawing.Size(75, 23);
            this.btnYunXing.TabIndex = 4;
            this.btnYunXing.Text = "运行";
            this.btnYunXing.UseVisualStyleBackColor = true;
            this.btnYunXing.Click += new System.EventHandler(this.btnYunXing_Click);
            // 
            // textBox1
            // 
            this.textBox1.AcceptsReturn = true;
            this.textBox1.CausesValidation = false;
            this.textBox1.Location = new System.Drawing.Point(278, 91);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(171, 197);
            this.textBox1.TabIndex = 3;
            // 
            // LShuoMing
            // 
            this.LShuoMing.AutoSize = true;
            this.LShuoMing.Location = new System.Drawing.Point(275, 72);
            this.LShuoMing.Name = "LShuoMing";
            this.LShuoMing.Size = new System.Drawing.Size(29, 12);
            this.LShuoMing.TabIndex = 2;
            this.LShuoMing.Text = "说明";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(12, 89);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(260, 196);
            this.listBox1.TabIndex = 1;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // LYiShiYong
            // 
            this.LYiShiYong.AutoSize = true;
            this.LYiShiYong.Location = new System.Drawing.Point(12, 72);
            this.LYiShiYong.Name = "LYiShiYong";
            this.LYiShiYong.Size = new System.Drawing.Size(65, 12);
            this.LYiShiYong.TabIndex = 0;
            this.LYiShiYong.Text = "已使用控件";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusText});
            this.statusStrip1.Location = new System.Drawing.Point(0, 376);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(485, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusText
            // 
            this.StatusText.Name = "StatusText";
            this.StatusText.Size = new System.Drawing.Size(32, 17);
            this.StatusText.Text = "就绪";
            // 
            // MainMenu
            // 
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(485, 24);
            this.MainMenu.TabIndex = 4;
            this.MainMenu.Text = "menuStrip1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 398);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.MainMenu);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnJianCe;
        private System.Windows.Forms.Button btnYunXing;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label LShuoMing;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label LYiShiYong;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusText;
        private System.Windows.Forms.MenuStrip MainMenu;
    }
}

