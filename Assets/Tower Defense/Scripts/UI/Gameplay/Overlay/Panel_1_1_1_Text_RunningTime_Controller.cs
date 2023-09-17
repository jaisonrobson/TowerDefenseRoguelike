using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using TheraBytes.BetterUi;

[HideMonoScript]
public class Panel_1_1_1_Text_RunningTime_Controller : MonoBehaviour
{
    // Private (Variables) [START]
    private BetterText watchtime;
    // Private (Variables) [END]

    // (Unity) Methods [START]
    void OnEnable()
    {
        ResetVariables();
    }

    void Update()
    {
        HandleWatchtimeRefresh();
    }
    // (Unity) Methods [END]


    // Private (Methods) [START]
    private void ResetVariables()
    {
        watchtime = GetComponent<BetterText>();
    }
    private void HandleWatchtimeRefresh()
    {
        watchtime.text = GameManager.instance.FormattedTime;
    }
    // Private (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////