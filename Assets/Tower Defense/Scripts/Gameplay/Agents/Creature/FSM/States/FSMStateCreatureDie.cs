using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Core.Patterns;
using Pathfinding;

public class FSMStateCreatureDie : FiniteStateMachine
{
    // Private (Variables) [START]
    private Animator anim;
    private Creature creature;
    private AIPath pathfinding;
    private CreatureFsmAi creatureFsmAi;
    // Private (Variables) [END]

    public FSMStateCreatureDie(Animator pAnim, Creature pCreature, AIPath pPathfinding) : base()
    {
        anim = pAnim;
        creature = pCreature;
        pathfinding = pPathfinding;
        name = AgentStateEnum.DIE;
        creatureFsmAi = creature.GetComponent<CreatureFsmAi>();
    }

    // Public (Methods) [START]
    public override void Enter()
    {
        pathfinding.canMove = false;
        pathfinding.enabled = false;

        creature.mainCollider.enabled = false;

        creatureFsmAi.StartDying();

        base.Enter();
    }
    public override void Update() {}
    public override void Exit() { base.Exit(); }
    // Public (Methods) [END]

    // Private (Methods) [START]
    // Private (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////