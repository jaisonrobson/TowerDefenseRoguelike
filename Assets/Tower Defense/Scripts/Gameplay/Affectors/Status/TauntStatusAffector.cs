using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

[HideMonoScript]
public class TauntStatusAffector : StatusAffector
{
    // (Unity) Methods [START]
    protected override void Update()
    {
        base.Update();

        HandleAffectedGoalChanging();
    }
    // (Unity) Methods [END]

    // Private (Methods) [START]
    private void HandleAffectedGoalChanging()
    {
        if (Target != null && Invoker != null)
        {
            Target.ActualGoal = Invoker;
        }
    }
    // Private (Methods) [END]

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