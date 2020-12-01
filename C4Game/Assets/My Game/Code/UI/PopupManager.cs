using UnityEngine;

namespace CornTheory.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class PopupManager : MonoBehaviour
    {
        [SerializeField] private GameObject Parent = null;
        
        public void ShowPopup(GameObject which)
        {
            if (null == which)
                return;

            var popup = Instantiate(which) as GameObject;
            popup.SetActive(true);
            popup.transform.localScale = new Vector3(1, 1, 1);

            if (null != Parent)
                popup.transform.SetParent(Parent.transform, false);

            PopupHandler handler = popup.GetComponent<PopupHandler>();
            if (!handler)
            {
                Debug.LogWarning($"{which.name} is missing PopupHandler class.");
                return;
            }
            
            handler.Open(Parent);
        }
    }
}