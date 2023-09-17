using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

/// <summary>
/// CANNOT MOVE, NEITHER ATTACK, IF HITTEN N" TIMES IT WILL AWAKE
/// </summary>
[HideMonoScript]
public class AsleepStatusAffector : StatusAffector
{
    // Private (Variables) [START]
    [ShowInInspector]
    [HideInEditorMode]
    [ReadOnly]
    private int attacksReceived = 0;
    // Private (Variables) [END]

    // (Unity) Methods [START]
    protected override void Update()
    {
        base.Update();

        HandleAgentAwakening();
    }
    // (Unity) Methods [END]

    // Private (Methods) [START]
    private void ResetProperties()
    {
        attacksReceived = 0;
    }
    private void HandleAgentAwakening()
    {
        if (attacksReceived >= statusAffectorSO.specialCondition)
        {
            Finished = true;
        }
    }
    // Private (Methods) [END]

    // Protected (Methods) [START]
    protected override void ExecuteTurnActions()
    {
        base.ExecuteTurnActions();
    }
    protected override void InitializeStatusActions()
    {
        Target.AddMovementPrevention();
        Target.AddAttackPrevention();
        Target.onReceiveDamageAction += IncreaseAttacksReceived;
    }
    protected override void FinishStatusActions()
    {
        Target.RemoveMovementPrevention();
        Target.RemoveAttackPrevention();
        Target.onReceiveDamageAction -= IncreaseAttacksReceived;

        ResetProperties();
    }
    // Protected (Methods) [END]


    // Public (Methods) [START]
    public void IncreaseAttacksReceived() => attacksReceived++;
    // Public (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////