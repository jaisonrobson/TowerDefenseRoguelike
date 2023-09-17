using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using Pathfinding;
using Core.Math;

/// <summary>
/// ATTACK ITSELF AND MOVES RANDOMLY FOR THE DURATION OF THE CONFIGURATION
/// </summary>
[HideMonoScript]
public class ConfusionStatusAffector : StatusAffector
{
    // (Unity) Methods [START]
    protected override void Update()
    {
        base.Update();

        HandleDestinationShuffling();
    }
    // (Unity) Methods [END]

    // Private (Methods) [START]
    private void HandleDestinationShuffling()
    {
        if (Target != null && Target.GetComponent<AIPath>() != null)
        {
            Target.ActualGoal = Target;

            if (RNG.Int(0, 100) > 98)
            {
                Vector3 randomNewPosition = RNG.Vector3(Target.transform.position, -3f, 3f);

                randomNewPosition.y = Target.transform.position.y;

                Target.GetComponent<AIPath>().destination = randomNewPosition;
            }
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
        
    }
    protected override void FinishStatusActions()
    {
        
    }
    // Protected (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////