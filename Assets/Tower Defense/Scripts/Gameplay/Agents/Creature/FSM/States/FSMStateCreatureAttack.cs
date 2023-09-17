using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
using Core.Patterns;
using Core.Math;
using Pathfinding;

public class FSMStateCreatureAttack : FiniteStateMachine
{
    // Private (Variables) [START]
    private Animator anim;
    private Creature creature;
    private AIPath pathfinding;
    private CreatureFsmAi creatureFsmAi;
    // Private (Variables) [END]

    public FSMStateCreatureAttack(Animator pAnim, Creature pCreature, AIPath pPathfinding) : base()
    {
        anim = pAnim;
        creature = pCreature;
        pathfinding = pPathfinding;
        creatureFsmAi = creature.GetComponent<CreatureFsmAi>();
        name = AgentStateEnum.ATTACK;
    }

    // Public (Methods) [START]
    public override void Enter()
    {
        anim.SetTrigger("isAttacking");

        base.Enter();
    }
    /// <summary>
    /// Things that the creature can do after Attack in hierarchical call priority:
    /// * Die
    /// * Walk
    /// * Idle
    /// </summary>
    public override void Update()
    {
        if (IsCreatureDead())
        {
            nextState = new FSMStateCreatureDie(anim, creature, pathfinding);
            stage = FSMEventEnum.EXIT;

            return;
        }

        LookAtGoal();

        if (creature.IsAttackPrevented || !creatureFsmAi.IsAggressive)
        {
            nextState = new FSMStateCreatureIdle(anim, creature, pathfinding);
            stage = FSMEventEnum.EXIT;

            return;
        }

        if (DidCreatureFoundEnemies() || creatureFsmAi.IsCreatureConfused)
        {
            if (!creatureFsmAi.IsAnyViableAttackUnderEnemyRange())
            {
                if (creatureFsmAi.IsMovable && !pathfinding.reachedDestination && !creature.IsMovementPrevented)
                {
                    nextState = new FSMStateCreatureWalk(anim, creature, pathfinding);
                    stage = FSMEventEnum.EXIT;
                }
                else if (!creatureFsmAi.IsMakingAnyAttack())
                {
                    nextState = new FSMStateCreatureIdle(anim, creature, pathfinding);
                    stage = FSMEventEnum.EXIT;
                }
            }
        }
        else
        {
            nextState = new FSMStateCreatureIdle(anim, creature, pathfinding);
            stage = FSMEventEnum.EXIT;
        }
    }
    public override void Exit()
    {
        anim.ResetTrigger("isAttacking");

        base.Exit();
    }
    // Public (Methods) [END]

    // Private (Methods) [START]
    private void LookAtGoal()
    {
        if (creature.ActualGoal == null)
            return;

        creature.transform.LookAt(new Vector3(creature.ActualGoal.transform.position.x, creature.transform.position.y, creature.ActualGoal.transform.position.z));
    }
    private bool DidCreatureFoundEnemies()
    {
        return (
            creature.PriorityGoals.Count > 0
            && creature.PriorityGoals.Any(pg => pg.ignoreBattle == false)
            || (creature.MainGoals.Count > 0 && creature.goal == AgentGoalEnum.CORESTRUCTURES)
        );
    }
    private bool IsCreatureDead() { return creature.ActualHealth <= 0f; }
    // Private (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////