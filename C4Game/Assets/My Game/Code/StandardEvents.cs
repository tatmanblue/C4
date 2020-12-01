using CornTheory.Data;
using UnityEngine;

namespace CornTheory
{
    /// <summary>
    /// An object will fire this event to indicate it has completed its task
    /// </summary>
    public delegate void CompletedAction();

    public delegate void CompletedFading();
    public delegate void CompletedTextTypingAction(TypeableTextLine item);

    /// <summary>
    /// A popup dialog has been opened
    /// </summary>
    
    public delegate void PopupOpened(GameObject which);

    /// <summary>
    /// A popup dialog has been closed
    /// </summary>
    public delegate void PopupClosed(GameObject which);

    /// <summary>
    /// If dialogs are opened sequentially, this event fires after the
    /// last dialog has been closed
    /// </summary>
    public delegate void PopupAllClosed();

    /// <summary>
    /// Fires when a dialog is opened (pretty much the same
    /// as PopupOpened except it fires once until PopupAllClosed() is fired)
    /// </summary>
    public delegate void PopupOpenStarted();
}