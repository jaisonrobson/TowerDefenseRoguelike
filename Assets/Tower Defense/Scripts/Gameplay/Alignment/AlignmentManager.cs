using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Core.Patterns;
using Sirenix.OdinInspector;

public class AlignmentManager : Singleton<AlignmentManager>
{
    // Private (Variables) [START]
    [ShowInInspector]
    [HideInEditorMode]
    [ReadOnly]
    private List<AlignmentSO> alignments;
    // Private (Variables) [END]

    // Public (Properties) [START]
    public List<AlignmentSO> Alignments { get { return alignments; } }
    public AlignmentSO PlayerAlignment { get { return MapManager.instance.map.playerAlignment; } }
    public List<AlignmentSO> PlayerOpponentsAlignments { get { return alignments.SkipWhile(al => al == PlayerAlignment).ToList(); } }
    // Public (Properties) [END]

    // (Unity) Methods [START]
    void Start()
    {
        InitializeVariables();
    }
    // (Unity) Methods [END]

    // Public (Methods) [START]
    public AlignmentSO GetAlignment(AlignmentEnum al) => Alignments.Where(ag => ag.alignment == al).First();
    public List<AlignmentSO> GetOpponents(AlignmentEnum pAlignment) { return MapManager.instanceExists && pAlignment != AlignmentEnum.GENERIC ? MapManager.instance.map.alignmentsOpponents.Where(ao => ao.alignment.alignment == pAlignment).First().opponents : null; }
    public bool IsAlignmentAnOpponent(AlignmentEnum pAlignmentOne, AlignmentEnum pAlignmentTwo) {
        List<AlignmentSO> opponents = GetOpponents(pAlignmentOne);

        return opponents != null ? opponents.Any(opponent => opponent.alignment == pAlignmentTwo) : false;
    }
    // Public (Methods) [END]

    // Private (Methods) [START]
    private void InitializeVariables()
    {
        InitializeAlignments();
    }
    private void InitializeAlignments()
    {
        alignments = MapManager.instance.map.alignmentsOpponents.Select(al => al.alignment).ToList();
    }
    // Private (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////