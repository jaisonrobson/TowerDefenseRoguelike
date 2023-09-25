using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Core.Physics;

[HideMonoScript]
public class PlacementArea : MonoBehaviour
{
    // Public (Variables) [START]
    [Required]
    public AlignmentEnum alignment;
    [Required]
    public MeshCollider trigger;
    [Required]
    public GameObject mesh;
    // Public (Variables) [END]

    // Private (Variables) [START]
    private Agent currentOccupyingAgent;
    // Private (Variables) [END]

    // Public (Properties) [START]
    public Agent CurrentOccupyingAgent { get { return currentOccupyingAgent; } set { currentOccupyingAgent = value; } }
    // Public (Properties) [END]

    // (Unity) Methods [START]
    private void OnEnable()
    {
        InitializeVariables();
    }
    private void Update()
    {
        HandlePlacementSelection();
        HandlePlacementAreaVisibility();
    }
    // (Unity) Methods [END]


    // Private (Methods) [START]
    private void InitializeVariables()
    {
        currentOccupyingAgent = null;
    }
    private void HandlePlacementSelection()
    {
        if (OverlayInterfaceManager.instance.IsOverUI() || PlayerCommandsManager.instance.IsTryingToCast || PlayerCommandsManager.instance.HasInputDelay || CurrentOccupyingAgent != null)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit = Raycasting.ScreenPointToRay(AgentPlacementController.instance.structureAreaLayer);

            if (!Raycasting.IsHitEmpty(hit) && hit.collider.gameObject == gameObject)
            {
                AgentPlacementController.instance.CurrentPlacementArea = GetComponent<PlacementArea>();
                OverlayInterfaceManager.instance.OpenAgentPlacementPanel();
            }
        }
    }
    private void HandlePlacementAreaVisibility()
    {
        if (CurrentOccupyingAgent != null)
        {
            trigger.enabled = false;
            mesh.SetActive(false);
        }
        else
        {
            trigger.enabled = true;
            mesh.SetActive(true);
        }
    }
    // Private (Methods) [END]

    // Public (Methods) [START]

    // Public (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////