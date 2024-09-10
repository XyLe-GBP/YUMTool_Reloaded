using PrivateProfile;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static YUMTool_R.Common;
using static YUMTool_R.Utils;

namespace YUMTool_R
{
    public partial class FormProgress : Form
    {
        public FormProgress()
        {
            InitializeComponent();
        }

        private void FormProgress_Load(object sender, EventArgs e)
        {
            switch (ProcessFlag)
            {
                case 0:
                    Text = "Decompressing...";
                    break;
                case 1:
                    Text = "Recompressing...";
                    break;
                default:
                    break;
            }
            timer_interval.Interval = 3000;
            timer_Abort.Interval = 1000;
            progressBar_MainProgress.Value = 0;
            progressBar_MainProgress.Minimum = 0;
            progressBar_MainProgress.Maximum = Common.ProgressMax;
            RunTask();
        }

        private async void RunTask()
        {
            switch (Common.ProcessFlag)
            {
                case 0: // Decompress
                    {
                        label_log.Text = string.Format(Localize.DecompressInfo, current_yum);
                        Common.cts = new CancellationTokenSource();
                        var cToken = Common.cts.Token;
                        var p = new Progress<int>(UpdateProgress);

                        timer_Abort.Enabled = true;
                        Common.Result = await Task.Run(() => Decomp_DoWork(p, cToken));
                        break;
                    }
                case 1: // Compress
                    {
                        label_log.Text = string.Format(Localize.RecompressInfo, current_yum);
                        Common.cts = new CancellationTokenSource();
                        var cToken = Common.cts.Token;
                        var p = new Progress<int>(UpdateProgress);

                        timer_Abort.Enabled = true;
                        Result = await Task.Run(() => Recomp_DoWork(p, cToken));
                        break;
                    }
                default:
                    {
                        Close();
                        break;
                    }
            }
            timer_interval.Enabled = true;
        }

        private static bool Decomp_DoWork(IProgress<int> p, CancellationToken cToken)
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
                    //pInfo.UseShellExecute = false;
                    //pInfo.RedirectStandardOutput = true;
                    pInfo.CreateNoWindow = true;
                    process = Process.Start(pInfo);

                    if (process is null) { return false; }
                    //pLog = process.StandardOutput;
                    while (!process.HasExited)
                    {
                        int files = Directory.GetFiles(current_savepath, "*", SearchOption.AllDirectories).Length;

                        if (cToken.IsCancellationRequested == true)
                        {
                            IsCancelled = true;

                            if (!process.HasExited)
                            {
                                process.Kill();
                            }
                            
                            process.CloseMainWindow();
                            process.Dispose();

                            return false;
                        }
                        p.Report(files);
                        continue;
                    }

                    if (Directory.GetFiles(current_savepath, "*", SearchOption.AllDirectories).Length == yum1_progress_max)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 1:
                    pInfo.FileName = "perl";
                    pInfo.Arguments = Directory.GetCurrentDirectory() + "\\tmp\\lzss.plx " + current_savepath + "\\" + current_yum;
                    pInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    pInfo.UseShellExecute = true;
                    //pInfo.UseShellExecute = false;
                    //pInfo.RedirectStandardOutput = true;
                    pInfo.CreateNoWindow = true;
                    process = Process.Start(pInfo);

                    if (process is null) { return false; }
                    //pLog = process.StandardOutput;
                    while (!process.HasExited)
                    {
                        int files = Directory.GetFiles(current_savepath, "*", SearchOption.AllDirectories).Length;

                        if (cToken.IsCancellationRequested == true)
                        {
                            IsCancelled = true;

                            if (!process.HasExited)
                            {
                                process.Kill();
                            }

                            process.CloseMainWindow();
                            process.Dispose();

                            return false;
                        }
                        p.Report(files);
                        continue;
                    }

                    if (Directory.GetFiles(current_savepath, "*", SearchOption.AllDirectories).Length == yum2_progress_max)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 2:
                    pInfo.FileName = "perl";
                    pInfo.Arguments = Directory.GetCurrentDirectory() + "\\tmp\\lzss.plx " + current_savepath + "\\" + current_yum;
                    pInfo.UseShellExecute = true;
                    pInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    //pInfo.UseShellExecute = false;
                    //pInfo.RedirectStandardOutput = true;
                    pInfo.CreateNoWindow = true;
                    process = Process.Start(pInfo);

