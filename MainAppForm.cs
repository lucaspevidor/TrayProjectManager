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
    public partial class MainAppForm : Form
    {
        ConfigManager configManager;
        public MainAppForm()
        {
            InitializeComponent();
            // Event definition

            // Initial definition
            configManager = new ConfigManager();
            MenuManager menuManager = new(configManager, notifyIcon);


        }

        protected override void SetVisibleCore(bool value)
        {
            base.SetVisibleCore(false);
        }

        private void MainAppForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            configManager.GenerateConfig();
        }
    }
}
