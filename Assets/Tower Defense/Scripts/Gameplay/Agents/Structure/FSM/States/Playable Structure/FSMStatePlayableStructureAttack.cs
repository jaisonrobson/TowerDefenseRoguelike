using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
using Core.Patterns;

public class FSMStatePlayableStructureAttack : FSMStatePlayableStructure
{
    public FSMStatePlayableStructureAttack(Animator pAnim, PlayableStructure pPlayableStructure) : base(pAnim, pPlayableStructure)
    {
        name = AgentStateEnum.ATTACK;
    }

    // Public (Methods) [START]
    public override void Enter()
    {
        if (anim != null && anim.isActiveAndEnabled && playableStructure.IsPlaced)
            anim.SetTrigger("isAttacking");

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

        if (playableStructureFSMAi.IsMakingAnyAttack())
            return;

        if (!DidStructureFoundEnemies() || !playableStructureFSMAi.IsAggressive || !playableStructureFSMAi.IsAnyViableAttackUnderEnemyRange())
        {
            nextState = new FSMStatePlayableStructureIdle(anim, playableStructure);
            stage = FSMEventEnum.EXIT;
        }
    }
    public override void Exit()
    {
        if (anim != null && anim.isActiveAndEnabled && playableStructure.IsPlaced)
            anim.ResetTrigger("isAttacking");

        base.Exit();
    }
    // Public (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////