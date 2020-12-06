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
        
        private void Start()
        {
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

            // TODO: need to get this from the settings
            ScreenResolutions.SetValueWithoutNotify(6);
            
            ScreenResolutions.onValueChanged.AddListener(delegate
            {
                ScreenResolutionValueChanged(ScreenResolutions.value);
            });
        }

        private void ScreenResolutionValueChanged(int index)
        {
            // TODO: need to save settings
            ScreenResolution item = list[index];
            Debug.Log($"changing resolution to {item.Display}");
            Screen.SetResolution(item.Width, item.Height, true);
        }
    }
}