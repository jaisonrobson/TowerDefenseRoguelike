using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Core.General;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class GoalFlagManager : MonoBehaviour
{
    // (Unity) Methods [START]
    private void Update()
    {
        UpdateMeshRendererMaterial();
        HandleFlagRendererVisibility();
    }
    // (Unity) Methods [END]

    // Private (Methods) [START]
    private void UpdateMeshRendererMaterial()
    {
        List<AlignmentMaterialsSO> amSOs = Resources.LoadAll<AlignmentMaterialsSO>("SO's/Alignment Materials").ToList();

        AlignmentEnum ae = GetComponentInParent<Agent>().Alignment;
        
        AlignmentMaterialsSO alignmentMaterial = amSOs.Where(am => am.alignment.alignment == ae).FirstOrDefault();

        if (alignmentMaterial != null)
            GetComponent<MeshRenderer>().material = alignmentMaterial.ghost_structures;
    }
    private void HandleFlagRendererVisibility()
    {
        GetComponent<MeshRenderer>().enabled = IsSelected();
    }
    private bool IsSelected() => SelectionManager.instance.SelectedAgents.Any(sl => Utils.IsGameObjectInsideAnother(transform, sl.transform));
    // Private (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////