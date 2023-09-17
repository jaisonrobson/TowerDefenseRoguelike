using System.Collections.Generic;
using UnityEngine;
using Core.General;
using Sirenix.OdinInspector;
using System.Linq;

[ManageableData]
public class AlignmentSO : BaseOptionDataSO
{
	[BoxGroup("Identity", LabelText = "Identity")]
	[Required]
	public new string name;

	[BoxGroup("Identity")]
	[Required]
	[ValidateInput("Validate_NotRepeated_Alignment", "[Alignment] Found AlignmentSO with repeated alignment enumerator.")]
	[ValidateInput("Validate_NotValid_Alignment", "[Alignment] Invalid alignment declared, the first enum element is considered generic and not possible to be declared as alignment.")]
	public AlignmentEnum alignment;

	[BoxGroup("Identity")]
	[ValidateInput("Validate_NotRepeated_Color", "[Alignment] Found AlignmentSO with repeated alignment color.")]
	public Color color;

	// Validation Methods [START]
#if UNITY_EDITOR
	private bool Validate_NotRepeated_Alignment()
	{
		List<AlignmentSO> existingAlignmentSOs = Utils.FindAssetsByType<AlignmentSO>();

		return !existingAlignmentSOs.Any(easo => easo != this && easo.alignment == alignment);
	}
	private bool Validate_NotRepeated_Color()
	{
		List<AlignmentSO> existingAlignmentSOs = Utils.FindAssetsByType<AlignmentSO>();

		return !existingAlignmentSOs.Any(easo => easo != this && easo.color == color);
	}
#endif
	private bool Validate_NotValid_Alignment() { return alignment != 0; }
	// Validation Methods [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////