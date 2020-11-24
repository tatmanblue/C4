using System.Collections;
using UnityEngine;

using CornTheory.Data;

namespace CornTheory.UI
{
    public class IncomingTextTypingHistoryRunner : MonoBehaviour
    {
        public event CompletedTextTypingAction OnTextTypingCompleted;
        /// <summary>
        /// Prefab:  TwoWayTextHistoryItem.prefab
        /// </summary>
        [SerializeField] private GameObject DisplayItem = null;
        /// <summary>
        /// ScrollView UI Element with ListCreator script attached
        /// </summary>
        [SerializeField] private GameObject ScrollView = null;

        [SerializeField] private AlphaFader Fader = null;

        private CornTheory.UI.ListCreator listCreator;
        private int messagesReceived = 0;
        private bool fadingStarted = false;

        private void Start()
        {
            listCreator = ScrollView.GetComponent<CornTheory.UI.ListCreator>();
        }

        public void AddIncomingTextHistoryItem(TypeableTextLine item)
        {
            GameObject spawnedItem = listCreator.StartItemToHistory(DisplayItem);
            IncomingTextTypingHistoryItem uiItem = spawnedItem.GetComponent<IncomingTextTypingHistoryItem>();
            StartCoroutine(StartTypingText(item, uiItem));
        }
        
        private IEnumerator StartTypingText(TypeableTextLine item, IncomingTextTypingHistoryItem uiItem)
        {
            // start by allowing the incoming message indicator to blink for duration
            float waitMS = uiItem.WaitMS / 1000F;
            yield return new WaitForSeconds(waitMS);
            
            // duration has expired, replace with text
            // TODO: should sound play first?
            listCreator.PlayReceivedIndicatorSound();
            yield return null;
            
            uiItem.WaitImage.SetActive(false);
            uiItem.Who.text = item.ActorId;
            uiItem.Said.text = item.Text;
            messagesReceived++;
            yield return null;
            
            // notify everyone
            CompletedTextTypingAction action = OnTextTypingCompleted;
            if (null != action) action(item);
            
            // and start the fading of the controls
            if ((fadingStarted == false) && (messagesReceived > 1) && (null != Fader))
            {
                fadingStarted = true;
                Fader.StartFading();
            }
        }
    }
}