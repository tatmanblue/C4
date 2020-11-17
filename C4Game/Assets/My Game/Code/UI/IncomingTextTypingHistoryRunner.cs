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

        private CornTheory.UI.ListCreator listCreator;

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
            float waitMS = uiItem.WaitMS / 1000F;
            yield return new WaitForSeconds(waitMS);
            uiItem.WaitImage.SetActive(false);
            uiItem.Who.text = item.ActorId;
            uiItem.Said.text = item.Text;
            listCreator.PlayReceivedIndicatorSound();
            CompletedTextTypingAction action = OnTextTypingCompleted;
            if (null != action) action(item);
        }
    }
}