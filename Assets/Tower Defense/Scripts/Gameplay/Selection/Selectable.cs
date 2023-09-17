using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[RequireComponent(typeof(Agent))]
[HideMonoScript]
public class Selectable : MonoBehaviour
{
    // Public (Variables) [START]

    // Public (Variables) [END]

    // Private (Variables) [START]
    private AgentTypeEnum selectableType;
    // Private (Variables) [END]

    // Public (Properties) [START]
    public AgentTypeEnum SelectableType { get { return selectableType; } }
    // Public (Properties) [END]

    // (Unity) Methods [START]
    private void OnEnable()
    {
        InitializeVariables();
    }
    private void Update()
    {
        
    }
    // (Unity) Methods [END]

    // Private (Methods) [START]
    private void InitializeVariables()
    {
        Agent agt = GetComponent<Structure>();
        if (agt != null)
            selectableType = AgentTypeEnum.STRUCTURE;
        else
            selectableType = AgentTypeEnum.CREATURE;
    }
    // Private (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////