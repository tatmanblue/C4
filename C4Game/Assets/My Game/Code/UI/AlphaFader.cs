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
        [SerializeField] private int AlphaAdjustTo;
        /// <summary>
        /// the number if milliseconds to incrementally adjust alpha.
        /// </summary>
        [SerializeField] private int DurationMS;

        public void StartFading(int alphaAdjustTo, int durationMS)
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

        private void FixedUpdate()
        {
            // https://owlcation.com/stem/How-to-fade-out-a-GameObject-in-Unity
            
            
        }
    }
}