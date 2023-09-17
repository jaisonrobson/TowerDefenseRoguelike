using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using Core.Patterns;

/// <summary>
/// MOVES AND ATTACK SLOWLY OR CANNOT MOVE NEITHER ATTACK (DEPENDS ON THE CONFIGURATION)
/// </summary>
[HideMonoScript]
public class FreezeStatusAffector : StatusAffector
{
    // Private (Properties) [START]
    private float BC_AttackVelocity { get; set; }
    private float AC_AttackVelocity { get; set; }
    private float BC_Velocity { get; set; }
    private float AC_Velocity { get; set; }
    // Private (Properties) [END]

    // Private (Methods) [START]
    private void ResetProperties()
    {
        BC_AttackVelocity = 0f;
        AC_AttackVelocity = 0f;
        BC_Velocity = 0f;
        AC_Velocity = 0f;
    }
    // Private (Methods) [END]

    // Protected (Methods) [START]
    protected override void ExecuteTurnActions()
    {
        base.ExecuteTurnActions();
    }
    protected override void InitializeStatusActions()
    {
        BC_AttackVelocity = Target.AttackVelocity;
        BC_Velocity = Target.Velocity;
        AC_AttackVelocity = BC_AttackVelocity * Mathf.Abs((statusAffectorSO.influence / 100f) - 1f);
        AC_Velocity = BC_Velocity * Mathf.Abs((statusAffectorSO.influence / 100f) - 1f);

        Target.UpdateAgentVelocity(AC_Velocity);
        Target.UpdateAgentAttackVelocity(AC_AttackVelocity);
    }
    protected override void FinishStatusActions()
    {
        Target.UpdateAgentVelocity(Target.Velocity + (BC_Velocity - AC_Velocity));
        Target.UpdateAgentAttackVelocity(Target.AttackVelocity + (BC_AttackVelocity - AC_AttackVelocity));

        ResetProperties();
    }
    // Protected (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////