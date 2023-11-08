using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
using Core.Patterns;
using Pathfinding;

public class FSMStateCreatureWalk : FiniteStateMachine
{
    // Private (Variables) [START]
    private Animator anim;
    private Creature creature;
    private AIPath pathfinding;
    private CreatureFsmAi creatureFSMAi;
    // Private (Variables) [END]

    public FSMStateCreatureWalk(Animator pAnim, Creature pCreature, AIPath pPathfinding) : base()
    {
        anim = pAnim;
        creature = pCreature;
        pathfinding = pPathfinding;
        name = AgentStateEnum.WALK;
        creatureFSMAi = creature.GetComponent<CreatureFsmAi>();
    }

    // Public (Methods) [START]
    public override void Enter()
    {
        anim.SetTrigger("isWalking");

        base.Enter();
    }
    /// <summary>
    /// Things that the creature can do after Walk in hierarchical call priority:
    /// * Die
    /// * Attack
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

        if (!creatureFSMAi.IsMovable || creature.IsMovementPrevented)
        {
            nextState = new FSMStateCreatureIdle(anim, creature, pathfinding);
            stage = FSMEventEnum.EXIT;

            return;
        }

        if (creatureFSMAi.IsAggressive)
        {
            if (DidCreatureFoundEnemies() || creatureFSMAi.IsCreatureConfused)
            {
                if (creatureFSMAi.IsAnyViableAttackUnderEnemyRange())
                {
                    nextState = new FSMStateCreatureAttack(anim, creature, pathfinding);
                    stage = FSMEventEnum.EXIT;

                    return;
                }
            }
        }

        if (pathfinding.reachedDestination)
        {
            nextState = new FSMStateCreatureIdle(anim, creature, pathfinding);
            stage = FSMEventEnum.EXIT;
        }
    }
    public override void Exit()
    {
        anim.ResetTrigger("isWalking");

        base.Exit();
    }
    // Public (Methods) [END]

    // Private (Methods) [START]
    private bool DidCreatureFoundEnemies()
    {
        return (
            creature.PriorityGoals.Count > 0
            || (creature.MainGoals.Count > 0 && creature.goal == AgentGoalEnum.CORESTRUCTURES)
        );
    }
    private bool IsCreatureDead() { return creature.ActualHealth <= 0f; }
    // Private (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////