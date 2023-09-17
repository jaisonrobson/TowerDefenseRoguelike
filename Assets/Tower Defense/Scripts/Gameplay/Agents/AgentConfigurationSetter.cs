using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[HideMonoScript]
[RequireComponent(typeof(Agent))]
public class AgentConfigurationSetter : MonoBehaviour
{
    // Public (Variables) [START]
    public AlignmentEnum alignment;
    // Public (Variables) [END]

    // (Unity) Methods [START]
    private void OnEnable()
    {
        GetComponent<Agent>().Alignment = alignment;
    }
    // (Unity) Methods [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////