using System;

namespace CornTheory.Data
{

    public enum TypeableTextLineType
    {
        /// <summary>
        /// don't care
        /// </summary>
        NA,
        /// <summary>
        ///  represents it being typed as the user
        /// </summary>
        Local,
        /// <summary>
        /// represents its being typed someone on another "system"
        /// </summary>
        Incoming
    }
    
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class TypeableTextLine
    {
        /// <summary>
        /// unique ID. Text will be be played sequentially based on this ID
        /// </summary>
        public int Id;
        /// <summary>
        /// 
        /// </summary>
        public TypeableTextLineType LineType = TypeableTextLineType.Local;
        /// <summary>
        /// unique ID per "voice" in chat.
        /// TODO: it would be nice if this could be processed through app settings similar to how an
        ///       env variable is used.
        /// </summary>
        public string ActorId;
        /// <summary>
        /// milliseconds to wait before start typing the text
        /// </summary>
        public int Delay = 750;
        /// <summary>
        /// the text to "typed"
        /// </summary>
        public string Text;
    }
}