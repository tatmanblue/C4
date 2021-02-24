using System.Collections.Generic;
using TMPro;
using UnityEngine;

using CornTheory.Data;

namespace CornTheory.UI.Dialogs
{
    public class SettingsDialog : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown ScreenResolutions;
        private List<ScreenResolution> list = null;
        private GlobalSettings settings = null;
        
        private void Start()
        {
            settings = GlobalSettings.Load();
            list = GlobalSettings.GetAllResolutions();
            ScreenResolutions.options.Clear();
            foreach (ScreenResolution item in list)
            {
                ScreenResolutions.options.Add(new TMP_Dropdown.OptionData() 
                    {
                        text = item.Display
                    }
                );
            }

            // settings saves the id #.  Currently the list is sorted by ID therefore the index
            // will be id - 1
            ScreenResolutions.SetValueWithoutNotify(settings.ResolutionId - 1);
            
            ScreenResolutions.onValueChanged.AddListener(delegate
            {
                ScreenResolutionValueChanged(ScreenResolutions.value);
            });
        }

        private void ScreenResolutionValueChanged(int index)
        {
            ScreenResolution item = list[index];
            Debug.Log($"changing resolution to {item.Width}x{item.Height} (using index {index})");
            Screen.SetResolution(item.Width, item.Height, true);
            
            // TODO: longer term there should be cancel/revert button in which changes are reverted
            settings.ResolutionId = item.Id;
            settings.Save();
        }
    }
}