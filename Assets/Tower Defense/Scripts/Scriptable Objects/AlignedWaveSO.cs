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
	public WaveSO wave;
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////