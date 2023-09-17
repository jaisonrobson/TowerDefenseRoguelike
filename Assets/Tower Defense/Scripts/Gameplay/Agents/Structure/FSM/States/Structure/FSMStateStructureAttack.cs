using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
using Core.Patterns;

public class FSMStateStructureAttack : FSMStateStructure
{
    public FSMStateStructureAttack(Animator pAnim, Structure pStructure) : base(pAnim, pStructure)
    {
        name = AgentStateEnum.ATTACK;
    }

    // Public (Methods) [START]
    public override void Enter()
    {
        anim.SetTrigger("isAttacking");

        base.Enter();
    }
    public override void Update()
    {
        if (IsStructureDefeated())
        {
            nextState = new FSMStateStructureDie(anim, structure);
            stage = FSMEventEnum.EXIT;

            return;
        }

        if (structureFSMAi.IsMakingAnyAttack())
            return;

        if (!DidStructureFoundEnemies() || !structureFSMAi.IsAggressive || !structureFSMAi.IsAnyViableAttackUnderEnemyRange())
        {
            nextState = new FSMStateStructureIdle(anim, structure);
            stage = FSMEventEnum.EXIT;
        }
    }
    public override void Exit()
    {
        anim.ResetTrigger("isAttacking");

        base.Exit();
    }
    // Public (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////