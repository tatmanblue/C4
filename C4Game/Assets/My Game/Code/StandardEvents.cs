using CornTheory.Data;

namespace CornTheory
{
    /// <summary>
    /// An object will fire this event to indicate it has completed its task
    /// </summary>
    public delegate void CompletedAction();

    public delegate void CompletedFading();
    public delegate void CompletedTextTypingAction(TypeableTextLine item);
}