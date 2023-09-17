using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

/*
 * This script is related to the creatures which will be spawned by the structures, greater creatures, etc.
 * A creature (Invoked) is also an agent.
 * The structure (Invoker) or great creature, also is an agent.
 */

[ManageableData]
public class SubSpawnSO : BaseOptionDataSO
{
    [BoxGroup("base", ShowLabel = false)]
    [Required]
    public AgentSO creature;

    [BoxGroup("base")]
    [PropertySpace(5f, 0f)]
    [PropertyTooltip("The delay between each creature spawn in seconds.")]
    [PropertyRange(0, 60)]
    public int delay = 0;

    [BoxGroup("base")]
    [PropertySpace(5f, 0f)]
    [PropertyTooltip("The maximum amount of creatures alive per master (agent that controls these spawned units).")]
    [PropertyRange(1, 10)]
    public int maxAlive = 1;
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////