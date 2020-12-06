using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

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
    /// TODO: simply a place holder for the moment
    /// </summary>
    public class GlobalSettings
    {
        public static List<ScreenResolution> GetAllResolutions()
        {
            // TODO:  read from disk
            string data = File.ReadAllText("Assets/My Game/Data/screen-resolutions.json");
            List<ScreenResolution> lines = JsonConvert.DeserializeObject<List<ScreenResolution>>(data);
            lines.OrderBy(i => i.Id);
            return lines;
        }
    }
}