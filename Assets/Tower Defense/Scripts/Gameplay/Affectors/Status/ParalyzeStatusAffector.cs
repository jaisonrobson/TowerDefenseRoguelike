using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using Pathfinding;
using Core.Math;

/// <summary>
/// TAKES DAMAGE PER TURN, CANNOT MOVE, CANNOT ATTACK,
/// ITS POSITION GETS CRAZY FOR A FEW STEPS RANDOMLY (THEN RETURN TO THE INITIAL POSITION)
/// </summary>
[HideMonoScript]
public class ParalyzeStatusAffector : StatusAffector
{
    // Private (Variables) [START]
    private float timeUntilReposition = 0f;
    private float timeForPositionShuffling = 0f;
    // Private (Variables) [END]

    // Private (Properties) [START]
    private Vector3 BC_TargetAgentPosition { get; set; }
    // Private (Properties) [END]

    // (Unity) Methods [START]
    protected override void Update()
    {
        base.Update();

        HandleAgentPositionShuffling();
    }
    // (Unity) Methods [END]

    // Private (Methods) [START]
    private void ResetProperties()
    {
        BC_TargetAgentPosition = Vector3.zero;
        timeUntilReposition = 0f;
        timeForPositionShuffling = 0f;
    }
    private void HandleAgentPositionShuffling()
    {
        if (Target == null || Invoker == null)
            return;

        if (Time.time < timeForPositionShuffling)
        {
            if (Time.time > timeUntilReposition)
            {
                Vector3 randomNewPosition = RNG.Vector3(Target.transform.position, 0f, 0.10f);
                randomNewPosition.y = Target.transform.position.y;

                if (Target?.GetComponent<AIPath>() != null)
                {
                    Target.GetComponent<AIPath>().destination = randomNewPosition;
                    Target.GetComponent<AIPath>().Teleport(randomNewPosition);
                }

                timeUntilReposition = Time.time + 0.25f;
            }
        }
    }
    // Private (Methods) [END]

    // Protected (Methods) [START]
    protected override void ExecuteTurnActions()
    {
        base.ExecuteTurnActions();

        Target.OnReceiveDamageByStatus(Alignment, Damage, statusAffectorSO, Invoker);

        timeForPositionShuffling = Time.time + 1f;
    }
    protected override void InitializeStatusActions()
    {
        BC_TargetAgentPosition = Target.transform.position;

        Target.AddMovementPrevention();
        Target.AddAttackPrevention();
    }
    protected override void FinishStatusActions()
    {
        if (Target?.GetComponent<AIPath>() != null)
        {
            Target.GetComponent<AIPath>().destination = BC_TargetAgentPosition;
            Target.GetComponent<AIPath>().Teleport(BC_TargetAgentPosition);
        }

        Target.RemoveMovementPrevention();
        Target.RemoveAttackPrevention();

        ResetProperties();
    }
    // Protected (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////