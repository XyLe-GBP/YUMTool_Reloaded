using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static YUMTool_R.Common;
using static YUMTool_R.Utils;

namespace YUMTool_R
{
    public partial class FormDelete : Form
    {
        private bool IsDeleted = false;
        public FormDelete()
        {
            InitializeComponent();
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            backgroundWorker_Delete.RunWorkerAsync();
            while (backgroundWorker_Delete.IsBusy)
            {
                if (Directory.Exists(current_savepath))
                {
                    var list = GetAllFiles(current_savepath);
                    worker.ReportProgress(progressBar1.Maximum - list.Count);

                    /*int lastlength = 0;
                    try
                    {
                        int length = Directory.GetFiles(current_savepath, "*", SearchOption.AllDirectories).Length;
                        lastlength = length;
                        worker.ReportProgress(progressBar1.Maximum - length);
                    }
                    catch (UnauthorizedAccessException)
                    {
                        worker.ReportProgress(progressBar1.Maximum - lastlength);
                    }
                    catch (DirectoryNotFoundException)
                    {
                        worker.ReportProgress(progressBar1.Maximum - lastlength);
                    }*/
                }
                else
                {
                    worker.ReportProgress(progressBar1.Maximum);
                }
            }
            return;
        }

        private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage != -1)
            {
                progressBar1.Value = e.ProgressPercentage;
            }
            else
            {
                progressBar1.Value = 0;
            }
            //Application.DoEvents();
            if (Directory.Exists(current_savepath))
            {
                return;
            }
            else
            {
                Close();
            }
        }

        private void BackgroundWorker_Delete_DoWork(object sender, DoWorkEventArgs e)
        {
            DeleteAll(current_savepath);
            while (Directory.Exists(current_savepath))
            {
                if (Directory.Exists(current_savepath))
                {
                    continue;
                }
                else
                {
                    IsDeleted = true;
                    break;
                }
            }
            
            return;
        }

        private void FormDelete_Load(object sender, EventArgs e)
        {
            progressBar1.Minimum = 0;
            progressBar1.Maximum = Directory.GetFiles(current_savepath, "*", SearchOption.AllDirectories).Length;
            progressBar1.Value = 0;
            backgroundWorker1.RunWorkerAsync();
        }
    }
}
