using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

[HideMonoScript]
public class Panel_7_1_AgentPlacement_Controller : MonoBehaviour
{
    // Public (Variables) [START]
    [Required]
    [SceneObjectsOnly]
    public GameObject content;
    [Required]
    [AssetsOnly]
    public GameObject optionPrefab;
    // Public (Variables) [END]

    // Private (Variables) [START]
    private List<GameObject> options;
    // Private (Variables) [END]

    // (Unity) Methods [START]
    public void OnEnable()
    {
        InitializeVariables();
    }
    private void OnDisable()
    {
        ResetAgentPlacementVariables();
    }
    // (Unity) Methods [END]

    // Private (Methods) [START]
    private void ResetAgentPlacementVariables()
    {
        AgentPlacementController.instance.CurrentPlacementArea = null;
    }
    private void InitializeVariables()
    {
        InitializeOptions();
    }
    private void InitializeOptions()
    {
        if (options != null)
        {
            options.ForEach(opt => Destroy(opt));
            options.Clear();
        }
        else
            options = new List<GameObject>();

        options.AddRange(Enumerable.Range(0, AgentPlacementController.instance.LevelPlayableStructures.Count).ToList().Select(index => {
            GameObject newOption = Instantiate(optionPrefab);

            newOption.GetComponent<Panel_7_1_2_Button_AgentPlacementOption_Controller>().PlayableAgentSO = AgentPlacementController.instance.LevelPlayableStructures.ElementAt(index);
            newOption.transform.SetParent(content.transform);

            return newOption;
        }));
    }
    // Private (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////