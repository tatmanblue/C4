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
            lines.OrderBy(i => i.Id);
            activeLine = 0;
            SendNextItem();
        }

        private void SendNextItem()
        {
            if (activeLine >= lines.Count) return;
            if (null == lines) return;
            
            Debug.Log($"line type {lines[activeLine].LineType}");
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
}