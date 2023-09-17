using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

/// <summary>
/// CANNOT MOVE (BUT CAN ATTACK)
/// </summary>
[HideMonoScript]
public class GroundedStatusAffector : StatusAffector
{
    // Protected (Methods) [START]
    protected override void ExecuteTurnActions()
    {
        base.ExecuteTurnActions();
    }
    protected override void InitializeStatusActions()
    {
        Target.AddMovementPrevention();
    }
    protected override void FinishStatusActions()
    {
        Target.RemoveMovementPrevention();
    }
    // Protected (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////