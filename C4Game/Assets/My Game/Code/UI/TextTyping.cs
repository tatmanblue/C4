using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CornTheory.UI
{
    /// <summary>
    ///
    /// Emulates someone typing on a computer screen.  Text will be displayed character by character
    /// in uneven random time intervals.  Audio is played at each text change.
    /// 
    /// Need to handle special chars
    /// ref: http://digitalnativestudios.com/textmeshpro/docs/ScriptReference/RichTags.html
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(AudioClip))]
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextTyping : MonoBehaviour
    {
        public event CompletedAction OnCompleted;
        [SerializeField] private AudioClip AudioClip;
        [SerializeField] private AudioSource AudioSource;
        [SerializeField] private TextMeshProUGUI UIField;
        [SerializeField] private string Text;
        [SerializeField] private int MinDelay = 375;
        [SerializeField] private int MaxDelay = 840;

        private int currentPosition = 0;
        private DateTime lastCheckTime = DateTime.MinValue;
        private int nextTypingEventMS = 0;
        private bool allDone = true;

        public void SetText(string text)
        {
            Text = text;
            allDone = false;
            currentPosition = 0;
            lastCheckTime = DateTime.MinValue;
        }
        
        private void FixedUpdate()
        {
            // this means all text has been typed AND the event has fired
            if (allDone == true) return;

            // initialization
            if (lastCheckTime == DateTime.MinValue)
            {
                nextTypingEventMS = MinDelay;
                lastCheckTime = DateTime.Now;
                if (Text.Length > 0) allDone = false;
                return;
            }
            
            // text typing is done
            if (currentPosition >= Text.Length)
            {
                allDone = true;
                lastCheckTime = DateTime.MaxValue;
                CompletedAction action = OnCompleted;
                if (action != null) action();
                return;
            }

            // now the fun thing of figuring out when
            // to type the next character
            TimeSpan span = DateTime.Now - lastCheckTime;
            if (span.Milliseconds >= nextTypingEventMS)
            {
                nextTypingEventMS = RandomMS();
                
                lastCheckTime = DateTime.Now;
                
                AudioSource.PlayOneShot(AudioClip);
                // TODO:
                // 1 rather always printing 1 char, print between 1 and x (like 3) to mimic typing more closely
                // 2 need to handle special tags like <B> </B> etc
                string textToShow = Text.Substring(0, currentPosition + 1);
                currentPosition++;
                
                UIField.text = textToShow;
            }
        }

        private int RandomMS()
        {
            int result = Random.Range(MinDelay, MaxDelay);
            if (result > MaxDelay) result = MaxDelay;

            return result;
        }

        private int RandomChars()
        {
            return Random.Range(0, 4);
        }
    }
}