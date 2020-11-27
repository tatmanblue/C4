using UnityEngine;
using UnityEngine.UI;

using CornTheory.Data;

namespace CornTheory.UI
{
    /// <summary>
    /// Listens for TextTyping completion messages so that it can add the text
    /// to the ScrollView which is for displaying history of TypeableTextLine
    /// </summary>
    public class TextTypingHistoryRunner : MonoBehaviour
    {
        [SerializeField] private TextTyping Typing;
        /// <summary>
        /// Prefab:  TextHistoryItem.prefab
        /// </summary>
        [SerializeField] private GameObject DisplayItem = null;
        /// <summary>
        /// ScrollView UI Element with ListCreator script attached
        /// </summary>
        [SerializeField] private GameObject ScrollView = null;

        private CornTheory.UI.ListCreator listCreator;
        private ScrollRect scrollView;

        private void Start()
        {
            listCreator = ScrollView.GetComponent<CornTheory.UI.ListCreator>();
            scrollView = ScrollView.GetComponent<ScrollRect>();
            
            Typing.OnTextTypingCompleted += (TypeableTextLine item) =>
            {
                TextTypingHistoryItem displayItem = listCreator.AddItemToHistory<TextTypingHistoryItem>(DisplayItem);
                displayItem.Who.text = item.ActorId;
                displayItem.Said.text = item.Text;
                
                Canvas.ForceUpdateCanvases();
                scrollView.verticalScrollbar.value = 0f;
            };
        }
    }
}