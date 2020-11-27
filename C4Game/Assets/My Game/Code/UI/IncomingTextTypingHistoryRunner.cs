using System.Collections;
using UnityEngine;

using CornTheory.Data;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;

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

        [SerializeField] private RawImageAlphaFader ImageFader1 = null;
        [SerializeField] private ButtonAlphaFader ButtonFader1 = null;
        [SerializeField] private ButtonAlphaFader ButtonFader2 = null;
        [SerializeField] private TextAlphaFader TextFader1 = null;
        [SerializeField] private TextAlphaFader TextFader2 = null;

        private CornTheory.UI.ListCreator listCreator = null;
        private ScrollRect scrollView;
        private int messagesReceived = 0;
        private bool fadingStarted = false;

        private void Start()
        {
            listCreator = ScrollView.GetComponent<CornTheory.UI.ListCreator>();
            scrollView = ScrollView.GetComponent<ScrollRect>();
        }

        public void AddIncomingTextHistoryItem(TypeableTextLine item)
        {
            GameObject spawnedItem = listCreator.StartItemToHistory(DisplayItem);
            Canvas.ForceUpdateCanvases();
            scrollView.verticalScrollbar.value = 0f;
            IncomingTextTypingHistoryItem uiItem = spawnedItem.GetComponent<IncomingTextTypingHistoryItem>();
            StartCoroutine(StartTypingText(item, uiItem));
        }
        
        private IEnumerator StartTypingText(TypeableTextLine item, IncomingTextTypingHistoryItem uiItem)
        {
            // start by allowing the incoming message indicator to blink for duration
            Canvas.ForceUpdateCanvases();
            scrollView.verticalScrollbar.value = 0f;
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
            if ((fadingStarted == false) && (messagesReceived > 1))
            {
                fadingStarted = true;
                if (null != ImageFader1) ImageFader1.StartFading();
                if (null != ButtonFader1) ButtonFader1.StartFading();
                if (null != ButtonFader2) ButtonFader2.StartFading();
                if (null != TextFader1) TextFader1.StartFading();
                if (null != TextFader2) TextFader2.StartFading();
            }
        }
    }
}