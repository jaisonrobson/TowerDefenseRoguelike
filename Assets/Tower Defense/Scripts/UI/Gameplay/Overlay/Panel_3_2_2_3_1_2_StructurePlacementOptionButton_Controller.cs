using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using TheraBytes.BetterUi;
using UnityEngine.UI;

[HideMonoScript]
public class Panel_3_2_2_3_1_2_StructurePlacementOptionButton_Controller : MonoBehaviour
{
    // Public (Variables) [START]
    public PlayableAgentSO playableAgentSO;
    [Required]
    public Image buttonImage;
    // Public (Variables) [END]

    // (Unity) Methods [START]
    private void Start()
    {
        RefreshButtonImage();
    }
    // (Unity) Methods [END]

    // Public (Methods) [START]
    public void RefreshButtonImage()
    {
        buttonImage.overrideSprite = playableAgentSO.agent.image;
    }
    public void SelectPlacementStructure()
    {
        StructurePlacementController.instance.SelectPlacementStructure(playableAgentSO);
    }
    // Public (Methods) [END]

}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////