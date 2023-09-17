using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Core.Patterns;
using Sirenix.OdinInspector;

[System.Serializable]
public struct SubSpawnPositioning
{
    public AgentSO subSpawn;
    public List<Vector3> agentsPositions;

    public SubSpawnPositioning(AgentSO pSubSpawn)
    {
        agentsPositions = new List<Vector3>();
        subSpawn = pSubSpawn;
    }
}

[RequireComponent(typeof(StructureEvolutionController))]
[HideMonoScript]
public class StructureEvolutionManager : Singleton<StructureEvolutionManager>
{
    // Private (Variables) [START]
    [BoxGroup("Last Structure Informations")]
    [PropertyOrder(-1)]
    [ShowInInspector]
    [HideInEditorMode]
    [ReadOnly]
    private PlayableStructure playableStructure;
    [BoxGroup("Last Structure Informations")]
    [ShowInInspector]
    [HideInEditorMode]
    [ReadOnly]
    private AgentSO agentSO;
    [BoxGroup("Last Structure Informations")]
    [ShowInInspector]
    [HideInEditorMode]
    [ReadOnly]
    private Vector3 position = new Vector3(0, 0, 0);
    [BoxGroup("Last Structure Informations")]
    [ShowInInspector]
    [HideInEditorMode]
    [ReadOnly]
    private List<SubSpawnPositioning> subspawnsPositions = new List<SubSpawnPositioning>();
    [BoxGroup("Last Structure Informations")]
    [ShowInInspector]
    [HideInEditorMode]
    [ReadOnly]
    private Vector3 flagPosition = new Vector3(0, 0, 0);
    [BoxGroup("Last Structure Informations")]
    [ShowInInspector]
    [HideInEditorMode]
    [ReadOnly]
    private float lifePercentage = 100f;
    [BoxGroup("Last Structure Informations")]
    [ShowInInspector]
    [HideInEditorMode]
    [ReadOnly]
    private AgentSO evolutionSelected;
    // Private (Variables) [END]

    // Public (Properties) [START]
    public PlayableStructure PlayableStructure { get { return playableStructure; } }
    public AgentSO AgentSO { get { return agentSO; } }
    public Vector3 Position { get { return position; } }
    public List<SubSpawnPositioning> SubspawnsPositions { get { return subspawnsPositions; } }
    public Vector3 FlagPosition { get { return flagPosition; } }
    public float LifePercentage { get { return lifePercentage; } }
    // Public (Properties) [END]

    // Private (Methods) [START]
    private bool CanEvolveStructure(PlayableStructure pPS) => pPS.CanEvolve && pPS.Alignment == AlignmentManager.instance.PlayerAlignment.alignment && PlayerManager.instance.CanDecreasePoints(pPS.ExperienceToEvolve);
    private void EvolveStructure(PlayableStructure pPlayableStructure, bool useSelectedEvolution = false)
    {
        if (!CanEvolveStructure(pPlayableStructure))
            return;

        PlayerManager.instance.DecreasePoints(pPlayableStructure.ExperienceToEvolve);

        SubspawnsPositions.Clear();

        playableStructure = pPlayableStructure;
        agentSO = pPlayableStructure.agent;
        position = pPlayableStructure.transform.position;

        pPlayableStructure.SubSpawns.ForEach(
            ss => {
                SubSpawnPositioning sp = new SubSpawnPositioning(ss.subSpawn.creature);

                ss.spawnedAgents.ForEach(
                    sa => sp.agentsPositions.Add(sa.transform.position)
                );

                subspawnsPositions.Add(sp);
            }
        );

        flagPosition = pPlayableStructure.GoalFlag.position;
        lifePercentage = ((pPlayableStructure.ActualHealth * 100) / pPlayableStructure.MaxHealth);

        if (useSelectedEvolution)
            StructureEvolutionController.instance.EvolveStructure(evolutionSelected);
        else
            StructureEvolutionController.instance.EvolveStructure();
    }
    // Private (Methods) [END]


    // Public (Methods) [START]
    public void EvolveSelectedStructure() {
        PlayableStructure ps = SelectionManager.instance.SelectedAgents.First()?.GetComponent<PlayableStructure>();

        EvolveSpecificStructure(ps);
    }
    public void EvolveSpecificStructure(PlayableStructure ps)
    {
        if (ps != null)
        {
            if (ps.HasEvolution) {
                if (ps.IsMultipleEvolution)
                {
                    OverlayInterfaceManager.instance.OpenStructureEvolutionPanel();
                }
                else
                {
                    EvolveStructure(ps);

                    SelectionManager.instance.RemoveSelectable(ps.GetComponent<Selectable>());
                }
            }
        }
    }
    public void EvolveSelectedEvolution(AgentSO pSelectedEvolution)
    {
        PlayableStructure ps = SelectionManager.instance.SelectedAgents.First()?.GetComponent<PlayableStructure>();

        if (ps != null)
        {
            evolutionSelected = pSelectedEvolution;

            EvolveStructure(ps, true);

            SelectionManager.instance.RemoveSelectable(ps.GetComponent<Selectable>());
        }
    }
    // Public (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////