using System;
using CornTheory;
using CornTheory.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace My_Game.Code.UI.Tweening
{
    /// <summary>
    /// Fades In (using alpha channel) a raw image
    /// </summary>
    public class RawImageAlphaFader : MonoBehaviour, IFader
    {
        public event CompletedFading OnFadingComplete;
        
        /// <summary>
        /// UI game object to affect the change
        /// TODO: should this be a different type?
        /// </summary>
        [SerializeField] private RawImage Item;
        /// <summary>
        /// At the end of DurationMS, Alpha channel of the color will be this
        /// </summary>
        [SerializeField] private float AlphaAdjustTo;
        /// <summary>
        /// the number if milliseconds to incrementally adjust alpha.
        /// </summary>
        [SerializeField] private int DurationMS;
        /// <summary>
        /// when true, the fading start immediately
        /// </summary>
        [SerializeField] private bool AutoStart = false;

        private DateTime startedAt;
        private DateTime lastUpdateAt;
        private float currentAlpha = 0.0F;
        private float alphaDelta = 0.0F;
        private bool run = true;
        private Color originalColor;

        /// <summary>
        /// The StartFading methods are used when the component is not allow to AutoStart
        /// aka (AutoStart == true)
        /// </summary>
        /// <param name="alphaAdjustTo"></param>
        /// <param name="durationMS"></param>
        public void StartFading(float alphaAdjustTo, int durationMS)
        {
            AlphaAdjustTo = alphaAdjustTo;
            DurationMS = durationMS;
            StartFading();   
        }

        public void StartFading()
        {
            run = true;
        }

        private void Start()
        {
            // dont let any frames start until we did our math
            run = false;
            lastUpdateAt = DateTime.Now;
            startedAt = lastUpdateAt;
            alphaDelta = AlphaAdjustTo / DurationMS;
            originalColor = Item.color;
            Item.color = new Color(originalColor.a, originalColor.g, originalColor.b, 0);
            
            // if autostart is true, then FixedUpdate will start doing the work 
            run = AutoStart;
        }

        private void FixedUpdate()
        {
            if (false == run) return;
            
            // https://owlcation.com/stem/How-to-fade-out-a-GameObject-in-Unity
            DateTime now = DateTime.Now;
            TimeSpan delta = now - lastUpdateAt;
            lastUpdateAt = now;
            
            if (delta.Milliseconds >= 0.01F)
            {
                Color color = Item.color;
                if (color.a >= 1.0F)
                {
                    Item.color = originalColor;
                    run = false;
                    return;
                }
                Color withNewAlpha = new Color(color.a, color.g, color.b, color.a + (alphaDelta * delta.Milliseconds));
                Item.color = withNewAlpha;
            }
        }
    }
}