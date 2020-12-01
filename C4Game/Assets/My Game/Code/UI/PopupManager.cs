using UnityEngine;

namespace CornTheory.UI
{
    /// <summary>
    /// PopManager is responsible for managing popups (dialogs, mostly but
    /// there might some other cases where some UI needs to "popup") by creating the
    /// popup and handling popup events.
    /// </summary>
    public class PopupManager : MonoBehaviour
    {
        [SerializeField] private GameObject Parent = null;
        
        public void ShowPopup(GameObject which)
        {
            if (null == which)
                return;

            GameObject popup = Instantiate(which) as GameObject;
            popup.SetActive(true);
            popup.transform.localScale = new Vector3(1, 1, 1);

            if (null != Parent)
                popup.transform.SetParent(Parent.transform, false);

            PopupHandler handler = popup.GetComponent<PopupHandler>();
            if (!handler)
            {
                Debug.LogWarning($"{which.name} is missing PopupHandler class.");
                // since we cannot manage the popup as expected, getting rid of it
                popup.SetActive(false);
                Destroy(popup);
                return;
            }

            handler.OnPopupOpened += HandlePopupOpened;
            handler.OnPopupClosed += HandlePopupClosed;
            handler.Open(Parent);
        }

        private void HandlePopupOpened(GameObject popupPrefab)
        {
            Debug.Log($"{popupPrefab.name} opened");
            // TODO: 
            // PlayerState.Instance.GameState = GameUIState.InWorldUI;
        }

        private void HandlePopupClosed(GameObject popupPrefab)
        {
            Debug.Log($"{popupPrefab.name} closed");
            
            // if we got to here, we have a different problem when PopupHandler isn't attached to popupPrefab
            // than we experience in ShowPopup(...).  For now will just let the exception bubble up but
            // we may want to consider handling it a bit better
            // toThink()
            PopupHandler handler = popupPrefab.GetComponent<PopupHandler>();
            handler.OnPopupClosed -= HandlePopupClosed;
            handler.OnPopupOpened -= HandlePopupOpened;
            
            // TODO: 
            // PlayerState.Instance.GameState = GameUIState.InWorld;

        }
    }
}