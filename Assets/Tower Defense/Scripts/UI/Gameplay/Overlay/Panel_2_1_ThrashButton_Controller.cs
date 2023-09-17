using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

[HideMonoScript]
public class Panel_2_1_ThrashButton_Controller : MonoBehaviour
{

    // Public (Methods) [START]
    public void DestroyStructure()
    {
        PlayableStructure ps = SelectionManager.instance.SelectedAgents.First()?.GetComponent<PlayableStructure>();

        if (ps != null)
        {
            ps.PoolAgent();

            SelectionManager.instance.ClearSelectables();
        }
    }
    // Public (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////