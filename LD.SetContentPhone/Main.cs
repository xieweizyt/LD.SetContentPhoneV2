using LD.SetContentPhone.Managers;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LD.SetContentPhone
{
    public partial class Main : UIForm
    {
        public Main()
        {
            InitializeComponent();

            this.MainTabControl = uiTabControl1;
            uiNavMenu1.TabControl = uiTabControl1;

            //设置初始页面索引（关联页面，唯一不重复即可）
            int pageIndex = 1000;

            int index = pageIndex++;
            AddPage(new SetSystem(), index);
            uiNavMenu1.CreateNode("配置", index);

            index = pageIndex++;
            AddPage(new SetOper(), index);
            uiNavMenu1.CreateNode("操作", index);

        }
    }
}
