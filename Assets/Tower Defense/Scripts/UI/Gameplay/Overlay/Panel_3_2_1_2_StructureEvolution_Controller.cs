using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

[HideMonoScript]
public class Panel_3_2_1_2_StructureEvolution_Controller : MonoBehaviour
{
    // Public (Variables) [START]
    [Required]
    [AssetsOnly]
    public GameObject structureEvolutionOptionPrefab;
    // Public (Variables) [END]

    // Private (Variables) [START]
    private AgentSO actualSelectedAgent;
    [ShowInInspector]
    private List<GameObject> evolutionOptions;
    // Private (Variables) [END]

    // (Unity) Methods [START]
    void OnEnable()
    {
        InitializeVariables();
    }

    void Update()
    {
        HandleEvolutionOptionsCreation();
    }
    // (Unity) Methods [END]


    // Private (Methods) [START]
    private void InitializeVariables()
    {
        actualSelectedAgent = SelectionManager.instance.SelectedAgents.FirstOrDefault()?.GetComponent<Agent>().GetAgent();

        if (evolutionOptions != null)
        {
            evolutionOptions.ForEach(eo => Destroy(eo));

            evolutionOptions.Clear();
        }
        else
            evolutionOptions = new List<GameObject>();
    }
    private void HandleEvolutionOptionsCreation()
    {
        actualSelectedAgent.evolutionTree.ForEach(
            agtSO =>
            {
                if (!evolutionOptions.Any(eo => eo.GetComponent<Panel_3_2_1_StructureEvolutionOption_Controller>().EvolutionOption == agtSO))
                {
                    GameObject newEvolutionOption = Instantiate(structureEvolutionOptionPrefab);
                    newEvolutionOption.GetComponent<Panel_3_2_1_StructureEvolutionOption_Controller>().EvolutionOption = agtSO;
                    newEvolutionOption.transform.SetParent(transform);

                    evolutionOptions.Add(newEvolutionOption);
                }
            }
        );
    }
    // Private (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////