using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

/*
 * This script is related to the agents in which the player can buy/place.
 */

[ManageableData]
public class PlayableAgentSO : BaseOptionDataSO
{
    [BoxGroup("base", ShowLabel = false)]
    [PropertyTooltip("The agent that will be invoked.")]
    [Required]
    public AgentSO agent;

    [BoxGroup("base")]
    [PropertySpace(5f, 0f)]
    [PropertyRange(1, 1000)]
    [PropertyTooltip("The cost to invoke this agent.")]
    public int cost = 1;
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////