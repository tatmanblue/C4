namespace CornTheory.Interfaces
{
    public interface IFader
    {
        void StartFading();
        event CompletedFading OnFadingComplete;
    }
}