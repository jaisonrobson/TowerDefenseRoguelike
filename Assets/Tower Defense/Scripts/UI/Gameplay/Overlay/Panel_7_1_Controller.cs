using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

[HideMonoScript]
public class Panel_7_1_Controller : MonoBehaviour
{
	// (Unity) Methods [START]
    private void OnEnable()
    {
        PlayerManager.instance.LockPlayerControl();
        GameManager.instance.SetTimeScale(0.05f);
    }
    private void OnDisable()
    {
        PlayerManager.instance.UnlockPlayerControl();
        GameManager.instance.ResetTimeScale();
    }
    // (Unity) Methods [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////