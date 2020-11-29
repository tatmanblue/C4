using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using CornTheory.Interfaces;

namespace CornTheory.UI
{
    /// <summary>
    /// Fades In (using alpha channel) an image and the text of a TextMeshProUGUI in unison
    ///
    /// Recommendation is this class goes on a UI button control.  It must have TextMeshProUGUI for text, however.
    /// This class can be attached to any UI object, however.
    /// </summary>
    public class ButtonAlphaFader : MonoBehaviour, IFader
    {
        public event CompletedFading OnFadingComplete;
        
        [SerializeField] private Image Image;
        [SerializeField] private TextMeshProUGUI Text;
        
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
        private Color originalButtonColor;
        private Color originalTextColor;
        
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
            
            originalButtonColor = Image.color;
            Image.color = new Color(originalButtonColor.a, originalButtonColor.g, originalButtonColor.b, 0);

            originalTextColor = Text.color;
            Text.color = new Color(originalTextColor.a, originalTextColor.g, originalTextColor.b, 0);
            
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
                Color color = Image.color;
                if (color.a >= 1.0F)
                {
                    Image.color = originalButtonColor;
                    run = false;
                    return;
                }
                Color withNewAlpha = new Color(color.a, color.g, color.b, color.a + (alphaDelta * delta.Milliseconds));
                Image.color = withNewAlpha;
                
                color = Text.color;
                withNewAlpha = new Color(color.a, color.g, color.b, color.a + (alphaDelta * delta.Milliseconds));
                Text.color = withNewAlpha;
            }
        }        
    }
}