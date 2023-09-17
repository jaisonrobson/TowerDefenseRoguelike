using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
using Core.Patterns;

public class FSMStatePlayableStructureDie : FSMStatePlayableStructure
{
    public FSMStatePlayableStructureDie(Animator pAnim, PlayableStructure pPlayableStructure) : base(pAnim, pPlayableStructure)
    {
        name = AgentStateEnum.DIE;
    }

    // Public (Methods) [START]
    public override void Enter()
    {
        if (playableStructure.IsPlaced) {
            if (anim != null && anim.isActiveAndEnabled)
                anim.SetTrigger("isDying");

            playableStructure.mainCollider.enabled = false;

            playableStructureFSMAi.StartDying();
        }

        base.Enter();
    }
    public override void Update() { }
    public override void Exit() { base.Exit(); }
    // Public (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////