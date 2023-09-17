using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using TheraBytes.BetterUi;

[HideMonoScript]
public class Panel_3_2_2_1_1_SelectedPlacementStructureStatistics_Controller : MonoBehaviour
{
    // Public (Variables) [START]
    [Required]
    public GameObject panel;
    [Required]
    public BetterText title;
    [Required]
    public BetterText damage;
    [Required]
    public BetterText attackVelocity;
    [Required]
    public BetterText evasion;
    [Required]
    public BetterText attackRange;
    [Required]
    public BetterText visibilityRange;
    [Required]
    public BetterText minionsQuantity;
    [Required]
    public BetterText cost;
    // Public (Variables) [END]

    // (Unity) Methods [START]
    void Update()
    {
        HandleInformationRefresh();
        HandlePanelVisibility();
    }
    // (Unity) Methods [END]


    // Private (Methods) [START]
    private void HandlePanelVisibility()
    {
        panel.SetActive(StructurePlacementController.instance.IsPlacing);
    }
    private void HandleInformationRefresh()
    {
        PlayableAgentSO currentPlacingAgent = StructurePlacementController.instance.CurrentPlacingPlayableAgent;

        if (currentPlacingAgent != null)
        {
            title.text = currentPlacingAgent.agent.name;
            damage.text = currentPlacingAgent.agent.damage.ToString();
            attackVelocity.text = currentPlacingAgent.agent.attackVelocity.ToString();
            evasion.text = currentPlacingAgent.agent.evasion.ToString();
            attackRange.text = currentPlacingAgent.agent.attackRange.ToString();
            visibilityRange.text = currentPlacingAgent.agent.visibilityArea.ToString();
            minionsQuantity.text = currentPlacingAgent.agent.subspawns.Aggregate(0, (int acc, SubSpawnSO value) => acc += value.maxAlive, result => result.ToString());
            cost.text = currentPlacingAgent.cost.ToString();
        }
    }
    // Private (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////