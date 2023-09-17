using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

[HideMonoScript]
public class Panel_3_2_2_3_1_2_StructurePlacementOptions_ButtonsCreation : MonoBehaviour
{
    // Public (Variables) [START]
    [Required]
    [AssetsOnly]
    public GameObject buttonPrefab;
    // Public (Variables) [END]

    // Private (Variables) [START]
    private bool didCreatedButtons = false;
    // Private (Variables) [END]

    // (Unity) Methods [START]
    public void Start()
    {
        CreateButtons();
    }
    // (Unity) Methods [END]


    // Private (Variables) [START]
    private void CreateButtons()
    {
        if (didCreatedButtons)
            return;

        PlayableAgentSO[] pAgents = MapManager.instance.map.playableAgents;

        foreach (PlayableAgentSO pAgt in pAgents)
        {
            if (pAgt.agent.type == AgentTypeEnum.STRUCTURE)
            {
                GameObject buttonObject = Instantiate(buttonPrefab);
                Panel_3_2_2_3_1_2_StructurePlacementOptionButton_Controller button = buttonObject.GetComponentInChildren<Panel_3_2_2_3_1_2_StructurePlacementOptionButton_Controller>();

                button.playableAgentSO = pAgt;

                button.RefreshButtonImage();

                buttonObject.transform.SetParent(transform);
            }
        }

        didCreatedButtons = true;
    }
    // Private (Variables) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////