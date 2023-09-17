using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
using Core.Patterns;
using Core.Physics;

[HideMonoScript]
public class StructurePlacementController : Singleton<StructurePlacementController>
{
    // Public (Variables) [START]
    [Required]
    public LayerMask groundLayer;
    [Required]
    public LayerMask structureAreaLayer;
    // Public (Variables) [END]

    // Private (Variables) [START]
    private Camera mainCamera;
    private GameObject currentPlacingStructure;
    private PlayableAgentSO currentPlacingPlayableAgentSO;
    private List<PlayableAgentSO> levelStructures = new List<PlayableAgentSO>();
    // Private (Variables) [END]

    // Public (Properties) [START]
    public bool IsPlacing { get { return currentPlacingStructure != null; } }
    public PlayableAgentSO CurrentPlacingPlayableAgent { get { return currentPlacingPlayableAgentSO; } }
    public bool CanBuyStructure { get { return currentPlacingPlayableAgentSO != null && PlayerManager.instanceExists && PlayerManager.instance.CanDecreasePoints(currentPlacingPlayableAgentSO.cost); } }
    // Public (Properties) [END]

    // (Unity) Methods [START]
    void Start()
    {
        mainCamera = Camera.main;

        InitializeLevelStructures();
    }

    void FixedUpdate()
    {
        if (!PlayerManager.instance.IsLockingPlayerControl)
        {
            HandleStructureWorldPositioning();
            HandleInput();
        }
    }

    void OnDrawGizmos()
    {
        if (currentPlacingStructure != null)
        {
            Vector3 pos = currentPlacingStructure.transform.position;
            pos.y += 1f;

            Gizmos.DrawRay(pos, Vector3.down);
        }
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
    private void HandleInput()
    {
        HandleGhostInstantiation();
        HandlePlacementCancelling();
        HandleStructurePlacement();
    }
    private void HandleStructureWorldPositioning()
    {
        if (currentPlacingStructure != null && currentPlacingStructure.activeInHierarchy)
        {
            RaycastHit hit = Raycasting.ScreenPointToRay(groundLayer);

            if (!Raycasting.IsHitEmpty(hit))
            {
                currentPlacingStructure.transform.position = hit.point;
            }
        }
    }
    private void HandleStructurePlacement()
    {
        if (currentPlacingStructure != null && currentPlacingStructure.activeInHierarchy)
        {
            //Placement
            if (Input.GetMouseButtonUp(0) && !OverlayInterfaceManager.instance.IsOverUI())
            {
                Vector3 structureGhostPosition = currentPlacingStructure.transform.position;
                structureGhostPosition.y += 1000f;

                if (Physics.Raycast(structureGhostPosition, Vector3.down, out RaycastHit hitInfo, Mathf.Infinity, structureAreaLayer, QueryTriggerInteraction.Collide))
                {
                    if (hitInfo.collider.gameObject.GetComponent<PlacementArea>().alignment == MapManager.instance.map.playerAlignment.alignment)
                    {
                        if (currentPlacingStructure.GetComponentInChildren<StructurePlacementCollisionManager>().CanPlaceStructure())
                        {
                            if (CanBuyStructure)
                            {
                                PlayerManager.instance.DecreasePoints(currentPlacingPlayableAgentSO.cost);

                                currentPlacingStructure.GetComponent<Agent>().Alignment = MapManager.instance.map.playerAlignment.alignment;
                                currentPlacingStructure.GetComponent<PlayableStructure>().PlaceStructure();
                                ResetPlacementVariables();
                            }
                        }
                    }
                }
            }
        }
    }
    private void HandlePlacementCancelling()
    {
        if (currentPlacingStructure != null)
        {
            if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Mouse1) || !currentPlacingStructure.activeInHierarchy)
            {
                ResetPlacement();
            }
        }
    }
    private void HandleGhostInstantiation()
    {
        for (int i = 1; i < levelStructures.Count+1; i++)
        {   
            if (Input.GetKey(i.ToString()))
            {
                SelectStructure(levelStructures.ElementAt(i-1));
            }
        }
    }
    private void ResetPlacement()
    {
        Poolable.TryPool(currentPlacingStructure);

        ResetPlacementVariables();
    }
    private void ResetPlacementVariables()
    {
        currentPlacingStructure = null;
        currentPlacingPlayableAgentSO = null;
        CursorController.instance.ResetCursorModeAndCommand();
    }
    private void InstantiateGhost(GameObject structure)
    {
        currentPlacingStructure = Poolable.TryGetPoolable(structure);
    }
    private void SelectStructure(PlayableAgentSO pPASO)
    {
        if (currentPlacingStructure != null)
        {
            ResetPlacement();
        }

        InstantiateGhost(pPASO.agent.prefab);
        currentPlacingPlayableAgentSO = pPASO;

        PlayerCommandsManager.instance.Command = PlayerCommandEnum.CASTING;
        CursorManager.instance.Mode = CursorModeEnum.CASTING_CONSTRUCTION;
    }
    // Private Methods [END]

    // Public (Methods) [START]
    public void SelectPlacementStructure(PlayableAgentSO pPASO)
    {
        SelectStructure(pPASO);
    }
    // Public (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////