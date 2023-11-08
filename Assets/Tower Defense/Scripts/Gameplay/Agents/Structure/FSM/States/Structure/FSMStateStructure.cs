using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
using Core.Patterns;

public class FSMStateStructure : FiniteStateMachine
{
    // Protected (Variables) [START]
    protected Animator anim;
    protected Structure structure;
    protected StructureFsmAi structureFSMAi;
    // Protected (Variables) [END]

    public FSMStateStructure(Animator pAnim, Structure pStructure) : base()
    {
        anim = pAnim;
        structure = pStructure;
        structureFSMAi = structure.GetComponent<StructureFsmAi>();
    }

    // Protected (Methods) [START]
    protected bool DidStructureFoundEnemies()
    {
        return (
            structure.PriorityGoals.Count > 0
            || (structure.MainGoals.Count > 0 && structure.goal == AgentGoalEnum.CORESTRUCTURES)
        );
    }
    protected bool IsStructureDefeated() { return structure.ActualHealth <= 0f; }
    // Protected (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////