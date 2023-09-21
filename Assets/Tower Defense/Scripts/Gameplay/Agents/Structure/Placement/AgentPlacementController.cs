using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
using Core.Patterns;
using Core.Physics;

[HideMonoScript]
public class AgentPlacementController : Singleton<AgentPlacementController>
{
    // Public (Variables) [START]
    [Required]
    public LayerMask groundLayer;
    [Required]
    public LayerMask structureAreaLayer;
    // Public (Variables) [END]

    // Private (Variables) [START]
    private List<PlayableAgentSO> levelStructures = new List<PlayableAgentSO>();
    [ShowInInspector]
    [HideInEditorMode]
    private PlacementArea currentPlacementArea;
    // Private (Variables) [END]

    // Public (Properties) [START]
    public List<PlayableAgentSO> LevelPlayableStructures { get { return levelStructures; } }
    public PlacementArea CurrentPlacementArea { get { return currentPlacementArea; } set { currentPlacementArea = value; } }
    // Public (Properties) [END]

    // (Unity) Methods [START]
    void Start()
    {
        InitializeLevelStructures();
    }
    // (Unity) Methods [END]

    // Private Methods [START]
    private void InitializeLevelStructures()
    {
        PlayableAgentSO[] pAgents = MapManager.instance.map.playableAgents;

        foreach (PlayableAgentSO pAgt in pAgents)
        {
            if (pAgt.agent.type == AgentTypeEnum.STRUCTURE)
            {
                levelStructures.Add(pAgt);
            }
        }
    }
    // Private Methods [END]

    // Public (Methods) [START]
    public void PlaceAgent(PlayableAgentSO pAgent)
    {
        if (!CanBuyAgent(pAgent)) return;

        PlayerManager.instance.DecreasePoints(pAgent.cost);

        GameObject newAgent = Poolable.TryGetPoolable(pAgent.agent.prefab);

        newAgent.transform.position = currentPlacementArea.transform.position - new Vector3(0, 5f, 0);
        newAgent.GetComponent<Agent>().Alignment = MapManager.instance.map.playerAlignment.alignment;
        newAgent.GetComponent<PlayableStructure>().PlaceStructure(CurrentPlacementArea);

        currentPlacementArea.CurrentOccupyingAgent = newAgent.GetComponent<Agent>();
        currentPlacementArea = null;
    }
    public bool CanBuyAgent(PlayableAgentSO pAgent) => PlayerManager.instance.CanDecreasePoints(pAgent.cost);
    // Public (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////