using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace CornTheory.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class ScreenResolution
    {
        /// <summary>
        /// Id, unique
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// horizontal resolution eg: 3840 when its 3840x2160
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// vertical resolution eg:  2160 when its 3840x2160
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// frequency, either 60 or 120.
        /// </summary>
        public int Frequency { get; set; } = 60;
        /// <summary>
        /// how to display it on screen
        /// eg: "3840x2160"
        /// </summary>
        public string Display { get; set; }

        /// <summary>
        /// either 16:9 or 4:3
        /// </summary>
        public string Class { get; set; } = "16:9";
    }
    
    /// <summary>
    /// 
    /// </summary>
    public class GlobalSettings
    {
        /// <summary>
        /// value equals one of the Id values in screen-resolutions.json
        /// </summary>
        public int ResolutionId { get; set; } = 7;

        /// <summary>
        /// future proofing settings data. In the future we can check versions and convert
        /// as needed
        /// </summary>
        public int SettingsVersion { get; set; } = 1;
        
        public void Save()
        {
            string globalSettingsFileName = Application.persistentDataPath + "/globalsettings.json";
            string data = JsonConvert.SerializeObject(this);
            File.WriteAllText(globalSettingsFileName, data);
        }
        
        /**
         * TODO: maybe these functions should be in a separate class and keep GlobalSettings a pure DTO
        */
        public static List<ScreenResolution> GetAllResolutions()
        {
            string data = File.ReadAllText("Assets/My Game/Data/screen-resolutions.json");
            List<ScreenResolution> lines = JsonConvert.DeserializeObject<List<ScreenResolution>>(data);
            lines.OrderBy(i => i.Id);
            return lines;
        }

        public static GlobalSettings Load()
        {
            string globalSettingsFileName = Application.persistentDataPath + "/globalsettings.json";
            if (File.Exists(globalSettingsFileName))
            {
                try
                {
                    string data = File.ReadAllText(globalSettingsFileName);
                    GlobalSettings settings = JsonConvert.DeserializeObject<GlobalSettings>(data);
                    return settings;
                }
                // any exception causes a new instance to be name
                catch {}
            }

            Debug.Log($"a new global settings instance was created at {globalSettingsFileName}");
            return new GlobalSettings();
        }
        
    }
}