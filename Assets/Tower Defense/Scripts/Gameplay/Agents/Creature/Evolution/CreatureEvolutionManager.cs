using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using Core.Patterns;
using Core.Math;

[HideMonoScript]
public class CreatureEvolutionManager : Singleton<CreatureEvolutionManager>
{
    // Public (Methods) [START]
    public void TryToEvolveCreature(Creature pCreature)
    {
        if (!pCreature.CanEvolve)
            return;

        Vector3 position = pCreature.transform.position;
        float lifePercentage = pCreature.ActualHealth / pCreature.MaxHealth;
        AgentSO agentBeforeEvolution = pCreature.GetAgent();
        AlignmentEnum alignment = pCreature.Alignment;

        if (agentBeforeEvolution.evolutionTree.Count > 0)
        {
            Poolable.TryPool(pCreature.gameObject);

            AgentSO evolution = agentBeforeEvolution.evolutionTree.ElementAt(RNG.Int(0, agentBeforeEvolution.evolutionTree.Count-1));

            GameObject newCreature = Poolable.TryGetPoolable(
                evolution.prefab,
                (newPooled) => {
                    newPooled.transform.position = position;

                    Agent agt = newPooled.GetComponent<Agent>();

                    agt.Alignment = alignment;
                    agt.ActualHealth = agt.MaxHealth * lifePercentage;
                }
            );

            newCreature?.GetComponent<Agent>().DoEvolutionFXs();
        }
    }
    // Public (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////