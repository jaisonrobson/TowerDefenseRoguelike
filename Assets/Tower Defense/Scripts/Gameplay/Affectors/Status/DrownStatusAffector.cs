using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using Pathfinding;

/// <summary>
/// TAKES DAMAGE PER TURN, CANNOT MOVE, CANNOT ATTACK,
/// THE AFFECTED FLOATS ITS POSITION A FEW STEPS FROM THE GROUND
/// (THEN RETURN TO THE INITIAL POSITION)
/// </summary>
[HideMonoScript]
public class DrownStatusAffector : StatusAffector
{
    // Private (Variables) [START]
    private float minimumYPosition = 0f;
    private float maximumYPosition = 0f;
    private float startAnimationTime = 0f;
    // Private (Variables) [END]

    // Private (Methods) [START]
    private void ResetProperties()
    {
        minimumYPosition = 0f;
        maximumYPosition = 0f;
        startAnimationTime = 0f;
    }
    // Private (Methods) [END]

    // (Unity) Methods [START]
    protected override void Update()
    {
        base.Update();

        if (Target != null && Target.GetComponent<AIPath>() != null && !Target.IsDead)
        {
            Vector3 newPos = Target.transform.position;

            float t = (Time.time - startAnimationTime) / 2f;

            if (t > 1f)
            {
                float localMin = minimumYPosition;
                minimumYPosition = maximumYPosition;
                maximumYPosition = localMin;
                startAnimationTime = Time.time;
                t = 0f;
            }

            newPos.y = Mathf.SmoothStep(minimumYPosition, maximumYPosition, t);

            Target.GetComponent<AIPath>().Teleport(newPos);
            Target.GetComponent<AIPath>().destination = Target.transform.position;
        }
    }
    // (Unity) Methods [END]

    // Protected (Methods) [START]
    protected override void ExecuteTurnActions()
    {
        base.ExecuteTurnActions();

        Target.OnReceiveDamageByStatus(Alignment, Damage, statusAffectorSO, Invoker);
    }
    protected override void InitializeStatusActions()
    {
        minimumYPosition = Target.transform.position.y;
        maximumYPosition = Target.transform.position.y + 3f;
        startAnimationTime = Time.time;

        Target.AddMovementPrevention();
        Target.AddAttackPrevention();
    }
    protected override void FinishStatusActions()
    {
        Target.RemoveMovementPrevention();
        Target.RemoveAttackPrevention();

        ResetProperties();
    }
    // Protected (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////