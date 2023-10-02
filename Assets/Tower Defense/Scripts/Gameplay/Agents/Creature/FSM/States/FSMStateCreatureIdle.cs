using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
using Core.Patterns;
using Pathfinding;

public class FSMStateCreatureIdle : FiniteStateMachine
{
    // Private (Variables) [START]
    private Animator anim;
    private Creature creature;
    private AIPath pathfinding;
    private CreatureFsmAi creatureFSMAi;
    // Private (Variables) [END]

    public FSMStateCreatureIdle(Animator pAnim, Creature pCreature, AIPath pPathfinding) : base()
    {
        anim = pAnim;
        creature = pCreature;
        pathfinding = pPathfinding;
        name = AgentStateEnum.IDLE;
        creatureFSMAi = creature.GetComponent<CreatureFsmAi>();
    }

    // Public (Methods) [START]
    public override void Enter()
    {
        base.Enter();
    }
    /// <summary>
    /// Things that the creature can do after Idle in hierarchical call priority:
    /// * Die (Check if health is zero and then just die)
    /// * Walk (If agent has goals/enemies and did no reached distance)
    /// * Attack (For units that reached the enemy/goal)
    /// </summary>
    public override void Update()
    {
        if (IsCreatureDead())
        {
            nextState = new FSMStateCreatureDie(anim, creature, pathfinding);
            stage = FSMEventEnum.EXIT;

            return;
        }

        if (DidCreatureFoundEnemies() || creatureFSMAi.IsCreatureConfused)
        {
            if (creatureFSMAi.IsAggressive && creatureFSMAi.IsAnyViableAttackUnderEnemyRange() && !creature.IsAttackPrevented)
            {
                if (creatureFSMAi.IsAllAttacksUnderCooldown)
                    return;

                nextState = new FSMStateCreatureAttack(anim, creature, pathfinding);
                stage = FSMEventEnum.EXIT;
            }
            else
            {
                if (creatureFSMAi.IsMovable && !pathfinding.reachedDestination && !creature.IsMovementPrevented)
                {
                    nextState = new FSMStateCreatureWalk(anim, creature, pathfinding);
                    stage = FSMEventEnum.EXIT;
                }
            }
        }        
    }
    public override void Exit()
    {
        base.Exit();
    }
    // Public (Methods) [END]

    // Private (Methods) [START]
    private bool DidCreatureFoundEnemies() {
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