using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using TheraBytes.BetterUi;

[HideMonoScript]
public class Panel_1_1_1_Text_Points_Controller : MonoBehaviour
{
    // Private (Variables) [START]
    private BetterText pointsText;
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
        pointsText = GetComponent<BetterText>();
    }
    private void HandleTextRefresh()
    {
        pointsText.text = PlayerManager.instance.Points.ToString();
    }
    // Private (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////