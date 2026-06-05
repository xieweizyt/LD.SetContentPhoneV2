using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LD.SetContentPhone
{
    public partial class LoadingForm : Form
    {
        public LoadingForm()
        {
            InitializeComponent();
            // --- 窗体基础设置 ---
            this.FormBorderStyle = FormBorderStyle.None;     // 无边框
            this.StartPosition = FormStartPosition.CenterScreen; // 屏幕居中
            this.TopMost = true;                             // 永远置顶
            this.ShowInTaskbar = false;                      // 不显示在任务栏
            this.BackColor = Color.White;
            this.Opacity = 0.95;                             // 半透明效果
            this.Size = new Size(30, 30);                  // 固定大小
            this.ControlBox = false;                         // 禁止右上角关闭按钮

            // --- 防止 Alt+F4 或 ESC 关闭 ---
            this.KeyPreview = true;
            this.KeyDown += (s, e) =>
            {
                e.Handled = true; // 屏蔽所有按键
            };

            var lbl = new Label
            {
                Text = "正在加载...",
                Dock = DockStyle.Bottom,
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(lbl);

            var gif = new PictureBox
            {
                Image = Image.FromFile("File/loading.gif"),
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom
            };
            this.Controls.Add(gif);

        }
    }
}
