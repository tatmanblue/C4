using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

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
        [SerializeField] private TextAsset ResourceFile;

        private List<TypeableTextLine> lines;
        private int activeLine = 0;
        
        private void Start()
        {
            Typing.OnTextTypingCompleted += (TypeableTextLine item) =>
            {
                SendNextItem();
            };

            /*
             saving this info here even though its for another functionality: save game state
             File.Create(Application.persistentDataPath + "/gamesave.save");
             */
            lines = JsonConvert.DeserializeObject<List<TypeableTextLine>>(ResourceFile.text);
            activeLine = 0;
            SendNextItem();
        }

        private void SendNextItem()
        {
            if (activeLine >= lines.Count) return;
            if (null == lines) return;
            
            StartCoroutine(DelaySettingText(lines[activeLine].Delay));
        }

        private IEnumerator DelaySettingText(float ms)
        {
            float waitMS = ms / 1000F;
            Debug.Log($"waiting for {waitMS}");
            yield return new WaitForSeconds(waitMS);
            Typing.SetText(lines[activeLine]);
            activeLine++;

        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class TypeableTextLine
    {
        /// <summary>
        /// unique ID. Text will be be played sequentially based on this ID
        /// </summary>
        public int Id;
        public string ActorId;
        /// <summary>
        /// milliseconds to wait before start typing the text
        /// </summary>
        public int Delay = 750;
        /// <summary>
        /// the text to "typed"
        /// </summary>
        public string Text;
    }
}