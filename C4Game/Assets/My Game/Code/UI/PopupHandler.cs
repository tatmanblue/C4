using System;
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
        [SerializeField] private float DestroyTime = 0.25f;
        
        private bool handlingClose = false;
        private GameObject backgroundObject;

        public void Open(GameObject parent)
        {
            lock (gameObject)
            {
                handlingClose = false;
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
                
                // RemoveBackground();
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
        
        /// <summary>
        /// We destroy the popup automatically DestroyTime after closing it.
        /// The destruction is performed asynchronously via a coroutine. 
        /// </summary>
        /// <returns></returns>
        private IEnumerator PopupDestroy()
        {
            yield return new WaitForSeconds(DestroyTime);
            Destroy(gameObject);
        }
    }
}