                    if (process is null) { return false; }
                    //pLog = process.StandardOutput;
                    while (!process.HasExited)
                    {
                        int files = Directory.GetFiles(current_savepath, "*", SearchOption.AllDirectories).Length;

                        if (cToken.IsCancellationRequested == true)
                        {
                            IsCancelled = true;

                            if (!process.HasExited)
                            {
                                process.Kill();
                            }

                            process.CloseMainWindow();
                            process.Dispose();

                            return false;
                        }
                        p.Report(files);
                        continue;
                    }

                    if (Directory.GetFiles(current_savepath, "*", SearchOption.AllDirectories).Length == yum3_progress_max)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    return false;
            }
        }

        private static bool Recomp_DoWork(IProgress<int> p, CancellationToken cToken)
        {
            var ini = new IniFile(@".\\settings.ini");
            int check1, check2;
            string tbox, recomp_offset;

            check1 = ini.GetInt("SETTINGS", "0x1000", 0xFFFF);
            check2 = ini.GetInt("SETTINGS", "0x1001", 0xFFFF);
            tbox = ini.GetString("SETTINGS", "0x0000", null);

            recomp_check_1 = check1 switch
            {
                0 => false,
                1 => true,
                _ => false,
            };
            recomp_check_2 = check2 switch
            {
                0 => false,
                1 => true,
                _ => false,
            };
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
                    //pInfo.UseShellExecute = false;
                    //pInfo.RedirectStandardOutput = true;
                    pInfo.CreateNoWindow = true;

                    process = Process.Start(pInfo);

                    if (process is null) { return false; }
                    //pLog = process.StandardOutput;
                    while (!process.HasExited)
                    {
                        int files = (int)GetFileSize(Dir + current_yum + ".bin");// Directory.GetFiles(current_savepath, "*", SearchOption.AllDirectories).Length;

                        if (cToken.IsCancellationRequested == true)
                        {
                            if (!process.HasExited)
                            {
                                process.Kill();
                            }
                            process.Close();

                            IsCancelled = true;
                            return false;
                        }
                        p.Report(files);
                        continue;
                    }

                    if (recomp_check_1 != false || recomp_check_2 != false)
                    {
                        if (recomp_check_1 != false)
                        {
                            if (GetFileSize(Dir + current_yum + ".bin") == yum1_repack_size)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else if (recomp_check_2 != false)
                        {
                            if (GetFileSize(Dir + current_yum + ".bin") == yum1_repack_nc_size)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            if (GetFileSize(Dir + current_yum + ".bin") == yum1_repack_size)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        if (GetFileSize(Dir + current_yum + ".bin") == yum1_repack_size)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
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

                    //pInfo.UseShellExecute = false;
                    //pInfo.RedirectStandardOutput = true;
                    pInfo.CreateNoWindow = true;

                    process = Process.Start(pInfo);

                    if (process is null) { return false; }
                    //pLog = process.StandardOutput;
                    while (!process.HasExited)
                    {
                        int files = (int)GetFileSize(Dir + current_yum + ".bin");// Directory.GetFiles(current_savepath, "*", SearchOption.AllDirectories).Length;

                        if (cToken.IsCancellationRequested == true)
                        {
                            if (!process.HasExited)
                            {
                                process.Kill();
                            }
                            process.Close();

                            IsCancelled = true;
                            return false;
                        }
                        p.Report(files);
                        continue;
                    }

                    if (recomp_check_1 != false || recomp_check_2 != false)
                    {
                        if (recomp_check_1 != false)
                        {
                            if (GetFileSize(Dir + current_yum + ".bin") == yum2_repack_size)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else if (recomp_check_2 != false)
                        {
                            if (GetFileSize(Dir + current_yum + ".bin") == yum2_repack_nc_size)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            if (GetFileSize(Dir + current_yum + ".bin") == yum2_repack_size)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        if (GetFileSize(Dir + current_yum + ".bin") == yum2_repack_size)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
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

                    //pInfo.UseShellExecute = false;
                    //pInfo.RedirectStandardOutput = true;
                    pInfo.CreateNoWindow = true;

                    process = Process.Start(pInfo);

                    if (process is null) { return false; }
                    //pLog = process.StandardOutput;
                    while (!process.HasExited)
                    {
                        int files = (int)GetFileSize(Dir + current_yum + ".bin");// Directory.GetFiles(current_savepath, "*", SearchOption.AllDirectories).Length;

                        if (cToken.IsCancellationRequested == true)
                        {
                            if (!process.HasExited)
                            {
                                process.Kill();
                            }
                            process.Close();

                            IsCancelled = true;
                            return false;
                        }
                        p.Report(files);
                        continue;
                    }

                    if (recomp_check_1 != false || recomp_check_2 != false)
                    {
                        if (recomp_check_1 != false)
                        {
                            if (GetFileSize(Dir + current_yum + ".bin") == yum3_repack_size)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else if (recomp_check_2 != false)
                        {
                            if (GetFileSize(Dir + current_yum + ".bin") == yum3_repack_nc_size)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            if (GetFileSize(Dir + current_yum + ".bin") == yum3_repack_size)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        if (GetFileSize(Dir + current_yum + ".bin") == yum3_repack_size)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                default:
                    return false;
            }
        }

        private void UpdateProgress(int p)
        {
            switch (ProcessFlag)
            {
                case 0:
                    {
                        progressBar_MainProgress.Value = p;
                        switch (yumflag)
                        {
                            case 0:
                                label_progress.Text = string.Format(Localize.ProgressDecompressed, current_yum, p.ToString(), yum1_progress_max.ToString());
                                break;
                            case 1:
                                label_progress.Text = string.Format(Localize.ProgressDecompressed, current_yum, p.ToString(), yum2_progress_max.ToString());
                                break;
                            case 2:
                                label_progress.Text = string.Format(Localize.ProgressDecompressed, current_yum, p.ToString(), yum3_progress_max.ToString());
                                break;
                            default:
                                break;
                        }
                        break;
                    }
                case 1:
                    {
                        var ini = new IniFile(@".\\settings.ini");
                        int check1, check2;
                        string tbox;

                        check1 = ini.GetInt("SETTINGS", "0x1000", 0xFFFF);
                        check2 = ini.GetInt("SETTINGS", "0x1001", 0xFFFF);
                        tbox = ini.GetString("SETTINGS", "0x0000", null);

                        progressBar_MainProgress.Value = p;
                        if (check1 == 0 || check2 == 1)
                        {
                            switch (yumflag)
                            {
                                case 0:
                                    label_progress.Text = string.Format(Localize.ProgressRecompressed, current_yum, p.ToString(), yum1_repack_nc_size.ToString());
                                    break;
                                case 1:
                                    label_progress.Text = string.Format(Localize.ProgressRecompressed, current_yum, p.ToString(), yum2_repack_nc_size.ToString());
                                    break;
                                case 2:
                                    label_progress.Text = string.Format(Localize.ProgressRecompressed, current_yum, p.ToString(), yum3_repack_nc_size.ToString());
                                    break;
                                default:
                                    break;
                            }
                        }

                        else
                        {
                            switch (yumflag)
                            {
                                case 0:
                                    label_progress.Text = string.Format(Localize.ProgressRecompressed, current_yum, p.ToString(), yum1_repack_size.ToString());
                                    break;
                                case 1:
                                    label_progress.Text = string.Format(Localize.ProgressRecompressed, current_yum, p.ToString(), yum2_repack_size.ToString());
                                    break;
                                case 2:
                                    label_progress.Text = string.Format(Localize.ProgressRecompressed, current_yum, p.ToString(), yum3_repack_size.ToString());
                                    break;
                                default:
                                    break;
                            }
                        }
                        
                        break;
                    }
                default:
                    break;
            }
        }

        private void Timer_interval_Tick(object sender, EventArgs e)
        {
            Close();
        }

        private void Button_Abort_Click(object sender, EventArgs e)
        {
            if (cts != null)
            {
                if (MessageBox.Show(Localize.AbortConfirm, Localize.ConfirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    cts.Cancel();
                    Close();
                }
            }
        }

        private void Timer_Abort_Tick(object sender, EventArgs e)
        {
            if (button_Abort.Enabled == false)
            {
                button_Abort.Enabled = true;
            }
        }
    }
}
