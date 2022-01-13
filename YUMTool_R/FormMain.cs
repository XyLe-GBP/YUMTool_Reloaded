using PrivateProfile;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static YUMTool_R.Common;
using static YUMTool_R.Utils;

namespace YUMTool_R
{
    public partial class FormMain : Form
    {
        const int WM_GETTEXTLENGTH = 0x000E;
        const int EM_SETSEL = 0x00B1;
        const int EM_REPLACESEL = 0x00C2;

        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, string lParam);

        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, int lParam);

        public FormMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FileVersionInfo ver = FileVersionInfo.GetVersionInfo(Application.ExecutablePath);
            if (ver.FileVersion != null)
            {
                Text = "YUMTool Rel ( build: " + ver.FileVersion.ToString() + "-Beta )";
            }

            TemporaryFileCheckExists();

            if (File.Exists(@"C:\\Perl64\\bin\\perl.exe"))
            {
                perlflag = true;
                WriteLog(string.Format(Localize.FoundActivePerl, "C:\\Perl64\\bin\\perl.exe"));
            }
            else if (File.Exists(@"C:\\Strawberry\\perl\\bin\\perl.exe"))
            {
                perlflag = true;
                WriteLog(string.Format(Localize.FoundStrawberryPerl, "C:\\Strawberry\\perl\\bin\\perl.exe"));
            }
            else
            {
                perlflag = false;
            }

            if (perlflag != true)
            {
                MessageBox.Show(this, Localize.ErrorPerlNotFound, Localize.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
            else
            {
                listView1.View = View.Details;
                listView1.Columns.Add(Localize.ColumnsName, 100);
                listView1.Columns.Add(Localize.ColumnsSize, 125);
                listView1.Columns.Add(Localize.ColumnsType, 200);

                WriteLog(Localize.RunningApp);
            }
        }

        /// <summary>
        /// アーカイブファイルを読み込む関数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new()
            {
                FileName = "",
                InitialDirectory = "",
                Filter = Localize.FilterArchive,
                FilterIndex = 1,
                Title = Localize.OpenFileDialogArchive,
                RestoreDirectory = true
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                FileInfo file = new(ofd.FileName);
                long filesize = file.Length;
                string filesizestr = String.Format(Localize.FileSizeText, filesize);
                string ext = Path.GetExtension(ofd.FileName);
                ListViewItem lvi;

                if (ext.ToUpper() == ".BIN")
                {
                    switch (Path.GetFileNameWithoutExtension(ofd.FileName))
                    {
                        case "YUMFILE_1":
                            if (filesize != yum1_original_size)
                            {
                                MessageBox.Show(this, string.Format(Localize.UnexpectedArchiveFileSize, ofd.FileName), Localize.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                WriteLog(string.Format(Localize.ErrorArchiveNotGenuine, ofd.FileName));
                                break;
                            }
                            else
                            {
                                listView1.Items.Clear();
                                lvi = listView1.Items.Add(Path.GetFileNameWithoutExtension(ofd.FileName));
                                lvi.SubItems.Add(filesizestr);
                                lvi.SubItems.Add(Localize.ArchiveTypePS);
                                pictureBox2.Image = Properties.Resources.logo_ps;
                                pictureBox2.Refresh();
                                button1.Enabled = true;
                                button2.Enabled = false;
                                yumflag = 0;
                                current_yum = "YUMFILE_1.BIN";
                                current_filepath = ofd.FileName;
                                WriteLog(string.Format(Localize.ReadedArchive, ofd.FileName, filesizestr, Localize.ArchiveTypePS));
                                break;
                            }
                        case "YUMFILE_2":
                            if (filesize != yum2_original_size)
                            {
                                MessageBox.Show(this, string.Format(Localize.UnexpectedArchiveFileSize, ofd.FileName), Localize.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                WriteLog(string.Format(Localize.ErrorArchiveNotGenuine, ofd.FileName));
                                break;
                            }
                            else
                            {
                                listView1.Items.Clear();
                                lvi = listView1.Items.Add(Path.GetFileNameWithoutExtension(ofd.FileName));
                                lvi.SubItems.Add(filesizestr);
                                lvi.SubItems.Add(Localize.ArchiveTypeWS);
                                pictureBox2.Image = Properties.Resources.logo_ws;
                                pictureBox2.Refresh();
                                button1.Enabled = true;
                                button2.Enabled = false;
                                yumflag = 1;
                                current_yum = "YUMFILE_2.BIN";
                                current_filepath = ofd.FileName;
                                WriteLog(string.Format(Localize.ReadedArchive, ofd.FileName, filesizestr, Localize.ArchiveTypeWS));
                                break;
                            }
                        case "YUMFILE_3":
                            if (filesize != yum3_original_size)
                            {
                                MessageBox.Show(this, string.Format(Localize.UnexpectedArchiveFileSize, ofd.FileName), Localize.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                WriteLog(string.Format(Localize.ErrorArchiveNotGenuine, ofd.FileName));
                                break;
                            }
                            else
                            {
                                listView1.Items.Clear();
                                lvi = listView1.Items.Add(Path.GetFileNameWithoutExtension(ofd.FileName));
                                lvi.SubItems.Add(filesizestr);
                                lvi.SubItems.Add(Localize.ArchiveTypeMM);
                                pictureBox2.Image = Properties.Resources.logo_mm;
                                pictureBox2.Refresh();
                                button1.Enabled = true;
                                button2.Enabled = false;
                                yumflag = 2;
                                current_yum = "YUMFILE_3.BIN";
                                current_filepath = ofd.FileName;
                                WriteLog(string.Format(Localize.ReadedArchive, ofd.FileName, filesizestr, Localize.ArchiveTypeMM));
                                break;
                            }
                        default:
                            MessageBox.Show(this, Localize.UnexpectedNotArchive, Localize.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            WriteLog(string.Format(Localize.ErrorArchive, ofd.FileName));
                            listView1.Items.Clear();
                            pictureBox2.Image = Properties.Resources.logo_not;
                            pictureBox2.Refresh();
                            button1.Enabled = false;
                            button2.Enabled = false;
                            yumflag = -1;
                            current_yum = null;
                            current_filepath = null;
                            break;
                    }
                    return;
                }
                else
                {
                    listView1.Items.Clear();
                    pictureBox2.Image = Properties.Resources.logo_not;
                    pictureBox2.Refresh();
                    button1.Enabled = false;
                    button2.Enabled = false;
                    yumflag = -1;
                    current_yum = null;
                    current_filepath = null;
                    return;
                }
            }

        }

        /// <summary>
        /// アーカイブディレクトリを読み込む関数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new()
            {
                Description = Localize.FbdArchiveOpenTitle,
                RootFolder = Environment.SpecialFolder.Desktop,
                SelectedPath = @"",
                ShowNewFolderButton = true
            };

            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                DirectoryInfo di = new(fbd.SelectedPath);
                long directorysize = GetDirectorySize(di);
                string name = Path.GetFileName(fbd.SelectedPath);
                ListViewItem lvi;

                switch (name)
                {
                    case "YUMFILE_1":
                        if (!File.Exists(GetLastDirectory(fbd.SelectedPath) + name + ".index"))
                        {
                            MessageBox.Show(this, string.Format(Localize.UnexpectedIndexNotFound, name), Localize.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            WriteLog(string.Format(Localize.ErrorIndex, fbd.SelectedPath, name));
                            break;
                        }
                        else
                        {
                            if (CheckIndexFile(GetLastDirectory(fbd.SelectedPath) + name + ".index") != true)
                            {
                                MessageBox.Show(this, string.Format(Localize.UnexpectedIndexCorrupted, GetLastDirectory(fbd.SelectedPath) + name + ".index"), Localize.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                            if (Directory.GetFiles(fbd.SelectedPath, "*", SearchOption.AllDirectories).Length != yum1_progress_max - 2)
                            {
                                MessageBox.Show(this, Localize.UnexpectedFileCountMismatch, Localize.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                            else
                            {
                                listView1.Items.Clear();
                                lvi = listView1.Items.Add(name);
                                lvi.SubItems.Add(string.Format(Localize.FileSizeText, directorysize));
                                lvi.SubItems.Add(Localize.ArchiveTypePS_Directory);
                                pictureBox2.Image = Properties.Resources.logo_ps;
                                pictureBox2.Refresh();
                                button1.Enabled = false;
                                button2.Enabled = true;
                                yumflag = 0;
                                current_yum = "YUMFILE_1";
                                current_filepath = fbd.SelectedPath;
                                WriteLog(string.Format(Localize.ReadedArchiveFolder, fbd.SelectedPath, string.Format(Localize.FileSizeText, directorysize), yum1_progress_max - 2, Localize.ArchiveTypePS_Directory));
                                break;
                            }
                        }
                    case "YUMFILE_2":
                        if (!File.Exists(GetLastDirectory(fbd.SelectedPath) + name + ".index"))
                        {
                            MessageBox.Show(this, string.Format(Localize.UnexpectedIndexNotFound, name), Localize.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            WriteLog(string.Format(Localize.ErrorIndex, fbd.SelectedPath, name));
                            break;
                        }
                        else
                        {
                            if (CheckIndexFile(GetLastDirectory(fbd.SelectedPath) + name + ".index") != true)
                            {
                                MessageBox.Show(this, string.Format(Localize.UnexpectedIndexCorrupted, GetLastDirectory(fbd.SelectedPath) + name + ".index"), Localize.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                            if (Directory.GetFiles(fbd.SelectedPath, "*", SearchOption.AllDirectories).Length != yum2_progress_max - 2)
                            {
                                MessageBox.Show(this, Localize.UnexpectedFileCountMismatch, Localize.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                            else
                            {
                                listView1.Items.Clear();
                                lvi = listView1.Items.Add(name);
                                lvi.SubItems.Add(string.Format(Localize.FileSizeText, directorysize));
                                lvi.SubItems.Add(Localize.ArchiveTypeWS_Directory);
                                pictureBox2.Image = Properties.Resources.logo_ws;
                                pictureBox2.Refresh();
                                button1.Enabled = false;
                                button2.Enabled = true;
                                yumflag = 1;
                                current_yum = "YUMFILE_2";
                                current_filepath = fbd.SelectedPath;
                                WriteLog(string.Format(Localize.ReadedArchiveFolder, fbd.SelectedPath, string.Format(Localize.FileSizeText, directorysize), yum2_progress_max - 2, Localize.ArchiveTypeWS_Directory));
                                break;
                            }
                        }
                    case "YUMFILE_3":
                        if (!File.Exists(GetLastDirectory(fbd.SelectedPath) + name + ".index"))
                        {
                            MessageBox.Show(this, string.Format(Localize.UnexpectedIndexNotFound, name), Localize.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            WriteLog(string.Format(Localize.ErrorIndex, fbd.SelectedPath, name));
                            break;
                        }
                        else
                        {
                            if (CheckIndexFile(GetLastDirectory(fbd.SelectedPath) + name + ".index") != true)
                            {
                                MessageBox.Show(this, string.Format(Localize.UnexpectedIndexCorrupted, GetLastDirectory(fbd.SelectedPath) + name + ".index"), Localize.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                            if (Directory.GetFiles(fbd.SelectedPath, "*", SearchOption.AllDirectories).Length != yum3_progress_max - 2)
                            {
                                MessageBox.Show(this, Localize.UnexpectedFileCountMismatch, Localize.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                            else
                            {
                                listView1.Items.Clear();
                                lvi = listView1.Items.Add(name);
                                lvi.SubItems.Add(string.Format(Localize.FileSizeText, directorysize));
                                lvi.SubItems.Add(Localize.ArchiveTypeMM_Directory);
                                pictureBox2.Image = Properties.Resources.logo_mm;
                                pictureBox2.Refresh();
                                button1.Enabled = false;
                                button2.Enabled = true;
                                yumflag = 2;
                                current_yum = "YUMFILE_3";
                                current_filepath = fbd.SelectedPath;
                                WriteLog(string.Format(Localize.ReadedArchiveFolder, fbd.SelectedPath, string.Format(Localize.FileSizeText, directorysize), yum3_progress_max - 2, Localize.ArchiveTypeMM_Directory));
                                break;
                            }
                        }
                    default:
                        MessageBox.Show(this, Localize.UnexpectedNotArchiveDirectory, Localize.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        WriteLog(string.Format(Localize.ErrorArchiveFolder, fbd.SelectedPath));
                        listView1.Items.Clear();
                        pictureBox2.Image = Properties.Resources.logo_not;
                        pictureBox2.Refresh();
                        button1.Enabled = false;
                        button2.Enabled = false;
                        yumflag = -1;
                        current_yum = null;
                        current_filepath = null;
                        break;
                }
                return;
            }
            else
            {
                listView1.Items.Clear();
                pictureBox2.Image = Properties.Resources.logo_not;
                pictureBox2.Refresh();
                button1.Enabled = false;
                button2.Enabled = false;
                yumflag = -1;
                current_yum = null;
                current_filepath = null;
                return;
            }
        }

        /// <summary>
        /// 開いたアーカイブファイル、ディレクトリを閉じる関数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            button1.Enabled = false;
            button2.Enabled = false;
            current_filepath = null;
            return;
        }

        /// <summary>
        /// アプリケーション終了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem8_Click(object sender, EventArgs e)
        {
            Close();
            return;
        }

        /// <summary>
        /// 設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem9_Click(object sender, EventArgs e)
        {
            FormSettings form = new();
            form.ShowDialog();
            form.Dispose();
            return;
        }

        /// <summary>
        /// About
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem10_Click(object sender, EventArgs e)
        {
            FormAbout form = new();
            form.ShowDialog();
            form.Dispose();
            return;
        }

        /// <summary>
        /// 更新の確認
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem12_Click(object sender, EventArgs e)
        {
#pragma warning disable SYSLIB0014 // 型またはメンバーが旧型式です
            WebClient wc = new();
#pragma warning restore SYSLIB0014 // 型またはメンバーが旧型式です
            Stream st;
            StreamReader sr;
            try
            {
                string netversion;

                st = wc.OpenRead("https://raw.githubusercontent.com/XyLe-GBP/YUMTool_Reloaded/master/VERSIONINFO");
                sr = new(st);
                netversion = sr.ReadToEnd();

                sr.Close();
                st.Close();

                FileVersionInfo ver = FileVersionInfo.GetVersionInfo(Application.ExecutablePath);

                switch (ver.FileVersion.ToString().CompareTo(netversion[8..].Replace("\n", "")))
                {
                    case -1:
                        DialogResult dr = MessageBox.Show(this, string.Format(Localize.LatestVersion, netversion[8..].Replace("\n", "") + "\n") + string.Format(Localize.CurrentVersion, ver.FileVersion + "\n") + Localize.UpdateConfirm, Localize.ConfirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dr == DialogResult.Yes)
                        {
                            Process.Start("https://github.com/XyLe-GBP/YUMTool_Reloaded");
                            return;
                        }
                        else
                        {
                            return;
                        }
                    case 0:
                        MessageBox.Show(this, string.Format(Localize.LatestVersion, netversion[8..].Replace("\n", "") + "\n") + string.Format(Localize.CurrentVersion, ver.FileVersion + "\n") + Localize.Latested, Localize.SuccessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case 1:
                        throw new Exception(netversion[8..].Replace("\n", "").ToString() + " < " + ver.FileVersion.ToString());
                }
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, Localize.UnexpectedError + "\r\n\r\n" + ex.ToString(), Localize.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        /// <summary>
        /// アーカイブ展開を行うメイン関数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1_Click(object sender, EventArgs e)
        {
            if (current_filepath == null)
            {
                return;
            }

            if (current_yum == null)
            {
                return;
            }

            FolderBrowserDialog fbd = new()
            {
                Description = Localize.FbdArchiveSaveTitle,
                RootFolder = Environment.SpecialFolder.Desktop,
                SelectedPath = @"",
                ShowNewFolderButton = true
            };
            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                current_savepath = fbd.SelectedPath;
                if (Directory.GetFiles(fbd.SelectedPath, "*", SearchOption.AllDirectories).Length != 0)
                {
                    DialogResult dr = MessageBox.Show(this, Localize.WarningExist, Localize.WarningTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == DialogResult.Yes)
                    {
                        FormDelete form = new();
                        form.ShowDialog();
                        form.Dispose();
                        Directory.CreateDirectory(fbd.SelectedPath);
                    }
                    else
                    {
                        return;
                    }           
                }

                WriteLog(Localize.DecompressProgress);
                Enabled = false;
                toolStripProgressBar1.Enabled = true;
                toolStripStatusLabel1.Enabled = true;

                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\tmp");

                byte[] lzss = Properties.Resources.app_lzss;
                byte[] lzss1 = Properties.Resources.app_lzss1;
                byte[] plfile1 = Properties.Resources.app_utils;
                byte[] plfile2 = Properties.Resources.app_string;
                File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\tmp\\lzss.exe", lzss);
                File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\tmp\\lzss.plx", lzss1);
                File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\tmp\\utils.plx", plfile1);
                File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\tmp\\string.plx", plfile2);

                File.Copy(current_filepath, fbd.SelectedPath + "\\" + current_yum);
                File.Move(Directory.GetCurrentDirectory() + "\\tmp\\lzss.exe", Directory.GetCurrentDirectory() + "\\lzss.exe");
                while (!File.Exists(fbd.SelectedPath + "\\" + current_yum))
                {
                    toolStripStatusLabel1.Text = string.Format(Localize.MoveText, current_yum);
                }
                Decompressed_RefleshProgress();
                backgroundWorker1.RunWorkerAsync();

                while (backgroundWorker1.IsBusy)
                {
                    Application.DoEvents();
                    if (workerflag == true)
                    {
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
                workerflag = false;

                if (decompress_ok != true)
                {
                    MessageBox.Show(this, string.Format(Localize.ErrorDecompressed, current_yum), Localize.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(this, string.Format(Localize.SuccessDecompressed, Directory.GetFiles(current_savepath, "*", SearchOption.AllDirectories).Length), Localize.SuccessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    switch (yumflag)
                    {
                        case 0:
                            WriteLog(string.Format(Localize.VerifyCount, yum1_progress_max - 1));
                            break;
                        case 1:
                            WriteLog(string.Format(Localize.VerifyCount, yum2_progress_max - 1));
                            break;
                        case 2:
                            WriteLog(string.Format(Localize.VerifyCount, yum3_progress_max - 1));
                            break;
                        default:
                            break;
                    }
                    WriteLog(Localize.SuccessProgress);
                }
                File.Move(Directory.GetCurrentDirectory() + "\\lzss.exe", Directory.GetCurrentDirectory() + "\\tmp\\lzss.exe");
                File.Delete(fbd.SelectedPath + "\\" + current_yum);
                ResetALL();
                Enabled = true;
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// アーカイブ再構築を行うメイン関数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new()
            {
                FileName = current_yum + ".BIN",
                InitialDirectory = "",
                FilterIndex = 1,
                Title = Localize.SaveFileDialogArchive,
                RestoreDirectory = true
            };
            switch (yumflag)
            {
                case 0:
                    sfd.Filter = Localize.FilterPS;
                    break;
                case 1:
                    sfd.Filter = Localize.FilterWS;
                    break;
                case 2:
                    sfd.Filter = Localize.FilterMM;
                    break;
                default:
                    return;
            }
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                WriteLog(Localize.RecompressProgress);
                Enabled = false;
                current_savepath = sfd.FileName;
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\tmp");

                byte[] lzss = Properties.Resources.app_lzss;
                byte[] lzss1 = Properties.Resources.app_lzss1;
                byte[] plfile1 = Properties.Resources.app_utils;
                byte[] plfile2 = Properties.Resources.app_string;
                File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\tmp\\lzss.exe", lzss);
                File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\tmp\\lzss.plx", lzss1);
                File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\tmp\\utils.plx", plfile1);
                File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\tmp\\string.plx", plfile2);

                File.Move(Directory.GetCurrentDirectory() + "\\tmp\\lzss.exe", Directory.GetCurrentDirectory() + "\\lzss.exe");

                Recompressed_RefleshProgress();
                backgroundWorker2.RunWorkerAsync();

                while (backgroundWorker2.IsBusy)
                {
                    Application.DoEvents();
                }
                backgroundWorker2.Dispose();
                workerflag = false;
                if (!File.Exists(GetLastDirectory(current_filepath) + "\\" + current_yum + ".bin"))
                {
                    MessageBox.Show(this, string.Format(Localize.ErrorRecompressed, current_yum), Localize.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    File.Move(Directory.GetCurrentDirectory() + "\\lzss.exe", Directory.GetCurrentDirectory() + "\\tmp\\lzss.exe");
                    ResetALL();
                    return;
                }
                else
                {
                    File.Move(GetLastDirectory(current_filepath) + "\\" + current_yum + ".bin", current_savepath);
                }

                FileInfo file = new(current_savepath);
                if (recompress_ok != true)
                {
                    MessageBox.Show(this, string.Format(Localize.ErrorRecompressed, current_yum), Localize.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(this, string.Format(Localize.SuccessRecompressed, file.Length), Localize.SuccessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    switch (yumflag)
                    {
                        case 0:
                            WriteLog(string.Format(Localize.VerifySize, yum1_repack_size));
                            break;
                        case 1:
                            WriteLog(string.Format(Localize.VerifySize, yum2_repack_size));
                            break;
                        case 2:
                            WriteLog(string.Format(Localize.VerifySize, yum3_repack_size));
                            break;
                        default:
                            break;
                    }
                    WriteLog(Localize.SuccessProgress);
                }
                File.Move(Directory.GetCurrentDirectory() + "\\lzss.exe", Directory.GetCurrentDirectory() + "\\tmp\\lzss.exe");
                ResetALL();
                Enabled = true;
            }
            else
            {
                return;
            }
        }

        private void ListView1_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = listView1.Columns[e.ColumnIndex].Width;
        }

        /// <summary>
        /// アーカイブ展開時の進捗状況を知らせるワーカースレッド関数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (workerflag != false)
            {
                return;
            }
            BackgroundWorker worker = (BackgroundWorker)sender;
            backgroundWorker_Decompress.RunWorkerAsync();

            switch (yumflag)
            {
                case 0:
                    while (backgroundWorker_Decompress.IsBusy)
                    {
                        if (Directory.GetFiles(current_savepath, "*", SearchOption.AllDirectories).Length == yum1_progress_max)
                        {
                            worker.ReportProgress(Directory.GetFiles(current_savepath, "*", SearchOption.AllDirectories).Length);
                            break;
                        }
                        else
                        {
                            worker.ReportProgress(Directory.GetFiles(current_savepath, "*", SearchOption.AllDirectories).Length);
                            continue;
                        }
                    }
                    break;
                case 1:
                    while (backgroundWorker_Decompress.IsBusy)
                    {
                        if (Directory.GetFiles(current_savepath, "*", SearchOption.AllDirectories).Length == yum2_progress_max)
                        {
                            worker.ReportProgress(Directory.GetFiles(current_savepath, "*", SearchOption.AllDirectories).Length);
                            break;
                        }
                        else
                        {
                            worker.ReportProgress(Directory.GetFiles(current_savepath, "*", SearchOption.AllDirectories).Length);
                            continue;
                        }
                    }
                    break;
                case 2:
                    while (backgroundWorker_Decompress.IsBusy)
                    {
                        if (Directory.GetFiles(current_savepath, "*", SearchOption.AllDirectories).Length == yum3_progress_max)
                        {
                            worker.ReportProgress(Directory.GetFiles(current_savepath, "*", SearchOption.AllDirectories).Length);
                            break;
                        }
                        else
                        {
                            worker.ReportProgress(Directory.GetFiles(current_savepath, "*", SearchOption.AllDirectories).Length);
                            continue;
                        }
                    }
                    break;
                default:
                    break;
            }
            workerflag = true;
        }

        private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBar1.Value = e.ProgressPercentage;
            switch (yumflag)
            {
                case 0:
                    toolStripStatusLabel1.Text = string.Format(Localize.ProgressDecompressed, current_yum, e.ProgressPercentage.ToString(), yum1_progress_max.ToString());
                    break;
                case 1:
                    toolStripStatusLabel1.Text = string.Format(Localize.ProgressDecompressed, current_yum, e.ProgressPercentage.ToString(), yum2_progress_max.ToString());
                    break;
                case 2:
                    toolStripStatusLabel1.Text = string.Format(Localize.ProgressDecompressed, current_yum, e.ProgressPercentage.ToString(), yum3_progress_max.ToString());
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// アーカイブ展開を行うメイン・ワーカースレッド関数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackgroundWorker_Decompress_DoWork(object sender, DoWorkEventArgs e)
        {
            ProcessStartInfo pInfo = new();
            Process process;
            switch (yumflag)
            {
                case 0:
                    pInfo.FileName = "perl";
                    pInfo.Arguments = Directory.GetCurrentDirectory() + "\\tmp\\lzss.plx " + current_savepath + "\\" + current_yum;
                    pInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    pInfo.UseShellExecute = true;
                    process = Process.Start(pInfo);
                    process.WaitForExit();
                    if (Directory.GetFiles(current_savepath, "*", SearchOption.AllDirectories).Length == yum1_progress_max)
                    {
                        decompress_ok = true;
                    }
                    else
                    {
                        decompress_ok = false;
                    }
                    break;
                case 1:
                    pInfo.FileName = "perl";
                    pInfo.Arguments = Directory.GetCurrentDirectory() + "\\tmp\\lzss.plx " + current_savepath + "\\" + current_yum;
                    pInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    pInfo.UseShellExecute = true;
                    process = Process.Start(pInfo);
                    process.WaitForExit();
                    if (Directory.GetFiles(current_savepath, "*", SearchOption.AllDirectories).Length == yum2_progress_max)
                    {
                        decompress_ok = true;
                    }
                    else
                    {
                        decompress_ok = false;
                    }
                    break;
                case 2:
                    pInfo.FileName = "perl";
                    pInfo.Arguments = Directory.GetCurrentDirectory() + "\\tmp\\lzss.plx " + current_savepath + "\\" + current_yum;
                    pInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    pInfo.UseShellExecute = true;
                    process = Process.Start(pInfo);
                    process.WaitForExit();
                    if (Directory.GetFiles(current_savepath, "*", SearchOption.AllDirectories).Length == yum3_progress_max)
                    {
                        decompress_ok = true;
                    }
                    else
                    {
                        decompress_ok = false;
                    }
                    break;
                default:
                    break;
            }
            return;
        }

        private void Decompressed_RefleshProgress()
        {
            if (current_savepath != null)
            {
                toolStripProgressBar1.Minimum = 0;
                switch (yumflag)
                {
                    case 0:
                        toolStripProgressBar1.Maximum = (int)yum1_progress_max;
                        break;
                    case 1:
                        toolStripProgressBar1.Maximum = (int)yum2_progress_max;
                        break;
                    case 2:
                        toolStripProgressBar1.Maximum = (int)yum3_progress_max;
                        break;
                    default:
                        break;
                }
                toolStripProgressBar1.Value = 0;
                toolStripStatusLabel1.Text = Localize.ProgressInitialize;
            }
        }

        /// <summary>
        /// アーカイブ再構築時の進捗状況を知らせるワーカースレッド関数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            if (workerflag != false)
            {
                return;
            }
            BackgroundWorker worker = (BackgroundWorker)sender;
            backgroundWorker_Recompress.RunWorkerAsync();

            string Dir = GetLastDirectory(current_filepath);
            while (!File.Exists(Dir + current_yum + ".bin"))
            {
                if (File.Exists(Dir + current_yum + ".bin"))
                {
                    break;
                }
                else
                {
                    continue;
                }
            }

            switch (yumflag)
            {
                case 0:
                    while (backgroundWorker_Recompress.IsBusy)
                    {
                        if (GetFileSize(Dir + current_yum + ".bin") == yum1_repack_size || GetFileSize(Dir + current_yum + ".bin") == yum1_repack_nc_size)
                        {
                            worker.ReportProgress((int)GetFileSize(Dir + current_yum + ".bin"));
                            break;
                        }
                        else
                        {
                            worker.ReportProgress((int)GetFileSize(Dir + current_yum + ".bin"));
                            continue;
                        }
                    }
                    break;
                case 1:
                    while (backgroundWorker_Recompress.IsBusy)
                    {
                        if (GetFileSize(Dir + current_yum + ".bin") == yum2_repack_size || GetFileSize(Dir + current_yum + ".bin") == yum2_repack_nc_size)
                        {
                            worker.ReportProgress((int)GetFileSize(Dir + current_yum + ".bin"));
                            break;
                        }
                        else
                        {
                            worker.ReportProgress((int)GetFileSize(Dir + current_yum + ".bin"));
                            continue;
                        }
                    }
                    break;
                case 2:
                    long size = GetFileSize(Dir + current_yum + ".bin");
                    while (backgroundWorker_Recompress.IsBusy)
                    {
                        if (GetFileSize(Dir + current_yum + ".bin") == yum3_repack_size || GetFileSize(Dir + current_yum + ".bin") == yum3_repack_nc_size)
                        {
                            worker.ReportProgress((int)GetFileSize(Dir + current_yum + ".bin"));
                            break;
                        }
                        else
                        {
                            worker.ReportProgress((int)GetFileSize(Dir + current_yum + ".bin"));
                            continue;
                        }
                    }
                    break;
                default:
                    break;
            }
            workerflag = true;
        }

        private void BackgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBar1.Value = e.ProgressPercentage;
            switch (yumflag)
            {
                case 0:
                    toolStripStatusLabel1.Text = string.Format(Localize.ProgressRecompressed, current_yum, e.ProgressPercentage.ToString(), yum1_repack_size.ToString());
                    break;
                case 1:
                    toolStripStatusLabel1.Text = string.Format(Localize.ProgressRecompressed, current_yum, e.ProgressPercentage.ToString(), yum2_repack_size.ToString());
                    break;
                case 2:
                    toolStripStatusLabel1.Text = string.Format(Localize.ProgressRecompressed, current_yum, e.ProgressPercentage.ToString(), yum3_repack_size.ToString());
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// アーカイブ再構築を行うメイン・ワーカースレッド関数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackgroundWorker_Recompress_DoWork(object sender, DoWorkEventArgs e)
        {
            var ini = new IniFile(@".\\settings.ini");
            int check1, check2;
            string tbox, recomp_offset;

            check1 = ini.GetInt("SETTINGS", "0x1000", 0xFFFF);
            check2 = ini.GetInt("SETTINGS", "0x1001", 0xFFFF);
            tbox = ini.GetString("SETTINGS", "0x0000", null);

            switch (check1)
            {
                case 0:
                    recomp_check_1 = false;
                    break;
                case 1:
                    recomp_check_1 = true;
                    break;
                default:
                    recomp_check_1 = false;
                    break;
            }

            switch (check2)
            {
                case 0:
                    recomp_check_2 = false;
                    break;
                case 1:
                    recomp_check_2 = true;
                    break;
                default:
                    recomp_check_2 = false;
                    break;
            }

            if (tbox != null)
            {
                recomp_offset = tbox;
            }
            else
            {
                recomp_offset = null;
            }

            ProcessStartInfo pInfo = new();
            Process process;
            string Dir = GetLastDirectory(current_filepath);

            switch (yumflag)
            {
                case 0:
                    pInfo.FileName = "perl";
                    if (recomp_check_1 != false || recomp_check_2 != false)
                    {
                        if (recomp_check_1 != false)
                        {
                            pInfo.Arguments = Directory.GetCurrentDirectory() + "\\tmp\\lzss.plx -p TYPE=" + recomp_offset + " " + current_filepath;
                        }
                        else if (recomp_check_2 != false)
                        {
                            pInfo.Arguments = Directory.GetCurrentDirectory() + "\\tmp\\lzss.plx -n " + current_filepath;
                        }
                        else
                        {
                            pInfo.Arguments = Directory.GetCurrentDirectory() + "\\tmp\\lzss.plx " + current_filepath;
                        }
                    }
                    else
                    {
                        pInfo.Arguments = Directory.GetCurrentDirectory() + "\\tmp\\lzss.plx " + current_filepath;
                    }
                    pInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    pInfo.UseShellExecute = true;
                    process = Process.Start(pInfo);
                    process.WaitForExit();
                    if (recomp_check_1 != false || recomp_check_2 != false)
                    {
                        if (recomp_check_1 != false)
                        {
                            if (GetFileSize(Dir + current_yum + ".bin") == yum1_repack_size)
                            {
                                recompress_ok = true;
                            }
                            else
                            {
                                recompress_ok = false;
                            }
                        }
                        else if (recomp_check_2 != false)
                        {
                            if (GetFileSize(Dir + current_yum + ".bin") == yum1_repack_nc_size)
                            {
                                recompress_ok = true;
                            }
                            else
                            {
                                recompress_ok = false;
                            }
                        }
                        else
                        {
                            if (GetFileSize(Dir + current_yum + ".bin") == yum1_repack_size)
                            {
                                recompress_ok = true;
                            }
                            else
                            {
                                recompress_ok = false;
                            }
                        }
                    }
                    else
                    {
                        if (GetFileSize(Dir + current_yum + ".bin") == yum1_repack_size)
                        {
                            recompress_ok = true;
                        }
                        else
                        {
                            recompress_ok = false;
                        }
                    }
                    break;
                case 1:
                    pInfo.FileName = "perl";
                    if (recomp_check_1 != false || recomp_check_2 != false)
                    {
                        if (recomp_check_1 != false)
                        {
                            pInfo.Arguments = Directory.GetCurrentDirectory() + "\\tmp\\lzss.plx -p TYPE=" + recomp_offset + " " + current_filepath;
                        }
                        else if (recomp_check_2 != false)
                        {
                            pInfo.Arguments = Directory.GetCurrentDirectory() + "\\tmp\\lzss.plx -n " + current_filepath;
                        }
                        else
                        {
                            pInfo.Arguments = Directory.GetCurrentDirectory() + "\\tmp\\lzss.plx " + current_filepath;
                        }
                    }
                    else
                    {
                        pInfo.Arguments = Directory.GetCurrentDirectory() + "\\tmp\\lzss.plx " + current_filepath;
                    }
                    pInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    pInfo.UseShellExecute = true;
                    process = Process.Start(pInfo);
                    process.WaitForExit();
                    if (recomp_check_1 != false || recomp_check_2 != false)
                    {
                        if (recomp_check_1 != false)
                        {
                            if (GetFileSize(Dir + current_yum + ".bin") == yum2_repack_size)
                            {
                                recompress_ok = true;
                            }
                            else
                            {
                                recompress_ok = false;
                            }
                        }
                        else if (recomp_check_2 != false)
                        {
                            if (GetFileSize(Dir + current_yum + ".bin") == yum2_repack_nc_size)
                            {
                                recompress_ok = true;
                            }
                            else
                            {
                                recompress_ok = false;
                            }
                        }
                        else
                        {
                            if (GetFileSize(Dir + current_yum + ".bin") == yum2_repack_size)
                            {
                                recompress_ok = true;
                            }
                            else
                            {
                                recompress_ok = false;
                            }
                        }
                    }
                    else
                    {
                        if (GetFileSize(Dir + current_yum + ".bin") == yum2_repack_size)
                        {
                            recompress_ok = true;
                        }
                        else
                        {
                            recompress_ok = false;
                        }
                    }
                    break;
                case 2:
                    pInfo.FileName = "perl";
                    if (recomp_check_1 != false || recomp_check_2 != false)
                    {
                        if (recomp_check_1 != false)
                        {
                            pInfo.Arguments = Directory.GetCurrentDirectory() + "\\tmp\\lzss.plx -p TYPE=" + recomp_offset + " " + current_filepath;
                        }
                        else if (recomp_check_2 != false)
                        {
                            pInfo.Arguments = Directory.GetCurrentDirectory() + "\\tmp\\lzss.plx -n " + current_filepath;
                        }
                        else
                        {
                            pInfo.Arguments = Directory.GetCurrentDirectory() + "\\tmp\\lzss.plx " + current_filepath;
                        }
                    }
                    else
                    {
                        pInfo.Arguments = Directory.GetCurrentDirectory() + "\\tmp\\lzss.plx " + current_filepath;
                    }
                    pInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    pInfo.UseShellExecute = true;
                    process = Process.Start(pInfo);
                    process.WaitForExit();
                    if (recomp_check_1 != false || recomp_check_2 != false)
                    {
                        if (recomp_check_1 != false)
                        {
                            if (GetFileSize(Dir + current_yum + ".bin") == yum3_repack_size)
                            {
                                recompress_ok = true;
                            }
                            else
                            {
                                recompress_ok = false;
                            }
                        }
                        else if (recomp_check_2 != false)
                        {
                            if (GetFileSize(Dir + current_yum + ".bin") == yum3_repack_nc_size)
                            {
                                recompress_ok = true;
                            }
                            else
                            {
                                recompress_ok = false;
                            }
                        }
                        else
                        {
                            if (GetFileSize(Dir + current_yum + ".bin") == yum3_repack_size)
                            {
                                recompress_ok = true;
                            }
                            else
                            {
                                recompress_ok = false;
                            }
                        }
                    }
                    else
                    {
                        if (GetFileSize(Dir + current_yum + ".bin") == yum3_repack_size)
                        {
                            recompress_ok = true;
                        }
                        else
                        {
                            recompress_ok = false;
                        }
                    }
                    break;
                default:
                    break;
            }
            return;
        }

        private void Recompressed_RefleshProgress()
        {
            var ini = new IniFile(@".\\settings.ini");
            int check1, check2;

            check1 = ini.GetInt("SETTINGS", "0x1000", 0xFFFF);
            check2 = ini.GetInt("SETTINGS", "0x1001", 0xFFFF);

            switch (check1)
            {
                case 0:
                    recomp_check_1 = false;
                    break;
                case 1:
                    recomp_check_1 = true;
                    break;
                default:
                    recomp_check_1 = false;
                    break;
            }

            switch (check2)
            {
                case 0:
                    recomp_check_2 = false;
                    break;
                case 1:
                    recomp_check_2 = true;
                    break;
                default:
                    recomp_check_2 = false;
                    break;
            }

            if (current_filepath != null)
            {
                toolStripProgressBar1.Minimum = 0;
                switch (yumflag)
                {
                    case 0:
                        if (recomp_check_1 != false || recomp_check_2 != false)
                        {
                            if (recomp_check_1 != false)
                            {
                                toolStripProgressBar1.Maximum = (int)yum1_repack_size;
                            }
                            else if (recomp_check_2 != false)
                            {
                                toolStripProgressBar1.Maximum = (int)yum1_repack_nc_size;
                            }
                            else
                            {
                                toolStripProgressBar1.Maximum = (int)yum1_repack_size;
                            }
                        }
                        else
                        {
                            toolStripProgressBar1.Maximum = (int)yum1_repack_size;
                        }
                        break;
                    case 1:
                        if (recomp_check_1 != false || recomp_check_2 != false)
                        {
                            if (recomp_check_1 != false)
                            {
                                toolStripProgressBar1.Maximum = (int)yum2_repack_size;
                            }
                            else if (recomp_check_2 != false)
                            {
                                toolStripProgressBar1.Maximum = (int)yum2_repack_nc_size;
                            }
                            else
                            {
                                toolStripProgressBar1.Maximum = (int)yum2_repack_size;
                            }
                        }
                        else
                        {
                            toolStripProgressBar1.Maximum = (int)yum2_repack_size;
                        }
                        break;
                    case 2:
                        if (recomp_check_1 != false || recomp_check_2 != false)
                        {
                            if (recomp_check_1 != false)
                            {
                                toolStripProgressBar1.Maximum = (int)yum3_repack_size;
                            }
                            else if (recomp_check_2 != false)
                            {
                                toolStripProgressBar1.Maximum = (int)yum3_repack_nc_size;
                            }
                            else
                            {
                                toolStripProgressBar1.Maximum = (int)yum3_repack_size;
                            }
                        }
                        else
                        {
                            toolStripProgressBar1.Maximum = (int)yum3_repack_size;
                        }
                        break;
                    default:
                        break;
                }
                toolStripProgressBar1.Value = 0;
                toolStripStatusLabel1.Text = Localize.ProgressInitialize;
            }
        }

        public void ResetALL()
        {
            listView1.Items.Clear();
            pictureBox2.Image = Properties.Resources.logo_not;
            pictureBox2.Refresh();
            decompress_ok = false;
            recompress_ok = false;
            button1.Enabled = false;
            button2.Enabled = false;
            current_filepath = null;
            current_savepath = null;
            current_yum = null;
            yumflag = -1;
            toolStripProgressBar1.Value = 0;
            toolStripStatusLabel1.Text = Localize.StatusReady;
            DeleteAll(Directory.GetCurrentDirectory() + "\\tmp");
        }

        public void WriteLog(string logText)
        {
            _ = SendMessage(textBox1.Handle, EM_REPLACESEL, 1, "[" + DateTime.Now.ToString() + "]->" + logText);
            //textBox1.AppendText("[" + DateTime.Now.ToString() + "]->" + logText);
        }

        public void ClearLog()
        {
            textBox1.Clear();
        }

        
    }
}
