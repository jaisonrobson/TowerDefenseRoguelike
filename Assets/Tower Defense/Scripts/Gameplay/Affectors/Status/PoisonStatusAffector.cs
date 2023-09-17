using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

/// <summary>
/// DAMAGED PER TURN BY CONFIGURED AMOUNT AND DURATION
/// </summary>
[HideMonoScript]
public class PoisonStatusAffector : StatusAffector
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