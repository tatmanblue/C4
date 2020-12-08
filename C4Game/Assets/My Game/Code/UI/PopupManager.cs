using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        [SerializeField] private Color BackgroundColor = new Color(10.0f / 255.0f, 10.0f / 255.0f, 10.0f / 255.0f, 0.6f);
        [SerializeField] private float DestroyTime = 0.5f;

        private Stack<GameObject> dialogs = new Stack<GameObject>();
        private bool handlingClose = false;
        private GameObject backgroundObject;
        
        public void ShowPopup(GameObject which)
        {
            if (null == which)
                return;

            if (0 == dialogs.Count) AddBackground();
            
            GameObject popup = Instantiate(which) as GameObject;

            dialogs.Push(popup);
            
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

            // TODO: add a test to make sure closed dialog is same as the one
            // on the top of the stack
            GameObject stackDialog = dialogs.Pop();
            if (0 == dialogs.Count)
            {
                StartCoroutine(PopupDestroy());
            }
            
            // if we got to here, we have a different problem when PopupHandler isn't attached to popupPrefab
            // than we experience in ShowPopup(...).  For now will just let the exception bubble up but
            // we may want to consider handling it a bit better
            // toThink()
            PopupHandler handler = popupPrefab.GetComponent<PopupHandler>();
            handler.OnPopupClosed -= HandlePopupClosed;
            handler.OnPopupOpened -= HandlePopupOpened;
            
            // TODO: 
            // PlayerState.Instance.GameState = GameUIState.InWorld;
            
            // TODO: using the Stack<> if there anything left in the stack
            // we want to open that, possibly :P

        }
        
        
        /// <summary>
        /// We destroy the popup automatically DestroyTime after closing it.
        /// The destruction is performed asynchronously via a coroutine. 
        /// </summary>
        /// <returns></returns>
        private IEnumerator PopupDestroy()
        {
            yield return new WaitForSeconds(DestroyTime);
            RemoveBackground();
            Destroy(backgroundObject);
        }
        
        /// <summary>
        /// This adds shading to the rest of the screen to make it looked somewhat greyed out
        /// </summary>
        private void AddBackground()
        {
            Texture2D backgroundTexture = new Texture2D(1, 1);
            backgroundTexture.SetPixel(0, 0, BackgroundColor);
            backgroundTexture.Apply();

            backgroundObject = new GameObject("PopupBackground");
            Rect rect = new Rect(0, 0, backgroundTexture.width, backgroundTexture.height);
            Sprite sprite = Sprite.Create(backgroundTexture, rect, new Vector2(0.5f, 0.5f), 1);
            
            Image image = backgroundObject.AddComponent<Image>();
            image.material.mainTexture = backgroundTexture;
            image.sprite = sprite;
            
            Color newColor = image.color;
            image.color = newColor;
            image.canvasRenderer.SetAlpha(0.0f);
            image.CrossFadeAlpha(1.0f, 0.4f, false);
            
            backgroundObject.transform.localScale = new Vector3(1, 1, 1);
            backgroundObject.GetComponent<RectTransform>().sizeDelta = Parent.GetComponent<RectTransform>().sizeDelta;
            backgroundObject.transform.SetParent(Parent.transform, false);
            // original code has this next line and when it was used in PopupHandler, it worked
            // leaving it here just for reminder/documentation should there be problems later.
            // backgroundObject.transform.SetSiblingIndex(transform.GetSiblingIndex());
            backgroundObject.transform.SetAsLastSibling();
        }

        /// <summary>
        /// inverse of AddBackground()
        /// </summary>
        private void RemoveBackground()
        {
            if (null == backgroundObject)
                return;

            Image image = backgroundObject.GetComponent<Image>();
            if (image != null)
                image.CrossFadeAlpha(0.0f, 0.2f, false);
        }        
    }
}