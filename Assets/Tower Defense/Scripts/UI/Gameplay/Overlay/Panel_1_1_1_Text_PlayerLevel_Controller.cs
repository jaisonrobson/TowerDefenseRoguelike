using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using TheraBytes.BetterUi;

[HideMonoScript]
public class Panel_1_1_1_Text_PlayerLevel_Controller : MonoBehaviour
{
    // Private (Variables) [START]
    private BetterText playerLevelText;
    // Private (Variables) [END]

    // (Unity) Methods [START]
    void OnEnable()
    {
        ResetVariables();
    }

    void Update()
    {
        HandleTextRefresh();
    }
    // (Unity) Methods [END]


    // Private (Methods) [START]
    private void ResetVariables()
    {
        playerLevelText = GetComponent<BetterText>();
    }
    private void HandleTextRefresh()
    {
        playerLevelText.text = "Level "+PlayerManager.instance.Level.ToString();
    }
    // Private (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////