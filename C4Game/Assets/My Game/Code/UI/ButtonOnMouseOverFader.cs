using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CornTheory.UI
{
    public class ButtonOnMouseOverFader : MonoBehaviour
    {
        [SerializeField] private AnimationClip FadeAnimationClip; 
        [SerializeField] private Animator  FadeAnimation;
        
        private void Start() {
            // FadeAnimation = GetComponent<Animator>();
        }
        
        public void OnMouseEnter()
        {
            if (null == FadeAnimation) return;

            if (FadeAnimation.enabled) return;

            Debug.Log("calling fadeIn");
            FadeAnimation.Play("FadeIn");
        }

        public void OnMouseExit()
        {
            if (null == FadeAnimation) return;

            Debug.Log("stopping fadeIn");
            FadeAnimation.enabled = false;
        }
    }
}