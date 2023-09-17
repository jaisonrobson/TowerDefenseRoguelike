using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
using Core.Patterns;

public class FSMStateStructureDie : FSMStateStructure
{
    public FSMStateStructureDie(Animator pAnim, Structure pStructure) : base(pAnim, pStructure)
    {
        name = AgentStateEnum.DIE;
    }

    // Public (Methods) [START]
    public override void Enter()
    {
        structure.mainCollider.enabled = false;

        structureFSMAi.StartDying();

        base.Enter();
    }
    public override void Update() { }
    public override void Exit() { base.Exit(); }
    // Public (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////