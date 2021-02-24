using UnityEngine;

namespace CornTheory.UI.Dialogs
{
    /// <summary>
    ///
    /// 
    /// </summary>
    public class DebugWindow : MonoBehaviour
    {
        [SerializeField] private GameObject Window = null;
        
        private const int DEBUG_WINDOW_COMMAND_LEN = 6;
        private const string DEBUG_WINDOW_COMMAND = "/debug";
        private string lastSixChars = string.Empty;
        
        /// <summary>
        /// need to check for a command (currently only have the one)
        /// once a command is found, activate/execute it
        /// </summary>
        private void LateUpdate()
        {
            string characters = Input.inputString;
            if (characters.Length <= 0) return;
            
            // TODO: I think this line could be deleted
            if (characters.Contains(DEBUG_WINDOW_COMMAND)) Debug.Log("detected debug window open");
            
            int length = characters.Length > 6 ? characters.Length - DEBUG_WINDOW_COMMAND_LEN : characters.Length;
            lastSixChars = $"{lastSixChars}{characters.Substring(length - 1)}";

            if (lastSixChars.Length > DEBUG_WINDOW_COMMAND_LEN)
            {
                length = lastSixChars.Length - DEBUG_WINDOW_COMMAND_LEN;
                lastSixChars = lastSixChars.Substring(length - 1);
            }
            
            if (lastSixChars.Contains(DEBUG_WINDOW_COMMAND)) Debug.Log("detected debug window open.");
        }
    }
}