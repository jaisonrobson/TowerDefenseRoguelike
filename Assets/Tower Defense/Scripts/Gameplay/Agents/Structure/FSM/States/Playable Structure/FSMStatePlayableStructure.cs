using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
using Core.Patterns;

public class FSMStatePlayableStructure : FiniteStateMachine
{
    // Protected (Variables) [START]
    protected Animator anim;
    protected PlayableStructure playableStructure;
    protected PlayableStructureFsmAi playableStructureFSMAi;
    // Protected (Variables) [END]

    public FSMStatePlayableStructure(Animator pAnim, PlayableStructure pPlayableStructure) : base()
    {
        anim = pAnim;
        playableStructure = pPlayableStructure;
        playableStructureFSMAi = playableStructure.GetComponent<PlayableStructureFsmAi>();
    }

    // Protected (Methods) [START]
    protected bool DidStructureFoundEnemies()
    {
        return (
            playableStructure.PriorityGoals.Count > 0
            && playableStructure.PriorityGoals.Any(pg => pg.ignoreBattle == false)
            || (playableStructure.MainGoals.Count > 0 && playableStructure.goal == AgentGoalEnum.CORESTRUCTURES)
        );
    }
    protected bool IsStructureDefeated() { return playableStructure.ActualHealth <= 0f; }
    // Protected (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////