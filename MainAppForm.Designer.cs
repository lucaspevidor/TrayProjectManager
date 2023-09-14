namespace TrayProjectManager
{
    partial class MainAppForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainAppForm));
            notifyIcon = new NotifyIcon(components);
            SuspendLayout();
            // 
            // notifyIcon
            // 
            notifyIcon.BalloonTipText = "teste";
            notifyIcon.BalloonTipTitle = "Title";
            notifyIcon.Icon = (Icon)resources.GetObject("notifyIcon.Icon");
            notifyIcon.Text = "notifyIcon";
            notifyIcon.Visible = true;
            // 
            // MainAppForm
            // 
            ClientSize = new Size(338, 178);
            Name = "MainAppForm";
            ShowInTaskbar = false;
            FormClosing += MainAppForm_FormClosing;
            ResumeLayout(false);
        }

        #endregion

        private NotifyIcon notifyIcon;
    }
}