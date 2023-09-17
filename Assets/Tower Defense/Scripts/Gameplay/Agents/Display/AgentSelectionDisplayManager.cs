using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using Core.General;

[HideMonoScript]
public class AgentSelectionDisplayManager : MonoBehaviour
{
    // Private (Variables) [START]
    private GameObject agentSelectionDisplayGObj;
    // Private (Variables) [END]

    // Public (Properties) [START]
    public GameObject AgentSelectionDisplayGObj { get { return agentSelectionDisplayGObj; } set { agentSelectionDisplayGObj = value; } }
    // Public (Properties) [END]

    // (Unity) Methods [START]
    private void Update()
    {
        HandleDisplayVisibility();
    }
    // (Unity) Methods [END]

    // Private (Methods) [START]
    private void HandleDisplayVisibility()
    {
        agentSelectionDisplayGObj.SetActive(IsSelected());
    }
    private bool IsSelected() => SelectionManager.instance.SelectedAgents.Any(sl => Utils.IsGameObjectInsideAnother(transform, sl.transform));
    // Private (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////