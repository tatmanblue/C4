using System.Collections.Generic;
using CornTheory.Interfaces;
using UnityEngine;

namespace CornTheory.UI.Tweening
{
    /// <summary>
    /// 
    /// </summary>
    public class FaderController : MonoBehaviour, IFader
    {
        public event CompletedFading OnFadingComplete;
        
        [SerializeField] private List<GameObject> Items = new List<GameObject>();
        
        public void StartFading()
        {
            Debug.Log("FadeController started fading controls");
            foreach (GameObject item in Items)
            {
                IFader objectFader = null;

                objectFader = item.GetComponent<IFader>();
                
                if (null == objectFader)
                {
                    Debug.LogWarning($"{item.name} doesn't have a IFader attached to it");
                    continue;
                }
                
                objectFader.StartFading();
            }
        }
    }
}