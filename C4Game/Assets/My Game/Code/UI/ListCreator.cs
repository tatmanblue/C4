using UnityEngine;

namespace CornTheory.UI
{
    /// <summary>
    /// This is a generic handler for adding items to ScrollView
    /// </summary>
    public class ListCreator : MonoBehaviour
    {
        [SerializeField] private Transform SpawnPoint = null;
        [SerializeField] private RectTransform Content = null;
        private int numberOfItems = 0;

        public T AddItemToHistory<T>(GameObject listItem, int height = 60)
        {
            Content.sizeDelta = new Vector2(0, numberOfItems * height);
            
            float spawnY = numberOfItems * height;

            // taken from https://www.codeneuron.com/creating-a-dynamic-scrollable-list-in-unity/
            // one difference is we are setting x to 0, always.  not sure why that has to be different
            Vector3 pos = new Vector3(0, -spawnY, SpawnPoint.position.z);
            GameObject spawnedItem = Instantiate(listItem, pos, SpawnPoint.rotation);
            spawnedItem.transform.SetParent(SpawnPoint, false);
            
            numberOfItems ++;
            
            return spawnedItem.GetComponent<T>();
        }
    }
}