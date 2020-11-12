using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CornTheory
{
    public static class Constants
    {
        // GameObjects
        public static readonly string Canvas = "Game Canvas";
        public static readonly string CameraArm = "CameraArm";
        public static readonly string MainCamera = "Main Camera";
        public static readonly string MissionMananger = "Mission Manager";
        public static readonly string Player = "Player2";

        // skybox - makes materials name
        public static readonly string SunUpSkyBox = "";
        public static readonly string SunDownSkyBox = "";
        public static readonly string DayTimeSkyBox = "Day Sun High CloudsLayer 2";
        public static readonly string NightTimeSkyBox = "";

        // GameObject Tags
        public static readonly string TagMainCamera = "MainCamera";

        // Scenes these need to match the sceen file name
        public static readonly string MainMenuScene = "MainMenu";
        public static readonly string MainWorldScene = "TBD-CHANGE-THIS";
        public static readonly string MainWorldSceneOld = "TBD-CHANGE-THIS";
        public static readonly string MainWorldSceneExperimental = "TBD-CHANGE-THIS";
        public static readonly string OpenMenuScene =  "MainMenu";
        public static readonly string DevPlayArenaScene = "Play Arena";

        // behaviors
        public static readonly float PopupInvokeDelay = 0.5f;
    }

}
