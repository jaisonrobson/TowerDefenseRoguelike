using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;

[ManageableData]
public class MapSO : BaseOptionDataSO
{
    [BoxGroup("Base", ShowLabel = false)]
    public new string name;

    [BoxGroup("Base")]
    [PropertySpace(5f, 0f)]
    [Required]
    [Tooltip("This represents the scene name that this level will occur.\n\nThis information is used inside the gameplay scenes to link this asset with the game.")]
    public string sceneName;

    [BoxGroup("Base")]
    [PropertySpace(5f, 0f)]
    [MultiLineProperty(3)]
    public string description;

    [BoxGroup("Player", LabelText = "Player Identity")]
    [PropertySpace(5f, 5f)]
    [GUIColor(0.9f, 0.9f, 0.9f)]
    [Tooltip("The team of the player on this level.")]
    [Required]
    public AlignmentSO playerAlignment;

    [BoxGroup("Player")]
    [PropertySpace(5f, 5f)]
    [GUIColor(0.9f, 0.9f, 0.9f)]
    [Tooltip("The initial quantity of points the player will have on this map.")]
    [Min(0)]
    public int playerInitialPoints = 0;

    [BoxGroup("Player")]
    [PropertySpace(5f, 5f)]
    [GUIColor(0.9f, 0.9f, 0.9f)]
    [ListDrawerSettings(Expanded = true)]
    [Tooltip("The agents in which the player can build/invoke during the gameplay.")]
    [Required]
    [ValidateInput("Validate_MustHaveElements_PlayableAgents", "Playable Agents must have at least one element.")]
    public PlayableAgentSO[] playableAgents;

    [BoxGroup("Player")]
    [PropertySpace(5f, 5f)]
    [GUIColor(0.9f, 0.9f, 0.9f)]
    [ListDrawerSettings(Expanded = true)]
    [Tooltip("The main structure agents that represents the player, if they are destroyed, the player lose.")]
    [Required]
    [ValidateInput("Validate_MustHaveElements_PlayerEntities", "Player Entities must have at least one element.")]
    [ValidateInput("Validate_MustBeStructure_PlayerEntities", "All player entities must be of the structure type.")]
    public AgentSO[] playerEntities;

    [BoxGroup("Miscelanous", true)]
    [PropertySpace(5f, 0f)]
    [GUIColor(0.9f, 0.9f, 0.9f)]
    [ListDrawerSettings(Expanded = true)]
    [Tooltip("each list represents a waves sequence list of the first fog.")]
    [Required]
    [ValidateInput("Validate_MustHaveElements_FogOneWaves", "[Fog One Waves] must have at least one sequence waves element.")]
    [ValidateInput("Validate_NotNull_FogOneWaves", "[Fog One Waves] All elements of the waves sequence must be valid.")]
    [ValidateInput("Validate_MustHaveEqualAlignments_Sequence_FogOneWaves", "[Fog One Waves] All elements of the same sequence must be of the same alignment.")]
    public AlignedWaveSO[] fogOneWaves;

    [BoxGroup("Miscelanous", true)]
    [PropertySpace(5f, 0f)]
    [GUIColor(0.9f, 0.9f, 0.9f)]
    [ListDrawerSettings(Expanded = true)]
    [Tooltip("each list represents a waves sequence list of the second fog.")]
    [Required]
    [ValidateInput("Validate_MustHaveElements_FogSecondWaves", "[Fog Second Waves] must have at least one sequence waves element.")]
    [ValidateInput("Validate_NotNull_FogSecondWaves", "[Fog Second Waves] All elements of the waves sequence must be valid.")]
    [ValidateInput("Validate_MustHaveEqualAlignments_Sequence_FogSecondWaves", "[Fog Second Waves] All elements of the same sequence must be of the same alignment.")]
    public AlignedWaveSO[] fogTwoWaves;

    [BoxGroup("Miscelanous", true)]
    [PropertySpace(5f, 0f)]
    [GUIColor(0.9f, 0.9f, 0.9f)]
    [ListDrawerSettings(Expanded = true)]
    [Tooltip("each list represents a waves sequence list of the third fog.")]
    [Required]
    [ValidateInput("Validate_MustHaveElements_FogThirdWaves", "[Fog Third Waves] must have at least one sequence waves element.")]
    [ValidateInput("Validate_NotNull_FogThirdWaves", "[Fog Third Waves] All elements of the waves sequence must be valid.")]
    [ValidateInput("Validate_MustHaveEqualAlignments_Sequence_FogThirdWaves", "[Fog Third Waves] All elements of the same sequence must be of the same alignment.")]
    public AlignedWaveSO[] fogThreeWaves;


    [BoxGroup("Miscelanous", true)]
    [PropertySpace(5f, 5f)]
    [GUIColor(0.9f, 0.9f, 0.9f)]
    [ListDrawerSettings(Expanded = true)]
    [Tooltip("Every team and its enemies on this level.")]
    [Required]
    [ValidateInput("Validate_MustHaveElements_AlignmentsOpponents", "[Alignments Opponents] must have at least one element.")]
    [ValidateInput("Validate_NotEqualElements_AlignmentsOpponents", "[Alignments Opponents] must not have equal elements.")]
    [ValidateInput("Validate_MustHaveElement_PlayerAlignmentOpponents", "[Alignments Opponents] must have the opponents for the player alignment.")]
    [ValidateInput("Validate_MustHaveElement_WavesAlignmentOpponents", "[Alignments Opponents] must have the opponents for all waves participant alignments.")]
    public AlignmentOpponentsSO[] alignmentsOpponents;

