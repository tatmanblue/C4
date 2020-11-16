using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CornTheory.UI
{
    /// <summary>
    /// For a message displayed in a ScrollView.  "Bound" to
    /// prefab "TextHistoryItem.prefab"
    /// </summary>
    public class TextTypingHistoryItem : MonoBehaviour
    {
        public TextMeshProUGUI Who;
        public TextMeshProUGUI Said;
    }
}