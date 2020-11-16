using UnityEngine;

namespace CornTheory.UI
{
    /// <summary>
    /// Listens for TextTyping completion messages so that it can add the text
    /// to the ScrollView which is for displaying history of TypeableTextLine
    /// </summary>
    public class TextHistoryRunner : MonoBehaviour
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

        private void Start()
        {
            listCreator = ScrollView.GetComponent<CornTheory.UI.ListCreator>();
            
            Typing.OnTextTypingCompleted += (TypeableTextLine item) =>
            {
                TextTypingHistoryItem displayItem = listCreator.AddItemToHistory<TextTypingHistoryItem>(DisplayItem);
                displayItem.Who.text = item.ActorId;
                displayItem.Said.text = item.Text;
            };
        }
    }
}