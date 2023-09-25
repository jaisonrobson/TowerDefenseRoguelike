using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

[ManageableData]
public class WaveSpawnSO : BaseOptionDataSO
{
    [Required]
    [ListDrawerSettings(Expanded = true)]
    [PropertyTooltip("The group of agents to be spawned during the waves.")]
    public AgentSO[] agents;
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////