using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

[HideMonoScript]
public class UIDisplayToggle : MonoBehaviour
{
    // Public (Variables) [START]
    public List<GameObject> cascadingDisplay;
    // Public (Variables) [END]


    // Public (Methods) [START]
    public void ToggleUIDisplay(GameObject pUIObjectToToggle)
    {
        pUIObjectToToggle.SetActive(!pUIObjectToToggle.activeSelf);
    }
    public void ToggleCascadingDisplay() {
        if (cascadingDisplay != null)
            cascadingDisplay.ForEach(obj => obj.SetActive(!obj.activeSelf));
    }
    // Public (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////