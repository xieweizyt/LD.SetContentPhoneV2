namespace LD.SetContentPhone
{
    partial class SetSystem
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            txt_SendContent = new Sunny.UI.UITextBox();
            txt_SendPhone = new Sunny.UI.UITextBox();
            label2 = new Label();
            btn_Save = new Sunny.UI.UIButton();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(29, 44);
            label1.Name = "label1";
            label1.Size = new Size(79, 16);
            label1.TabIndex = 0;
            label1.Text = "发送内容:";
            // 
            // txt_SendContent
            // 
            txt_SendContent.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txt_SendContent.Location = new Point(119, 14);
            txt_SendContent.Margin = new Padding(4, 5, 4, 5);
            txt_SendContent.MinimumSize = new Size(1, 16);
            txt_SendContent.Multiline = true;
            txt_SendContent.Name = "txt_SendContent";
            txt_SendContent.Padding = new Padding(5);
            txt_SendContent.ShowText = false;
            txt_SendContent.Size = new Size(776, 90);
            txt_SendContent.TabIndex = 1;
            txt_SendContent.TextAlignment = ContentAlignment.MiddleLeft;
            txt_SendContent.Watermark = "";
            // 
            // txt_SendPhone
            // 
            txt_SendPhone.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txt_SendPhone.Location = new Point(119, 114);
            txt_SendPhone.Margin = new Padding(4, 5, 4, 5);
            txt_SendPhone.MinimumSize = new Size(1, 16);
            txt_SendPhone.Multiline = true;
            txt_SendPhone.Name = "txt_SendPhone";
            txt_SendPhone.Padding = new Padding(5);
            txt_SendPhone.ShowScrollBar = true;
            txt_SendPhone.ShowText = false;
            txt_SendPhone.Size = new Size(776, 90);
            txt_SendPhone.TabIndex = 3;
            txt_SendPhone.TextAlignment = ContentAlignment.MiddleLeft;
            txt_SendPhone.Watermark = "";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(29, 144);
            label2.Name = "label2";
            label2.Size = new Size(79, 16);
            label2.TabIndex = 2;
            label2.Text = "发送手机:";
            // 
            // btn_Save
            // 
            btn_Save.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btn_Save.Location = new Point(345, 269);
            btn_Save.MinimumSize = new Size(1, 1);
            btn_Save.Name = "btn_Save";
            btn_Save.Size = new Size(100, 35);
            btn_Save.TabIndex = 4;
            btn_Save.Text = "保存";
            btn_Save.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btn_Save.Click += btn_Save_Click;
            // 
            // SetSystem
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(919, 482);
            Controls.Add(btn_Save);
            Controls.Add(txt_SendPhone);
            Controls.Add(label2);
            Controls.Add(txt_SendContent);
            Controls.Add(label1);
            Name = "SetSystem";
            Text = "SetSystem";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Sunny.UI.UITextBox txt_SendContent;
        private Sunny.UI.UITextBox txt_SendPhone;
        private Label label2;
        private Sunny.UI.UIButton btn_Save;
    }
}