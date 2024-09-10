namespace YUMTool_R
{
    partial class FormProgress
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProgress));
            label_log = new System.Windows.Forms.Label();
            progressBar_MainProgress = new System.Windows.Forms.ProgressBar();
            label_progress = new System.Windows.Forms.Label();
            timer_interval = new System.Windows.Forms.Timer(components);
            button_Abort = new System.Windows.Forms.Button();
            timer_Abort = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // label_log
            // 
            resources.ApplyResources(label_log, "label_log");
            label_log.Name = "label_log";
            // 
            // progressBar_MainProgress
            // 
            resources.ApplyResources(progressBar_MainProgress, "progressBar_MainProgress");
            progressBar_MainProgress.Name = "progressBar_MainProgress";
            // 
            // label_progress
            // 
            resources.ApplyResources(label_progress, "label_progress");
            label_progress.Name = "label_progress";
            // 
            // timer_interval
            // 
            timer_interval.Tick += Timer_interval_Tick;
            // 
            // button_Abort
            // 
            resources.ApplyResources(button_Abort, "button_Abort");
            button_Abort.Name = "button_Abort";
            button_Abort.UseVisualStyleBackColor = true;
            button_Abort.Click += Button_Abort_Click;
            // 
            // timer_Abort
            // 
            timer_Abort.Tick += Timer_Abort_Tick;
            // 
            // FormProgress
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ControlBox = false;
            Controls.Add(button_Abort);
            Controls.Add(label_progress);
            Controls.Add(progressBar_MainProgress);
            Controls.Add(label_log);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Name = "FormProgress";
            Load += FormProgress_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label_log;
        private System.Windows.Forms.ProgressBar progressBar_MainProgress;
        private System.Windows.Forms.Label label_progress;
        private System.Windows.Forms.Timer timer_interval;
        private System.Windows.Forms.Button button_Abort;
        private System.Windows.Forms.Timer timer_Abort;
    }
}