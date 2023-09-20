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

            // Initial definition
            configManager = new ConfigManager();
            MenuManager menuManager = new(configManager, notifyIcon);

            refreshTimer.Tick += menuManager.RefreshMenu;
            UpdateRefreshTimer(this, EventArgs.Empty);

            // Event definition
            configManager.ConfigGenerated += UpdateRefreshTimer;
        }

        protected override void SetVisibleCore(bool value)
        {
            base.SetVisibleCore(false);
        }

        private void MainAppForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            configManager.GenerateConfig();
        }

        private void UpdateRefreshTimer(object? sender, EventArgs e)
        {
            int refreshTime = configManager.settings.RefreshTime;
            if (refreshTime == 0)
            {
                refreshTimer.Enabled = false;
            } else if (refreshTime > 0) {
                refreshTimer.Interval = refreshTime * 1000;
                refreshTimer.Enabled = true;
            }
        }
    }
}
