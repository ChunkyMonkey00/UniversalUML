using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace UUML
{
    [System.Serializable]
    public class SettingsData
    {
        public readonly string version = "1.0.0";

        /* Self */
        public bool b_GodMode;

        /* Theme color */
        public Color c_Theme = new Color(1f, 1f, 1f, 1f);

        /* Settings */
        public bool b_Tooltips = true;
    }

    public class Settings
    {
        private string settingsFilePath;
        private const string settingsFileName = "UUMLSettings.json";

        public Settings()
        {
            settingsFilePath = Path.Combine(Application.persistentDataPath, settingsFileName);
            LoadSettings();
        }

        public static Settings Instance
        {
            get
            {
                if (Settings.instance == null)
                {
                    Settings.instance = new Settings();
                }
                return Settings.instance;
            }
        }

        /* UI */
        public static float TEXT_HEIGHT = 30f;
        public Rect windowRect = new Rect(50f, 50f, 545f, 400f);
        public bool b_isMenuOpen;

        public static class Changelog
        {
            public static StringBuilder changes;

            public static void ReadChanges()
            {
                changes = new StringBuilder(ResourceReader.ReadTextFile("UUML.Resources.Changelog.txt"));
            }
        }

        public static class Credits
        {
            public static StringBuilder credits;

            public static void ReadCredits()
            {
                credits = new StringBuilder(ResourceReader.ReadTextFile("UUML.Resources.Credits.txt"));
            }
        }

        /* SettingsData */
        public SettingsData settingsData = new SettingsData();

        public static class ResourceReader
        {
            public static string ReadTextFile(string resourceName)
            {
                string text = string.Empty;
                using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
                {
                    using (StreamReader streamReader = new StreamReader(manifestResourceStream))
                    {
                        text = streamReader.ReadToEnd();
                    }
                }
                return text;
            }
        }

        private void LoadSettings()
        {
            if (File.Exists(settingsFilePath))
            {
                string json = File.ReadAllText(settingsFilePath);
                settingsData = JsonUtility.FromJson<SettingsData>(json);
            }
            else
            {
                settingsData = new SettingsData();
                SaveSettings();
            }
        }

        public void SaveSettings()
        {
            string json = JsonUtility.ToJson(settingsData, true);
            File.WriteAllText(settingsFilePath, json);
        }

        private static Settings instance;
    }
}
