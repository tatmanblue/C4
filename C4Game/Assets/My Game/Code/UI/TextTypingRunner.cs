using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

using CornTheory.Data;

namespace CornTheory.UI
{
    /// <summary>
    /// Handles feeding a TextTyping instance with TypeableTextLine one at time as each
    /// TypeableTextLine is completed
    /// </summary>
    [RequireComponent(typeof(TextTyping))]
    public class TextTypingRunner : MonoBehaviour
    {
        [SerializeField] private TextTyping Typing;
        [SerializeField] private IncomingTextTypingHistoryRunner IncomingTyping;
        [SerializeField] private TextAsset ResourceFile;

        private List<TypeableTextLine> lines;
        private int activeLine = 0;
        
        private void Start()
        {
            Typing.OnTextTypingCompleted += (TypeableTextLine item) =>
            {
                SendNextItem();
            };

            IncomingTyping.OnTextTypingCompleted += (TypeableTextLine item) =>
            {
                SendNextItem();
            };

            /*
             saving this info here even though its for another functionality: save game state
             File.Create(Application.persistentDataPath + "/gamesave.save");
             */
            lines = JsonConvert.DeserializeObject<List<TypeableTextLine>>(ResourceFile.text);
            lines.OrderBy(i => i.Id);
            activeLine = 0;
            SendNextItem();
        }

        private void SendNextItem()
        {
            if (activeLine >= lines.Count) return;
            if (null == lines) return;

            switch (lines[activeLine].LineType)
            {
                case TypeableTextLineType.Incoming:
                    StartCoroutine(StartIncomingItem());
                    break;
                default:
                    StartCoroutine(StartTypingText());
                    break;
            }
            
        }

        /// <summary>
        /// In a messaging client, a user types text.  In this case, the user
        /// is "Cam" or the player.  So TypingText emulates "Cam" typing text
        /// </summary>
        /// <returns></returns>
        private IEnumerator StartTypingText()
        {
            int currentLine = activeLine;
            activeLine++;
            float waitMS = lines[currentLine].Delay / 1000F;
            yield return new WaitForSeconds(waitMS);
            Typing.SetText(lines[currentLine]);
        }

        /// <summary>
        /// In a messaging client, when someone remote is typing a message an indicator
        /// shows they are typing a message and beep occurs when the message is received. 
        /// In this case, IncomingItem is that behavior
        /// </summary>
        /// <returns></returns>
        private IEnumerator StartIncomingItem()
        {
            int currentLine = activeLine;
            activeLine++;
            float waitMS = lines[currentLine].Delay / 1000F;
            yield return new WaitForSeconds(waitMS);
            IncomingTyping.AddIncomingTextHistoryItem(lines[currentLine]);
            yield break;
        }
    }
}