using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net.NetworkInformation;
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

        private void FormAbout_Load(object sender, EventArgs e)
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                pictureBox3.ImageLocation = "https://avatars.githubusercontent.com/u/59692068?v=4";
            }
            else
            {
                pictureBox3.Image = Properties.Resources.IMG_1540;
            }
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

        private void pictureBox3_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                base.OnPaint(e);
                Bitmap canvas = new Bitmap(pictureBox3.Width, pictureBox3.Height);
                Graphics g = Graphics.FromImage(canvas);
                GraphicsPath gp = new();
                gp.AddEllipse(g.VisibleClipBounds);
                Region rgn = new(gp);
                pictureBox3.Region = rgn;
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Localize.UnexpectedError, ex.ToString()), Localize.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }
        }

        
    }
}
