using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FazEngine2D.Classes
{
    using System.Windows.Forms;
    public class WindowHelper : Form
    {
        public WindowHelper()
        {
            this.DoubleBuffered = true;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // WindowHelper
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "WindowHelper";
            this.Load += new System.EventHandler(this.WindowHelper_Load);
            this.ResumeLayout(false);

        }

        private void WindowHelper_Load(object sender, EventArgs e)
        {

        }
    }
}
