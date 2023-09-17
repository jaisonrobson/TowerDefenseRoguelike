using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
#endif
using System.Linq;

[ManageableData]
public class WaveSO : BaseOptionDataSO
{
    [BoxGroup("Box1", ShowLabel = false)]
    [HorizontalGroup("Box1/split", LabelWidth = 90)]

    [VerticalGroup("Box1/split/left")]
    [Required]
    [ListDrawerSettings(Expanded = true)]
    [PropertyTooltip("The agents to be spawned during the waves.")]
    [OnCollectionChanged(Before = "Before_CollectionChange_Agents")]
    [ValidateInput("Validate_MustHaveElements_Agents", "Agents must have at least one element.")]
    public AgentSO[] agents;

    [VerticalGroup("Box1/split/right")]
    [Required]
    [ListDrawerSettings(Expanded = true)]
    [PropertyTooltip("The time in seconds after the wave starts in which each agent needs to spawn.")]
    [OnCollectionChanged(Before = "Before_CollectionChange_Spawns")]
    [ValidateInput("Validate_MustHaveElements_Spawns", "Spawns must have at least one element.")]
    public float[] spawnTimes;


    // Helper Methods [START]
    #if UNITY_EDITOR
    private void Before_CollectionChange_Agents(CollectionChangeInfo info, object value)
    {
        switch (info.ChangeType)
        {
            case CollectionChangeType.Add:
                spawnTimes = spawnTimes.Append(0).ToArray();
                break;
            case CollectionChangeType.Insert:
                spawnTimes = spawnTimes.Append(0).ToArray();
                break;
            case CollectionChangeType.RemoveIndex:
                spawnTimes = spawnTimes.Where((value, index) => index != info.Index).ToArray();
                break;
        }
    }

    private void Before_CollectionChange_Spawns(CollectionChangeInfo info, object value)
    {
        switch (info.ChangeType)
        {
            case CollectionChangeType.Add:
                agents = agents.Append(null).ToArray();
                break;
            case CollectionChangeType.Insert:
                agents = agents.Append(null).ToArray();
                break;
            case CollectionChangeType.RemoveIndex:
                agents = agents.Where((value, index) => index != info.Index).ToArray();
                break;
        }
    }
    #endif
    // Helper Methods [END]


    // Validation Methods [START]
    private bool Validate_MustHaveElements_Agents() { return agents.Length > 0; }
    private bool Validate_MustHaveElements_Spawns() { return spawnTimes.Length > 0; }
    // Validation Methods [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////