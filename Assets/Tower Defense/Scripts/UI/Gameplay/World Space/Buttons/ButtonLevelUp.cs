using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

[HideMonoScript]
public class ButtonLevelUp : MonoBehaviour
{
    public Agent agent;

    // Public (Methods) [START]
    public void OnClick() => StructureEvolutionManager.instance.EvolveSpecificStructure(agent.GetComponent<PlayableStructure>());
    // Public (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////