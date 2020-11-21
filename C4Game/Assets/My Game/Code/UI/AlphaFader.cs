using UnityEngine;

namespace CornTheory.UI
{
    public class AlphaFader : MonoBehaviour
    {
        /// <summary>
        /// UI game object to affect the change
        /// TODO: should this be a different type?
        /// </summary>
        [SerializeField] private GameObject Item;
        /// <summary>
        /// how much to adjust Alpha in the duration.  Obviously, 255 is max
        /// it should something small like 10
        /// </summary>
        [SerializeField] private int AlphaAdjustment;
        /// <summary>
        /// the number if milliseconds to incrementally adjust alpha.
        /// </summary>
        [SerializeField] private int DurationMS;

        public void StartAdjustment(int alphaAdjustment, int durationMS)
        {
            // 1 - calculate the amount of adjustment to make
            //       formula of # milliseconds and total AlphaAdjustment
            //        eg:  10MS and 10 alpha would be 1 alpha per 1 MS
            // 2 - start the process
            Debug.Log("TODO");
        }

        public void StartAdjustment()
        {
            StartAdjustment(AlphaAdjustment, DurationMS);
        }

        private void FixedUpdate()
        {
            // https://owlcation.com/stem/How-to-fade-out-a-GameObject-in-Unity
            
            
        }
    }
}