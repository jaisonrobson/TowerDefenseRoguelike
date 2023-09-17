using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

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

    // (Unity) Methods [START]
    private void Update()
    {
        HandleAreaVisibility();
    }
    // (Unity) Methods [END]


    // Private (Methods) [START]
    private void HandleAreaVisibility()
    {
        if (StructurePlacementController.instance.IsPlacing)
        {
            trigger.enabled = true;
            mesh.SetActive(true);
        }
        else
        {
            trigger.enabled = false;
            mesh.SetActive(false);
        }
    }
    // Private (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////