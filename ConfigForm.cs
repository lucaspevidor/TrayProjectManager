using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrayProjectManager.Classes;

namespace TrayProjectManager
{
    internal partial class ConfigForm : Form
    {
        public string SelectedCodePath { get; private set; }
        private ConfigManager configManager;

        public ConfigForm(ConfigManager configManager)
        {
            InitializeComponent();

            this.configManager = configManager;

            pathTextBox.Text = configManager.settings.VSCodePath;
            ValidateOkButton(this, EventArgs.Empty);

            DefineEvents();
        }

        private void DefineEvents()
        {
            pathTextBox.TextChanged += ValidateOkButton;
            browseButton.Click += BrowseButtonClicked;
            okButton.Click += OkButtonClicked;
        }

        private void ValidateOkButton(object sender, EventArgs e)
        {
            okButton.Enabled = !string.IsNullOrWhiteSpace(pathTextBox.Text);
        }

        private void BrowseButtonClicked(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Executable files (*.exe)|*.exe";
                DialogResult res = openFileDialog.ShowDialog();

                if (res == DialogResult.OK)
                {
                    pathTextBox.Text = openFileDialog.FileName;
                }
            }
        }

        private void OkButtonClicked(object sender, EventArgs e)
        {
            if (File.Exists(pathTextBox.Text))
            {
                SelectedCodePath = pathTextBox.Text;
                DialogResult = DialogResult.OK;
                configManager.settings.VSCodePath = SelectedCodePath;
                configManager.GenerateConfig();
                Close();
            }
        }
    }
}
