namespace TrayProjectManager
{
    partial class FolderPicker
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
            pathTextBox = new TextBox();
            browseButton = new Button();
            nameTextBox = new TextBox();
            okButton = new Button();
            SuspendLayout();
            // 
            // pathTextBox
            // 
            pathTextBox.Location = new Point(12, 12);
            pathTextBox.Name = "pathTextBox";
            pathTextBox.Size = new Size(453, 23);
            pathTextBox.TabIndex = 1;
            // 
            // browseButton
            // 
            browseButton.Location = new Point(471, 12);
            browseButton.Name = "browseButton";
            browseButton.Size = new Size(39, 23);
            browseButton.TabIndex = 0;
            browseButton.Text = "...";
            browseButton.UseVisualStyleBackColor = true;
            // 
            // nameTextBox
            // 
            nameTextBox.Location = new Point(12, 41);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new Size(407, 23);
            nameTextBox.TabIndex = 3;
            // 
            // okButton
            // 
            okButton.Location = new Point(425, 41);
            okButton.Name = "okButton";
            okButton.Size = new Size(85, 23);
            okButton.TabIndex = 2;
            okButton.Text = "OK";
            okButton.UseVisualStyleBackColor = true;
            // 
            // FolderPicker
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(522, 79);
            Controls.Add(okButton);
            Controls.Add(browseButton);
            Controls.Add(nameTextBox);
            Controls.Add(pathTextBox);
            Name = "FolderPicker";
            Text = "FolderPicker";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox pathTextBox;
        private Button browseButton;
        private TextBox nameTextBox;
        private Button okButton;
    }
}