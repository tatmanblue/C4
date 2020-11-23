using UnityEngine;

namespace CornTheory
{
    public class ApplicationControl : MonoBehaviour
    {
        public void Quit(int exitCode = 0)
        {
            Application.Quit(exitCode);
        }

        public void Set640x480(bool fullScreen = true)
        {
            Screen.SetResolution(640, 480, fullScreen);
        }
        
        public void Set1024x768(bool fullScreen = true)
        {
            Screen.SetResolution(1024, 768, fullScreen);
        }
    }
}