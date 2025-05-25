namespace KeyChat
{
    partial class P2PChat
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbox_my_fport = new System.Windows.Forms.TextBox();
            this.tbox_other_fport = new System.Windows.Forms.TextBox();
            this.tbox_msg = new System.Windows.Forms.TextBox();
            this.btn_send = new System.Windows.Forms.Button();
            this.btn_my_fset = new System.Windows.Forms.Button();
            this.btn_other_fset = new System.Windows.Forms.Button();
            this.tbox_my_ip = new System.Windows.Forms.TextBox();
            this.tbox_other_ip = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbox_msg = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbox_my_fport
            // 
            this.tbox_my_fport.Location = new System.Drawing.Point(9, 98);
            this.tbox_my_fport.Margin = new System.Windows.Forms.Padding(4);
            this.tbox_my_fport.Name = "tbox_my_fport";
            this.tbox_my_fport.Size = new System.Drawing.Size(242, 28);
            this.tbox_my_fport.TabIndex = 0;
            // 
            // tbox_other_fport
            // 
            this.tbox_other_fport.Location = new System.Drawing.Point(7, 380);
            this.tbox_other_fport.Margin = new System.Windows.Forms.Padding(4);
            this.tbox_other_fport.Name = "tbox_other_fport";
            this.tbox_other_fport.Size = new System.Drawing.Size(242, 28);
            this.tbox_other_fport.TabIndex = 1;
            // 
            // tbox_msg
            // 
            this.tbox_msg.Location = new System.Drawing.Point(288, 747);
            this.tbox_msg.Margin = new System.Windows.Forms.Padding(4);
            this.tbox_msg.Name = "tbox_msg";
            this.tbox_msg.Size = new System.Drawing.Size(1092, 28);
            this.tbox_msg.TabIndex = 2;
            this.tbox_msg.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbox_msg_KeyDown);
            // 
            // btn_send
            // 
            this.btn_send.Location = new System.Drawing.Point(1410, 738);
            this.btn_send.Margin = new System.Windows.Forms.Padding(4);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(131, 37);
            this.btn_send.TabIndex = 3;
            this.btn_send.Text = "보내기";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // btn_my_fset
            // 
            this.btn_my_fset.Location = new System.Drawing.Point(9, 134);
            this.btn_my_fset.Margin = new System.Windows.Forms.Padding(4);
            this.btn_my_fset.Name = "btn_my_fset";
            this.btn_my_fset.Size = new System.Drawing.Size(130, 42);
            this.btn_my_fset.TabIndex = 4;
            this.btn_my_fset.Text = "설정";
            this.btn_my_fset.UseVisualStyleBackColor = true;
            this.btn_my_fset.Click += new System.EventHandler(this.btn_my_fset_Click);
            // 
            // btn_other_fset
            // 
            this.btn_other_fset.Location = new System.Drawing.Point(7, 416);
            this.btn_other_fset.Margin = new System.Windows.Forms.Padding(4);
            this.btn_other_fset.Name = "btn_other_fset";
            this.btn_other_fset.Size = new System.Drawing.Size(130, 40);
            this.btn_other_fset.TabIndex = 5;
            this.btn_other_fset.Text = "설정";
            this.btn_other_fset.UseVisualStyleBackColor = true;
            this.btn_other_fset.Click += new System.EventHandler(this.btn_other_fset_Click);
            // 
            // tbox_my_ip
            // 
            this.tbox_my_ip.Location = new System.Drawing.Point(9, 29);
            this.tbox_my_ip.Name = "tbox_my_ip";
            this.tbox_my_ip.Size = new System.Drawing.Size(240, 28);
            this.tbox_my_ip.TabIndex = 7;
            // 
            // tbox_other_ip
            // 
            this.tbox_other_ip.Location = new System.Drawing.Point(6, 317);
            this.tbox_other_ip.Name = "tbox_other_ip";
            this.tbox_other_ip.Size = new System.Drawing.Size(243, 28);
            this.tbox_other_ip.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 18);
            this.label1.TabIndex = 9;
            this.label1.Text = "IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 18);
            this.label2.TabIndex = 10;
            this.label2.Text = "port";
            // 
            // lbox_msg
            // 
            this.lbox_msg.FormattingEnabled = true;
            this.lbox_msg.ItemHeight = 18;
            this.lbox_msg.Location = new System.Drawing.Point(288, 29);
            this.lbox_msg.Name = "lbox_msg";
            this.lbox_msg.Size = new System.Drawing.Size(1230, 670);
            this.lbox_msg.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 296);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 18);
            this.label3.TabIndex = 12;
            this.label3.Text = "IP";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 358);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 18);
            this.label4.TabIndex = 13;
            this.label4.Text = "port";
            // 
            // P2PChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1554, 828);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbox_msg);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbox_other_ip);
            this.Controls.Add(this.tbox_my_ip);
            this.Controls.Add(this.btn_other_fset);
            this.Controls.Add(this.btn_my_fset);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.tbox_msg);
            this.Controls.Add(this.tbox_other_fport);
            this.Controls.Add(this.tbox_my_fport);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "P2PChat";
            this.Text = "P2PChat";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbox_my_fport;
        private System.Windows.Forms.TextBox tbox_other_fport;
        private System.Windows.Forms.TextBox tbox_msg;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.Button btn_my_fset;
        private System.Windows.Forms.Button btn_other_fset;
        private System.Windows.Forms.TextBox tbox_my_ip;
        private System.Windows.Forms.TextBox tbox_other_ip;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lbox_msg;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

