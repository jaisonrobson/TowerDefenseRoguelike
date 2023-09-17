using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

[HideMonoScript]
public class UIDisplayToggle : MonoBehaviour
{
	public void ToggleUIDisplay(GameObject pUIObjectToToggle)
    {
        pUIObjectToToggle.SetActive(!pUIObjectToToggle.activeSelf);
    }
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////