using UnityEngine;

namespace CornTheory.UI
{
    [RequireComponent(typeof(TextTyping))]
    public class TextTypingRunner : MonoBehaviour
    {
        [SerializeField] private TextTyping Typing;

        private void Start()
        {
            Typing.OnCompleted += () =>
            {
                print("typing done");
            };
            
            Typing.SetText("This is some really <B>long text</B> and some special chars.");
        }
        
    }
}