using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static YUMTool_R.Utils;

namespace YUMTool_R
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenURI("https://xyle-official.com");
            return;
        }

        private void LinkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenURI("https://github.com/xyle-gbp/yumtool_reloaded");
            return;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Close();
            return;
        }
    }
}
