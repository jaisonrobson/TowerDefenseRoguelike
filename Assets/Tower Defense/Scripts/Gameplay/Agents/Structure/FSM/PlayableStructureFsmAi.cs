using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableStructureFsmAi : StructureFsmAi
{
    // Private (Variables) [START]
    private PlayableStructure playableStructure;
    // Private (Variables) [END]

    // (Unity) Methods [START]
    // Start is called before the first frame update
    protected override void OnEnable()
    {
        base.OnEnable();

        playableStructure = AgentGOBJ.GetComponent<PlayableStructure>();

        currentState = new FSMStatePlayableStructureIdle(Anim, playableStructure);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        UpdateAIGoal();
    }
    // (Unity) Methods [END]

    // Private (Methods) [START]
    private void UpdateAIGoal()
    {
        if (IsAgentDead)
            return;

        List<PriorityGoal> creaturePriorityEnemies = agent.PriorityGoals;

        if (creaturePriorityEnemies.Count > 0 && IsAggressive)
        {
            PriorityGoal nearestPriorityEnemy = agent.GetAgentNearestViablePriorityEnemy();

            agent.ActualGoal = nearestPriorityEnemy.goal;
        }
        else
        {
            agent.ActualGoal = null;
        }
    }
    // Private (Methods) [END]

    // Protected (Methods) [START]
    protected override void GoToIdleState()
    {
        currentState = new FSMStatePlayableStructureIdle(Anim, playableStructure);
    }
    // Protected (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////