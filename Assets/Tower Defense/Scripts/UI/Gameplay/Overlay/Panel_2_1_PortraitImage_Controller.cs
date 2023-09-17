using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Panel_2_1_PortraitImage_Controller : MonoBehaviour
{
    // Private (Variables) [START]
    private Image image;
    // Private (Variables) [END]

    // (Unity) Methods [START]
    private void Start()
    {
        InitializeVariables();
    }
    private void Update()
    {
        HandlePortraitUpdate();
    }
    // (Unity) Methods [END]


    // Private (Methods) [START]
    private void InitializeVariables()
    {
        image = GetComponent<Image>();
    }
    private void HandlePortraitUpdate()
    {
        if (SelectionManager.instance.IsAnythingSelected)
            image.sprite = SelectionManager.instance.SelectedAgents.Last().GetComponent<Agent>().GetAgent().image;
        else
            image.sprite = null;
    }
    // Private (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////