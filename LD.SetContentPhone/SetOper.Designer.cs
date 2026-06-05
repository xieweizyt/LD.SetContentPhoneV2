namespace LD.SetContentPhone
{
    partial class SetOper
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            uiDataGridView1 = new Sunny.UI.UIDataGridView();
            ComName = new DataGridViewTextBoxColumn();
            Carrier = new DataGridViewTextBoxColumn();
            CenterPhone = new DataGridViewTextBoxColumn();
            SendCount = new DataGridViewTextBoxColumn();
            SendSuccessCount = new DataGridViewTextBoxColumn();
            SendFailCount = new DataGridViewTextBoxColumn();
            btn_import = new Sunny.UI.UIButton();
            btn_Start = new Sunny.UI.UIButton();
            btn_Pause = new Sunny.UI.UIButton();
            btn_Continue = new Sunny.UI.UIButton();
            btn_Stop = new Sunny.UI.UIButton();
            led_SendCount = new Sunny.UI.UIDigitalLabel();
            ((System.ComponentModel.ISupportInitialize)uiDataGridView1).BeginInit();
            SuspendLayout();
            // 
            // uiDataGridView1
            // 
            uiDataGridView1.AllowUserToAddRows = false;
            uiDataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(235, 243, 255);
            uiDataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            uiDataGridView1.BackgroundColor = Color.White;
            uiDataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle2.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            uiDataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            uiDataGridView1.ColumnHeadersHeight = 32;
            uiDataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            uiDataGridView1.Columns.AddRange(new DataGridViewColumn[] { ComName, Carrier, CenterPhone, SendCount, SendSuccessCount, SendFailCount });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            uiDataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            uiDataGridView1.EnableHeadersVisualStyles = false;
            uiDataGridView1.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiDataGridView1.GridColor = Color.FromArgb(80, 160, 255);
            uiDataGridView1.Location = new Point(12, 12);
            uiDataGridView1.Name = "uiDataGridView1";
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(235, 243, 255);
            dataGridViewCellStyle4.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle4.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle4.SelectionForeColor = Color.White;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            uiDataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.BackColor = Color.White;
            dataGridViewCellStyle5.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiDataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle5;
            uiDataGridView1.SelectedIndex = -1;
            uiDataGridView1.Size = new Size(1353, 527);
            uiDataGridView1.StripeOddColor = Color.FromArgb(235, 243, 255);
            uiDataGridView1.TabIndex = 0;
            // 
            // ComName
            // 
            ComName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ComName.DataPropertyName = "ComName";
            ComName.HeaderText = "com口";
            ComName.Name = "ComName";
            // 
            // Carrier
            // 
            Carrier.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Carrier.DataPropertyName = "Carrier";
            Carrier.HeaderText = "运营商";
            Carrier.Name = "Carrier";
            // 
            // CenterPhone
            // 
            CenterPhone.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            CenterPhone.DataPropertyName = "CenterPhone";
            CenterPhone.HeaderText = "中心号码";
            CenterPhone.Name = "CenterPhone";
            // 
            // SendCount
            // 
            SendCount.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            SendCount.DataPropertyName = "SendCount";
            SendCount.HeaderText = "已发送次数";
            SendCount.Name = "SendCount";
            // 
            // SendSuccessCount
            // 
            SendSuccessCount.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            SendSuccessCount.DataPropertyName = "SendSuccessCount";
            SendSuccessCount.HeaderText = "成功次数";
            SendSuccessCount.Name = "SendSuccessCount";
            // 
            // SendFailCount
            // 
            SendFailCount.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            SendFailCount.DataPropertyName = "SendFailCount";
            SendFailCount.HeaderText = "失败次数";
            SendFailCount.Name = "SendFailCount";
            // 
            // btn_import
            // 
            btn_import.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btn_import.Location = new Point(12, 563);
            btn_import.MinimumSize = new Size(1, 1);
            btn_import.Name = "btn_import";
            btn_import.Size = new Size(100, 35);
            btn_import.TabIndex = 1;
            btn_import.Text = "导入";
            btn_import.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btn_import.Click += btn_import_Click;
            // 
            // btn_Start
            // 
            btn_Start.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btn_Start.Location = new Point(127, 563);
            btn_Start.MinimumSize = new Size(1, 1);
            btn_Start.Name = "btn_Start";
            btn_Start.Size = new Size(100, 35);
            btn_Start.TabIndex = 1;
            btn_Start.Text = "开始";
            btn_Start.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btn_Start.Click += btn_Start_Click;
            // 
            // btn_Pause
            // 
            btn_Pause.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btn_Pause.Location = new Point(244, 563);
            btn_Pause.MinimumSize = new Size(1, 1);
            btn_Pause.Name = "btn_Pause";
            btn_Pause.Size = new Size(100, 35);
            btn_Pause.TabIndex = 1;
            btn_Pause.Text = "暂停";
            btn_Pause.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btn_Pause.Click += btn_Pause_Click;
            // 
            // btn_Continue
            // 
            btn_Continue.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btn_Continue.Location = new Point(361, 563);
            btn_Continue.MinimumSize = new Size(1, 1);
            btn_Continue.Name = "btn_Continue";
            btn_Continue.Size = new Size(100, 35);
            btn_Continue.TabIndex = 1;
            btn_Continue.Text = "继续";
            btn_Continue.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btn_Continue.Click += btn_Continue_Click;
            // 
            // btn_Stop
            // 
            btn_Stop.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btn_Stop.Location = new Point(479, 563);
            btn_Stop.MinimumSize = new Size(1, 1);
            btn_Stop.Name = "btn_Stop";
            btn_Stop.Size = new Size(100, 35);
            btn_Stop.TabIndex = 1;
            btn_Stop.Text = "停止";
            btn_Stop.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btn_Stop.Click += btn_Stop_Click;
            // 
            // led_SendCount
            // 
            led_SendCount.BackColor = Color.Black;
            led_SendCount.DecimalPlaces = 0;
            led_SendCount.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            led_SendCount.ForeColor = Color.Lime;
            led_SendCount.Location = new Point(12, 616);
            led_SendCount.MinimumSize = new Size(1, 1);
            led_SendCount.Name = "led_SendCount";
            led_SendCount.Size = new Size(125, 42);
            led_SendCount.TabIndex = 2;
            led_SendCount.Text = "uiDigitalLabel1";
            // 
            // SetOper
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1377, 670);
            Controls.Add(led_SendCount);
            Controls.Add(btn_Stop);
            Controls.Add(btn_Continue);
            Controls.Add(btn_Pause);
            Controls.Add(btn_Start);
            Controls.Add(btn_import);
            Controls.Add(uiDataGridView1);
            Name = "SetOper";
            Text = "SetOper";
            Initialize += SetOper_Initialize;
            ((System.ComponentModel.ISupportInitialize)uiDataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Sunny.UI.UIDataGridView uiDataGridView1;
        private DataGridViewTextBoxColumn ComName;
        private DataGridViewTextBoxColumn Carrier;
        private DataGridViewTextBoxColumn CenterPhone;
        private DataGridViewTextBoxColumn SendCount;
        private DataGridViewTextBoxColumn SendSuccessCount;
        private DataGridViewTextBoxColumn SendFailCount;
        private Sunny.UI.UIButton btn_import;
        private Sunny.UI.UIButton btn_Start;
        private Sunny.UI.UIButton btn_Pause;
        private Sunny.UI.UIButton btn_Continue;
        private Sunny.UI.UIButton btn_Stop;
        private Sunny.UI.UIDigitalLabel led_SendCount;
    }
}
