using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using Core.Patterns;

/// <summary>
/// TAKES DAMAGE PER TURN
/// </summary>
[HideMonoScript]
public class BurnStatusAffector : StatusAffector
{
    // Protected (Methods) [START]
    protected override void ExecuteTurnActions()
    {
        base.ExecuteTurnActions();

        Target.OnReceiveDamageByStatus(Alignment, Damage, statusAffectorSO, Invoker);
    }
    protected override void InitializeStatusActions()
    {
        
    }
    protected override void FinishStatusActions()
    {
        
    }
    // Protected (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////