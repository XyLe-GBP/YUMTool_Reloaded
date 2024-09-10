using PrivateProfile;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YUMTool_R
{
    public partial class FormSettings : Form
    {
        public FormSettings()
        {
            InitializeComponent();
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            var ini = new IniFile(@".\\settings.ini");
            int check1, check2, combo;
            string tbox;

            check1 = ini.GetInt("SETTINGS", "0x1000", 0xFFFF);
            check2 = ini.GetInt("SETTINGS", "0x1001", 0xFFFF);
            combo = ini.GetInt("SETTINGS", "0x2000", 0xFFFF);
            tbox = ini.GetString("SETTINGS", "0x0000", "");

            switch (check1)
            {
                case 0:
                    checkBox1.Checked = false;
                    checkBox2.Enabled = true;
                    break;
                case 1:
                    comboBox1.Enabled = true;
                    textBox1.Enabled = true;
                    checkBox1.Checked = true;
                    checkBox2.Checked = false;
                    checkBox2.Enabled = false;
                    break;
                default:
                    comboBox1.Enabled = false;
                    textBox1.Enabled = false;
                    textBox1.Text = null;
                    checkBox1.Checked = false;
                    checkBox2.Checked = false;
                    checkBox2.Enabled = true;
                    break;
            }

            switch (check2)
            {
                case 0:
                    checkBox1.Enabled = true;
                    checkBox2.Checked = false;
                    break;
                case 1:
                    comboBox1.Enabled = false;
                    textBox1.Enabled = false;
                    textBox1.Text = null;
                    checkBox1.Checked = false;
                    checkBox1.Enabled = false;
                    checkBox2.Checked = true;
                    break;
                default:
                    comboBox1.Enabled = false;
                    textBox1.Enabled = false;
                    textBox1.Text = null;
                    checkBox1.Checked = false;
                    checkBox1.Enabled = true;
                    checkBox2.Checked = false;
                    break;
            }

            switch (combo)
            {
                case 0:
                    comboBox1.SelectedIndex = (int)combo;
                    break;
                default:
                    comboBox1.SelectedIndex = 0;
                    break;
            }

            if (tbox != "")
            {
                textBox1.Text = tbox[2..];
            }
            else
            {
                textBox1.Text = null;
            }
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked != false)
            {
                comboBox1.Enabled = true;
                textBox1.Enabled = true;
                textBox1.Text = "2000";
                checkBox2.Checked = false;
                checkBox2.Enabled = false;
            }
            else
            {
                comboBox1.Enabled = false;
                textBox1.Enabled = false;
                textBox1.Text = null;
                checkBox2.Checked = false;
                checkBox2.Enabled = true;
            }
        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked != false)
            {
                comboBox1.Enabled = false;
                textBox1.Enabled = false;
                textBox1.Text = null;
                checkBox1.Checked = false;
                checkBox1.Enabled = false;
            }
            else
            {
                comboBox1.Enabled = false;
                textBox1.Enabled = false;
                textBox1.Text = null;
                checkBox1.Checked = false;
                checkBox1.Enabled = true;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var ini = new IniFile(@".\\settings.ini");

            if (checkBox1.Checked != false)
            {
                if (textBox1.Text.Length < 1)
                {
                    if (textBox1.Text.Length >= 1)
                    {
                        ini.WriteString("SETTINGS", "0x0000", "0x" + textBox1.Text);
                    }
                    else
                    {
                        MessageBox.Show(this, Localize.ErrorOffsetLength, Localize.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    ini.WriteString("SETTINGS", "0x0000", "0x" + textBox1.Text);
                }

                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        ini.WriteString("SETTINGS", "0x2000", "0");
                        break;
                    default:
                        ini.WriteString("SETTINGS", "0x2000", "");
                        break;
                }

                ini.WriteString("SETTINGS", "0x1000", "1");
                ini.WriteString("SETTINGS", "0x1001", "0");
            }
            else
            {
                ini.WriteString("SETTINGS", "0x1000", "0");
            }
            if (checkBox2.Checked != false)
            {
                ini.WriteString("SETTINGS", "0x0000", "");
                ini.WriteString("SETTINGS", "0x1000", "0");
                ini.WriteString("SETTINGS", "0x1001", "1");
                ini.WriteString("SETTINGS", "0x2000", "");
            }
            else
            {
                ini.WriteString("SETTINGS", "0x1001", "0");
            }
            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }
    }
}
