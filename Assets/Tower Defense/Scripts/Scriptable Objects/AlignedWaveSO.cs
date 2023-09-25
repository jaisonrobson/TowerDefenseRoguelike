using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.General;
using Sirenix.OdinInspector;
using System.Linq;

[ManageableData]
public class AlignedWaveSO : BaseOptionDataSO
{
	[Required]
	public AlignmentSO alignment;

	[Required]
	[ListDrawerSettings(Expanded = true)]
	[ValidateInput("Validate_MustHaveElements_Waves", "Waves must have at least one element.")]
	public WaveSO[] waves;


	// (Validation) Methods [START]
	private bool Validate_MustHaveElements_Waves() { return waves != null && waves.Length > 0; }
	// (Validation) Methods [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////