using System;
using UnityEngine;
using UnityEngine.UI;

namespace CornTheory.UI
{
    public class AlphaFader : MonoBehaviour
    {
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

        private DateTime startedAt;
        private DateTime lastUpdateAt;
        private float currentAlpha = 0.0F;
        private float alphaDelta = 0.0F;
        private bool run = false;

        public void StartFading(float alphaAdjustTo, int durationMS)
        {
            // 1 - calculate the amount of adjustment to make
            //       formula of # milliseconds and total AlphaAdjustment
            //        eg:  10MS and 10 alpha would be 1 alpha per 1 MS
            // 2 - start the process
            Debug.Log("TODO");
        }

        public void StartFading()
        {
            StartFading(AlphaAdjustTo, DurationMS);
        }

        private void Start()
        {
            lastUpdateAt = DateTime.Now;
            startedAt = lastUpdateAt;
            alphaDelta = AlphaAdjustTo / DurationMS;
            // Color color = Item.material.color;
            // Item.material.color = new Color(color.a, color.g, color.b, 0);
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
                Color color = Item.material.color;
                if (color.a >= 1.0F)
                {
                    run = false;
                    return;
                }
                Color withNewAlpha = new Color(color.a, color.g, color.b, color.a + alphaDelta);
                Debug.Log($"changing alpha to {withNewAlpha.a} from {color.a}");
                Item.material.color = withNewAlpha;
            }
        }
    }
}