using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
using Core.Patterns;

public class FSMStatePlayableStructureIdle : FSMStatePlayableStructure
{
    public FSMStatePlayableStructureIdle(Animator pAnim, PlayableStructure pPlayableStructure) : base(pAnim, pPlayableStructure)
    {
        name = AgentStateEnum.IDLE;
    }

    // Public (Methods) [START]
    public override void Enter()
    {
        if (anim != null && anim.isActiveAndEnabled && playableStructure.IsPlaced)
            anim.SetTrigger("isIdle");

        base.Enter();
    }
    public override void Update()
    {
        if (!playableStructure.IsPlaced)
        {
            stage = FSMEventEnum.ENTER;

            return;
        }

        if (IsStructureDefeated())
        {
            nextState = new FSMStatePlayableStructureDie(anim, playableStructure);
            stage = FSMEventEnum.EXIT;

            return;
        }

        if (DidStructureFoundEnemies() && playableStructureFSMAi.IsAggressive && playableStructureFSMAi.IsAnyViableAttackUnderEnemyRange())
        {
            if (playableStructureFSMAi.IsAllAttacksUnderCooldown)
                return;

            nextState = new FSMStatePlayableStructureAttack(anim, playableStructure);
            stage = FSMEventEnum.EXIT;
        }
    }
    public override void Exit()
    {
        if (anim != null && anim.isActiveAndEnabled && playableStructure.IsPlaced)
            anim.ResetTrigger("isIdle");

        base.Exit();
    }
    // Public (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////