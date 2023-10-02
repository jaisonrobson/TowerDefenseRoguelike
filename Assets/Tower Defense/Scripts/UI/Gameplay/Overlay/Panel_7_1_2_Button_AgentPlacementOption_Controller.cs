using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using TheraBytes.BetterUi;

[HideMonoScript]
public class Panel_7_1_2_Button_AgentPlacementOption_Controller : MonoBehaviour
{
    // Public (Variables) [START]
    [Required]
    public BetterImage agentImage;
    [Required]
    public BetterText agentName;
    [Required]
    public BetterText agentValue;
    [Required]
    public BetterText health;
    [Required]
    public BetterText damage;
    [Required]
    public BetterText attackSpeed;
    [Required]
    public BetterText attackRange;
    [Required]
    public BetterText visibilityRange;
    // Public (Variables) [END]

    // Private (Variables) [START]
    private PlayableAgentSO playableAgentSO;
    // Private (Variables) [END]

    // Public (Properties) [START]
    public PlayableAgentSO PlayableAgentSO { get { return playableAgentSO; } set { playableAgentSO = value; } }
    // Public (Properties) [END]

    // (Unity) Methods [START]
    void Start()
    {
        
    }
    void Update()
    {
        HandleInformationRefresh();
    }
    // (Unity) Methods [END]

    // Private (Methods) [START]
    private void HandleInformationRefresh()
    {
        if (playableAgentSO == null)
            return;

        agentImage.sprite = playableAgentSO.agent.image;
        agentValue.text = playableAgentSO.cost.ToString();
        agentName.text = playableAgentSO.agent.name;
        health.text = playableAgentSO.agent.health.ToString();
        damage.text = playableAgentSO.agent.damage.ToString();
        attackSpeed.text = playableAgentSO.agent.attackVelocity.ToString();
        attackRange.text = playableAgentSO.agent.attackRange.ToString();
        visibilityRange.text = playableAgentSO.agent.visibilityArea.ToString();
    }
    // Private (Methods) [END]

    // Public (Methods) [START]
    public void OnClick()
    {
        if (AgentPlacementController.instance.CanBuyAgent(playableAgentSO))
        {
            AgentPlacementController.instance.PlaceAgent(playableAgentSO);        
            OverlayInterfaceManager.instance.CloseAgentPlacementPanel();
        }
    }
    // Public (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////