namespace TrayProjectManager
{
    partial class ConfigForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigForm));
            browseButton = new Button();
            okButton = new Button();
            groupBox1 = new GroupBox();
            pathTextBox = new TextBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // browseButton
            // 
            browseButton.Location = new Point(399, 23);
            browseButton.Name = "browseButton";
            browseButton.Size = new Size(34, 23);
            browseButton.TabIndex = 1;
            browseButton.Text = "...";
            browseButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            okButton.Location = new Point(376, 79);
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 23);
            okButton.TabIndex = 3;
            okButton.Text = "OK";
            okButton.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(pathTextBox);
            groupBox1.Controls.Add(browseButton);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(439, 61);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "VSCode Executable Path";
            // 
            // pathTextBox
            // 
            pathTextBox.Location = new Point(6, 23);
            pathTextBox.Name = "pathTextBox";
            pathTextBox.PlaceholderText = "VSCode Path";
            pathTextBox.Size = new Size(387, 23);
            pathTextBox.TabIndex = 2;
            // 
            // ConfigForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(463, 117);
            Controls.Add(groupBox1);
            Controls.Add(okButton);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(479, 156);
            MinimizeBox = false;
            MinimumSize = new Size(479, 156);
            Name = "ConfigForm";
            Text = "Settings";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button browseButton;
        private Button okButton;
        private GroupBox groupBox1;
        private TextBox pathTextBox;
    }
}