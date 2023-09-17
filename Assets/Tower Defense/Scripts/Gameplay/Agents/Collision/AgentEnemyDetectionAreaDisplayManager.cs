using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Core.General;

public class AgentEnemyDetectionAreaDisplayManager : MonoBehaviour
{
    // Private (Variables) [START]
    private GameObject areaDisplayGameObject;
    // Private (Variables) [END]

    // Public (Properties) [START]
    public GameObject AreaDisplayGameObject { get { return areaDisplayGameObject; } set { areaDisplayGameObject = value; } }
    // Public (Properties) [END]

    // (Unity) Methods [START]
    private void Update()
    {
        HandleAreaDisplayVisibility();
    }
    // (Unity) Methods [END]

    // Private (Methods) [START]
    private void HandleAreaDisplayVisibility()
    {
        areaDisplayGameObject.SetActive(IsSelected());
    }
    private bool IsSelected() => SelectionManager.instance.SelectedAgents.Any(sl => Utils.IsGameObjectInsideAnother(transform, sl.transform));
    // Private (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////