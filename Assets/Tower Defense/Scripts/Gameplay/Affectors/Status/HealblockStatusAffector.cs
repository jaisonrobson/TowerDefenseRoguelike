using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

/// <summary>
/// CANNOT HEAL OR BE HEALED FOR THE DURATION OF THE EFFECT
/// </summary>
[HideMonoScript]
public class HealblockStatusAffector : StatusAffector
{
    // Protected (Methods) [START]
    protected override void ExecuteTurnActions()
    {
        base.ExecuteTurnActions();
    }
    protected override void InitializeStatusActions() { }
    protected override void FinishStatusActions() { }
    // Protected (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////