    [BoxGroup("Miscelanous", true)]
    [PropertySpace(5f, 5f)]
    [GUIColor(0.9f, 0.9f, 0.9f)]
    [Tooltip("The weaknesses table to be used on this level.")]
    [Required]
    public WeaknessesSO weaknesses;

    // Validation Methods [START]
    private bool Validate_MustHaveElements_PlayableAgents() { return playableAgents.Length > 0; }
    private bool Validate_MustHaveElements_PlayerEntities() { return playerEntities.Length > 0; }
    private bool Validate_MustBeStructure_PlayerEntities() { return playerEntities != null && playerEntities.All(pe => pe != null && pe.type == AgentTypeEnum.STRUCTURE); }
    private bool Validate_MustHaveElements_FogOneWaves() { return fogOneWaves != null && fogOneWaves.Length > 0; }
    private bool Validate_NotNull_FogOneWaves()
    {
        if (fogOneWaves == null || fogOneWaves.Length <= 0) return true;

        return fogOneWaves.Any(element => element != null);
    }
    private bool Validate_MustHaveEqualAlignments_Sequence_FogOneWaves()
    {
        if (fogOneWaves == null || fogOneWaves.Length <= 0) return true;

        AlignmentSO lastAlignmentFound = fogOneWaves.First().alignment;

        return fogOneWaves.All(el => el.alignment == lastAlignmentFound);
    }

    private bool Validate_MustHaveElements_FogSecondWaves() { return fogTwoWaves != null && fogTwoWaves.Length > 0; }
    private bool Validate_NotNull_FogSecondWaves()
    {
        if (fogTwoWaves == null || fogTwoWaves.Length <= 0) return true;

        return fogTwoWaves.Any(element => element != null);
    }
    private bool Validate_MustHaveEqualAlignments_Sequence_FogSecondWaves()
    {
        if (fogTwoWaves == null || fogTwoWaves.Length <= 0) return true;

        AlignmentSO lastAlignmentFound = fogTwoWaves.First().alignment;

        return fogTwoWaves.All(el => el.alignment == lastAlignmentFound);
    }
    private bool Validate_MustHaveElements_FogThirdWaves() { return fogThreeWaves != null && fogThreeWaves.Length > 0; }
    private bool Validate_NotNull_FogThirdWaves()
    {
        if (fogThreeWaves == null || fogThreeWaves.Length <= 0) return true;

        return fogThreeWaves.Any(element => element != null);
    }
    private bool Validate_MustHaveEqualAlignments_Sequence_FogThirdWaves()
    {
        if (fogThreeWaves == null || fogThreeWaves.Length <= 0) return true;

        AlignmentSO lastAlignmentFound = fogThreeWaves.First().alignment;

        return fogThreeWaves.All(el => el.alignment == lastAlignmentFound);
    }
    private bool Validate_MustHaveElements_AlignmentsOpponents() { return alignmentsOpponents.Length > 0; }
    private bool Validate_NotEqualElements_AlignmentsOpponents()
    {
        if (alignmentsOpponents == null) return true;

        bool result = true;

        alignmentsOpponents.ToList().ForEach(
            ao => {
                if (!result) return;

                if (alignmentsOpponents.ToList().Where(x => x.alignment == ao.alignment).Count() > 1)
                    result = false;
            }
        );

        return result;
    }

    private bool Validate_MustHaveElement_PlayerAlignmentOpponents()
    {
        if (playerAlignment == null) return false;
        if (alignmentsOpponents == null) return false;

        return alignmentsOpponents.ToList().Where(ao => ao.alignment == playerAlignment).Count() > 0;
    }
    private bool Validate_MustHaveElement_WavesAlignmentOpponents()
    {
        if (playerAlignment == null) return false;
        if (alignmentsOpponents == null) return false;

        List<AlignmentEnum> alignedWavesAligments = new List<AlignmentEnum>();

        fogOneWaves.ToList().ForEach(el => {
            if (alignedWavesAligments.All(al => el.alignment.alignment != al))
                alignedWavesAligments.Add(el.alignment.alignment);
        });

        fogTwoWaves.ToList().ForEach(el => {
            if (alignedWavesAligments.All(al => el.alignment.alignment != al))
                alignedWavesAligments.Add(el.alignment.alignment);
        });

        fogThreeWaves.ToList().ForEach(el => {
            if (alignedWavesAligments.All(al => el.alignment.alignment != al))
                alignedWavesAligments.Add(el.alignment.alignment);
        });            

        bool result = true;

        foreach (AlignmentEnum al in alignedWavesAligments)
            if (!alignmentsOpponents.Any(ao => ao.alignment.alignment == al))
                result = false;

        return result;
    }
    // Validation Methods [END]

    // In Game Methods [START]
    public string GetName()
    {
        return this.name;
    }

    public AlignmentOpponentsSO GetAlignmentOpponents(AlignmentSO alignment)
    {
        return alignmentsOpponents.FirstOrDefault(ao => ao.alignment == alignment);
    }
    // In Game Methods [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////