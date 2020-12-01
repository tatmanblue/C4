using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace CornTheory.UI
{
    /// <summary>
    /// Place this class on a prefab that is intended to be displayed as popup dialog
    /// Expectation is the prefab is made of 2d UI component
    /// </summary>
    public class PopupHandler : MonoBehaviour
    {
        public event PopupOpened OnPopupOpened;
        public event PopupClosed OnPopupClosed;
        [SerializeField] private Color BackgroundColor = new Color(10.0f / 255.0f, 10.0f / 255.0f, 10.0f / 255.0f, 0.6f);
        [SerializeField] private float DestroyTime = 0.5f;
        
        private bool handlingClose = false;
        private GameObject backgroundObject;
        private GameObject parent = null;

        public void Open(GameObject parent)
        {
            lock (gameObject)
            {
                this.parent = parent;
                handlingClose = false;
                AddBackground();
                PopupOpened action = OnPopupOpened;
                if (null != action) action(gameObject);
            }
        }

        public void Close()
        {
            lock (gameObject)
            {
                if (handlingClose == true) return;

                handlingClose = true;
                
                PopupClosed action = OnPopupClosed;
                if (null != action) action(gameObject);
                
                // TODO: where is "open" started?
                Animator animator = GetComponent<Animator>();
                if (animator && animator.GetCurrentAnimatorStateInfo(0).IsName("Open"))
                    animator.Play("Close");
                
                RemoveBackground();
                StartCoroutine(PopupDestroy());
            }
        }

        private void LateUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Close();
            }
        }

        // ---------------------------------
        // TODO
        // Questioning if AddBackground(), RemoveBackground and maybe PopupDestroy
        // belong here or in PopupManager
        
        /// <summary>
        /// We destroy the popup automatically DestroyTime after closing it.
        /// The destruction is performed asynchronously via a coroutine. 
        /// </summary>
        /// <returns></returns>
        private IEnumerator PopupDestroy()
        {
            yield return new WaitForSeconds(DestroyTime);
            Destroy(backgroundObject);
            Destroy(gameObject);
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
            backgroundObject.GetComponent<RectTransform>().sizeDelta = parent.GetComponent<RectTransform>().sizeDelta;
            backgroundObject.transform.SetParent(parent.transform, false);
            backgroundObject.transform.SetSiblingIndex(transform.GetSiblingIndex());
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