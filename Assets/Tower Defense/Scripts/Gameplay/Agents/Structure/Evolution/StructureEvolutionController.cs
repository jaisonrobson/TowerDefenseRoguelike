using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Core.Patterns;
using System.Linq;

[RequireComponent(typeof(StructureEvolutionManager))]
[HideMonoScript]
public class StructureEvolutionController : Singleton<StructureEvolutionController>
{
    // (Unity) Methods [START]

    // (Unity) Methods [END]


    // Public (Methods) [START]
    public void EvolveStructure(AgentSO selectedEvolution = null)
    {
        StructureEvolutionManager sem = StructureEvolutionManager.instance;

        AgentSO newStructureAgentSO = selectedEvolution != null ? selectedEvolution : sem.AgentSO.evolutionTree.FirstOrDefault();

        if (newStructureAgentSO != null)
        {
            PlacementArea pa = sem.PlayableStructure.PlacementArea;

            sem.PlayableStructure.PoolAgent();

            PlayableStructure newStructure = Poolable.TryGetPoolable(newStructureAgentSO.prefab).GetComponent<PlayableStructure>();

            newStructure.transform.position = sem.Position;

            newStructure.GetComponent<Agent>().Alignment = MapManager.instance.map.playerAlignment.alignment;
            newStructure.GetComponent<PlayableStructure>().PlaceStructure(pa);
            newStructure.GetComponent<PlayableStructure>().InitializeGoalFlag(true);

            newStructure.SetFlagPosition(sem.FlagPosition);
            newStructure.ActualHealth = (sem.LifePercentage / 100) * newStructure.MaxHealth;

            newStructure.DoEvolutionFXs();
        }
    }
    // Public (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////