using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.General;
using Sirenix.OdinInspector;
using System.Linq;

[ManageableData]
public class AlignmentMaterialsSO : BaseOptionDataSO
{
	[BoxGroup("Identity", LabelText = "Identity")]
	[Required]
	[ValidateInput("Validate_NotRepeated_Alignment", "[Alignment] Found AlignmentMaterialsSO with repeated alignment.")]
	public AlignmentSO alignment;

	[Required]
	public Material ghost_structures;

	[Required]
	public Material area_highlighting;

	// Validation Methods [START]
#if UNITY_EDITOR
	private bool Validate_NotRepeated_Alignment()
	{
		List<AlignmentMaterialsSO> existingSOs = Utils.FindAssetsByType<AlignmentMaterialsSO>();

		return !existingSOs.Any(so => so != this && so.alignment == alignment);
	}
#endif
	// Validation Methods [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////