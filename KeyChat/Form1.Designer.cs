namespace KeyChat
{
    partial class Form1
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.btn_send = new System.Windows.Forms.Button();
            this.btn_setting1 = new System.Windows.Forms.Button();
            this.btn_setting2 = new System.Windows.Forms.Button();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 88);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(194, 25);
            this.textBox1.TabIndex = 0;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(12, 160);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(194, 25);
            this.textBox2.TabIndex = 1;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(230, 537);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(874, 25);
            this.textBox3.TabIndex = 2;
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // btn_send
            // 
            this.btn_send.Location = new System.Drawing.Point(1126, 537);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(105, 31);
            this.btn_send.TabIndex = 3;
            this.btn_send.Text = "보내기";
            this.btn_send.UseVisualStyleBackColor = true;
            // 
            // btn_setting1
            // 
            this.btn_setting1.Location = new System.Drawing.Point(12, 119);
            this.btn_setting1.Name = "btn_setting1";
            this.btn_setting1.Size = new System.Drawing.Size(104, 35);
            this.btn_setting1.TabIndex = 4;
            this.btn_setting1.Text = "설정";
            this.btn_setting1.UseVisualStyleBackColor = true;
            // 
            // btn_setting2
            // 
            this.btn_setting2.Location = new System.Drawing.Point(12, 191);
            this.btn_setting2.Name = "btn_setting2";
            this.btn_setting2.Size = new System.Drawing.Size(104, 33);
            this.btn_setting2.TabIndex = 5;
            this.btn_setting2.Text = "설정";
            this.btn_setting2.UseVisualStyleBackColor = true;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(230, 33);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(987, 484);
            this.textBox4.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1243, 690);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.btn_setting2);
            this.Controls.Add(this.btn_setting1);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.Button btn_setting1;
        private System.Windows.Forms.Button btn_setting2;
        private System.Windows.Forms.TextBox textBox4;
    }
}

