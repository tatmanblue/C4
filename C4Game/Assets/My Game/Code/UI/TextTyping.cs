using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

using CornTheory.Data;

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

        public event CompletedTextTypingAction OnTextTypingCompleted;
        /// <summary>
        /// The sound to make for a keypress
        /// </summary>
        [SerializeField] private AudioClip AudioClip;
        /// <summary>
        /// Required for playing the sound
        /// </summary>
        [SerializeField] private AudioSource AudioSource;
        [SerializeField] private TextMeshProUGUI UIField;        
        [SerializeField] private int MinDelay = 305;
        [SerializeField] private int MaxDelay = 840;

        private TypeableTextLine item;
        private int currentPosition = 0;
        private DateTime lastCheckTime = DateTime.MinValue;
        private int nextTypingEventMS = 0;
        private bool allDone = true;

        public void SetText(TypeableTextLine item)
        {
            this.item = item;
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
                if (item.Text.Length > 0) allDone = false;
                return;
            }
            
            // text typing is done
            if (currentPosition >= item.Text.Length)
            {
                allDone = true;
                lastCheckTime = DateTime.MaxValue;
                UIField.text = string.Empty;
                CompletedTextTypingAction action = OnTextTypingCompleted;
                if (action != null) action(item);
                return;
            }

            // now the fun thing of figuring out when
            // to type the next character
            TimeSpan span = DateTime.Now - lastCheckTime;
            if (span.Milliseconds >= nextTypingEventMS)
            {
                // set the next event ahead of doing anything else to
                // prevent another FixedUpdate from stomping on this
                nextTypingEventMS = RandomMS();
                lastCheckTime = DateTime.Now;
                currentPosition++;
                
                AudioSource.PlayOneShot(AudioClip);
                // TODO:
                // 1 rather always printing 1 char, print between 1 and x (like 3) to mimic typing more closely
                // 2 need to handle special tags like <B> </B> etc
                string textToShow = item.Text.Substring(0, currentPosition);

                
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