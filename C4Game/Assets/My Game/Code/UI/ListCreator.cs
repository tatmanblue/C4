using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CornTheory.UI
{
    /// <summary>
    /// This is a generic handler for adding items to ScrollView
    /// </summary>
    public class ListCreator : MonoBehaviour
    {
        /// <summary>
        /// The sound to make for a keypress
        /// </summary>
        [SerializeField] private AudioClip AudioClip;
        /// <summary>
        /// Required for playing the sound
        /// </summary>
        [SerializeField] private AudioSource AudioSource;
        /// <summary>
        /// GameObject in the Content (ScrollView/ViewPort/Content) to attach
        /// the new list item to 
        /// </summary>
        [SerializeField] private Transform SpawnPoint = null;
        /// <summary>
        /// The Content game object (ScrollView/ViewPort/Content) which holds the list
        /// of items
        /// </summary>
        [SerializeField] private RectTransform Content = null;
        private int numberOfItems = 0;

        public T AddItemToHistory<T>(GameObject listItem, int height = 50)
        {
            GameObject spawnedItem = StartItemToHistory(listItem, height);
            PlayReceivedIndicatorSound();
            return spawnedItem.GetComponent<T>();
        }

        /// <summary>
        /// Plays sound clip AudioClip
        /// </summary>
        /// <param name="ms"></param>
        public void PlayReceivedIndicatorSound(int ms = 250)
        {
            // TODO: make this delay data driven
            StartCoroutine(PlaySound(250));
        }

        /// <summary>
        /// As long as multiple items do not call this method at the same time we are ok.
        /// TODO: make blocking
        /// </summary>
        /// <param name="listItem"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public GameObject StartItemToHistory(GameObject listItem, int height = 50)
        {
            int itemCount = numberOfItems;
            numberOfItems++;
            
            Content.sizeDelta = new Vector2(0, itemCount * height);
            float spawnY = itemCount * height;

            // taken from https://www.codeneuron.com/creating-a-dynamic-scrollable-list-in-unity/
            // one difference is we are setting x to 0, always.  not sure why that has to be different
            Vector3 pos = new Vector3(0, -spawnY, SpawnPoint.position.z);
            GameObject spawnedItem = Instantiate(listItem, pos, SpawnPoint.rotation);
            spawnedItem.transform.SetParent(SpawnPoint, false);

            return spawnedItem;
        }
        
        private IEnumerator PlaySound(float ms)
        {
            float waitMS = ms / 1000F;
            yield return new WaitForSeconds(waitMS);
            AudioSource.PlayOneShot(AudioClip);
        }
    }
}