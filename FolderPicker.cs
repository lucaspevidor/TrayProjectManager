using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrayProjectManager
{
    public partial class FolderPicker : Form
    {
        public string SelectedPath { get; private set; }
        public string SelectedName { get; private set; }

        public enum DialogType
        {
            PROJECT,
            FOLDER,
        }

        private DialogType Type;

        public FolderPicker(DialogType type = DialogType.PROJECT)
        {
            InitializeComponent();

            Type = type;
            nameTextBox.Enabled = false;
            okButton.Enabled = false;

            if (type == DialogType.PROJECT)
            {
                this.Text = "Pick a project";
                pathTextBox.PlaceholderText = "Project path";
                nameTextBox.PlaceholderText = "Project name";
            }
            if (type == DialogType.FOLDER)
            {
                this.Text = "Pick a folder";
                pathTextBox.PlaceholderText = "Folder path";
                nameTextBox.PlaceholderText = "Folder name";
            }

            DefineEvents();
        }

        private void DefineEvents()
        {
            pathTextBox.TextChanged += ItemPathChanged;
            nameTextBox.TextChanged += ItemNameChanged;
            browseButton.Click += BrowseButtonClicked;
            okButton.Click += OkButtonClicked;
        }

        private void ItemPathChanged(object sender, EventArgs e)
        {
            nameTextBox.Enabled = pathTextBox.Text != "";

            ValidateButton();
        }

        private void ItemNameChanged(object sender, EventArgs e)
        {
            ValidateButton();
        }

        private void ValidateButton()
        {
            bool pathOk = !String.IsNullOrEmpty(pathTextBox.Text);
            bool nameOk = !String.IsNullOrEmpty(nameTextBox.Text);

            okButton.Enabled = pathOk && nameOk;
        }

        private void BrowseButtonClicked(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult res = fbd.ShowDialog();

                if (res == DialogResult.OK && !String.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    pathTextBox.Text = fbd.SelectedPath;
                }
            }
        }

        private void OkButtonClicked(object sender, EventArgs e)
        {
            if (!Directory.Exists(pathTextBox.Text))
            {
                MessageBox.Show("The selected path is invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SelectedPath = pathTextBox.Text;
                SelectedName = nameTextBox.Text;
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
