using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
using Core.Patterns;

public class FSMStateStructureIdle : FSMStateStructure
{
    public FSMStateStructureIdle(Animator pAnim, Structure pStructure) : base(pAnim, pStructure)
    {
        name = AgentStateEnum.IDLE;
    }

    // Public (Methods) [START]
    public override void Enter()
    {
        anim.SetTrigger("isIdle");

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

        if (DidStructureFoundEnemies() && structureFSMAi.IsAggressive && structureFSMAi.IsAnyViableAttackUnderEnemyRange())
        {
            if (structureFSMAi.IsAllAttacksUnderCooldown)
                return;

            nextState = new FSMStateStructureAttack(anim, structure);
            stage = FSMEventEnum.EXIT;
        }
    }
    public override void Exit()
    {
        anim.ResetTrigger("isIdle");

        base.Exit();
    }
    // Public (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////