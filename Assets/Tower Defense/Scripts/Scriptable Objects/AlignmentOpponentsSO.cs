using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.General;
using Sirenix.OdinInspector;
using System.Linq;

[ManageableData]
public class AlignmentOpponentsSO : BaseOptionDataSO
{
	[BoxGroup("Identity", LabelText = "Identity")]
	[Required]
	public AlignmentSO alignment;

	/// <summary>
	/// A collection of other alignment objects that we can harm
	/// </summary>
	[ListDrawerSettings(Expanded = true)]
	[PropertySpace(5f)]
	[ValidateInput("Validate_MustHaveElements_Opponents", "Opponents must have at least one element.")]
	[ValidateInput("Validate_NotEqualAlignment_Opponents", "Opponents list must not contain the own aligment as opponent.")]
	[ValidateInput("Validate_NotEqualElements_Opponents", "Opponents list must not contain duplicated elements.")]
	[ValidateInput("Validate_NotInvalidElements_Opponents", "Opponents list must not contain invalid elements.")]
	public List<AlignmentSO> opponents;


	// Methods [START]
	/// <summary>
	/// Gets whether the given alignment is in our known list of opponents
	/// </summary>
	public bool CanHarm(AlignmentSO other)
	{
		if (other == null)
		{
			return false;
		}

		return other != null && opponents.Contains(other);
	}
	// Methods [END]


	// Validation Methods [START]
	private bool Validate_MustHaveElements_Opponents() { return opponents != null && opponents.Count > 0; }
	private bool Validate_NotEqualAlignment_Opponents()
	{
		if (opponents == null) return true;

		return opponents.Where(op => op == alignment).ToList().Count == 0;
	}
	private bool Validate_NotEqualElements_Opponents()
	{
		if (opponents == null) return true;

		bool result = true;

		opponents.ToList().ForEach(
			a => {
				if (!result) return;

				if (opponents.ToList().Where(x => x == a).Count() > 1)
					result = false;
			}
		);

		return result;
	}

    private bool Validate_NotInvalidElements_Opponents()
	{
		if (opponents == null) return true;

		return opponents.Where(o => o == null).ToList().Count == 0;
	}
	// Validation Methods [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////