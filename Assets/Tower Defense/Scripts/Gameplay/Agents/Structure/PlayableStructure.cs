using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Core.Patterns;

[RequireComponent(typeof(PlayableStructureFsmAi))]
public class PlayableStructure : Structure
{
    [BoxGroup("Structure Identity")]
    [Required]
    public GameObject structure;
    [BoxGroup("Structure Identity")]
    [Required]
    public GameObject ghost;
    // Public (Variables) [END]

    // Private (Variables) [START]
    [HideInEditorMode]
    private bool isPlaced = false;
    private PlacementArea placementArea = null;
    // Private (Variables) [END]

    // Public (Properties) [START]
    public bool IsPlaced { get { return isPlaced; } }
    public bool HasEvolution { get { return agent.evolutionTree.Count > 0; } }
    public bool IsMultipleEvolution { get { return agent.evolutionTree.Count > 1; } }
    public PlacementArea PlacementArea { get { return placementArea; } }
    // Public (Properties) [END]

    // (Unity) Methods [START]
    protected override void Start()
    {
        base.Start();

        InitializeVariables();
    }
    protected override void Update()
    {
        base.Update();

        HandleStructurePlacement();
    }
    // (Unity) Methods [END]

    // Private Methods [START]
    private void HandleStructurePlacement()
    {
        if (isPlaced)
        {
            if (!structure.gameObject.activeSelf)
            {
                ghost.SetActive(false);

                structure.SetActive(true);
            }
        }
        else
        {
            if (!ghost.gameObject.activeSelf)
            {
                structure.SetActive(false);

                ghost.SetActive(true);
            }
        }
    }
    private void ResetStructurePlacement()
    {
        placementArea = null;
        isPlaced = false;
        structure.SetActive(false);
        ghost.SetActive(true);
    }
    private void InitializeVariables()
    {
    }    
    // Private Methods [END]

    // Public (Methods) [START]
    public void PlaceStructure(PlacementArea pPlacementArea)
    {
        isPlaced = true;
        placementArea = pPlacementArea;

        pPlacementArea.CurrentOccupyingAgent = this;

        DoSpawnFXs();
    }
    public override void PoolRetrievalAction(Poolable poolable)
    {
        ResetStructurePlacement();

        base.PoolRetrievalAction(poolable);
    }
    public override void PoolInsertionAction(Poolable poolable)
    {
        if (placementArea != null)
            placementArea.CurrentOccupyingAgent = null;

        GetComponentInChildren<StructurePlacementCollisionManager>(true).PoolInsertionAction();

        ResetStructurePlacement();

        base.PoolInsertionAction(poolable);
    }
    // Public (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////