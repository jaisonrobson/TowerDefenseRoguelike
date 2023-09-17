using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Core.Patterns;
using Core.Physics;
using Pathfinding;

[HideMonoScript]
public class Structure : Agent
{
    // Public (Variables) [START]
    [TitleGroup("Agent Identity/Main Information")]
    [PropertyOrder(1)]
    [Required]
    [ValidateInput("Validate_AgentType_IsStructure", "Agent is not a structure.")]
    public AgentSO agent;
    

    // (Unity) Methods [START]
    protected override void Start()
    {
        base.Start();
    }    
    // (Unity) Methods [END]

    // Public Methods [START]
    public override AgentSO GetAgent()
    {
        return agent;
    }
    
    // Public Methods [END]

    // Validation Methods [START]
    private bool Validate_AgentType_IsStructure() { return agent != null && agent.type == AgentTypeEnum.STRUCTURE; }
    // Validation Methods [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////