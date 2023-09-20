using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace TrayProjectManager.Classes
{
    internal class ConfigManager
    {
        private string configFilePath;
        public Settings settings;

        public event EventHandler ConfigGenerated; 

        public ConfigManager()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string appFolderPath = Path.Combine(appDataPath, "TrayProjectManager");

            Directory.CreateDirectory(appFolderPath);

            configFilePath = Path.Combine(appFolderPath, "settings.json");
            settings = new();

            if (!File.Exists(configFilePath))
            {
                GenerateConfig();
            }
            else
            {
                try
                {
                    string json = File.ReadAllText(configFilePath);
                    settings = JsonConvert.DeserializeObject<Settings>(json);
                } catch(Exception)
                {
                    DialogResult res = MessageBox.Show("Erro ao ler o arquivo de configurações. Deseja gerar um novo?", 
                        "Erro", MessageBoxButtons.YesNo);
                    if (res == DialogResult.Yes)
                    {
                        GenerateConfig();
                    } else
                    {
                        Application.Exit();
                    }
                }                
            }

            ValidatePaths();
        }

        public void GenerateConfig()
        {
            ValidatePaths();
            string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(configFilePath, json);

            ConfigGenerated?.Invoke(this, EventArgs.Empty);
        }

        private void ValidatePaths()
        {
            FolderPath[] validRecent = settings.RecentProjects.Where(f => Directory.Exists(f.Path)).ToArray();
            FolderPath[] validFolders = settings.WatchFolderPaths.Where(f => Directory.Exists(f.Path)).ToArray();
            FolderPath[] validProjects = settings.IndividualProjectPath.Where(f => Directory.Exists(f.Path)).ToArray();

            settings.RecentProjects = validRecent;
            settings.WatchFolderPaths = validFolders;
            settings.IndividualProjectPath = validProjects;
        }
    }
}
