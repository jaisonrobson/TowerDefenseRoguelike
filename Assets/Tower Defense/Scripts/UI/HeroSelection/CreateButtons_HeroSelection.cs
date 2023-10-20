using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

[HideMonoScript]
public class CreateButtons_HeroSelection : MonoBehaviour
{
    // Public (Variables) [START]
    [Required]
    [AssetsOnly]
    public GameObject buttonPrefab;

    [Required]
    [SceneObjectsOnly]
    public Transform buttonsParent;
    // Public (Variables) [END]

    // (Unity) Methods [START]
    void Start()
    {
        CreateButtons();
    }

    void Update()
    {
        
    }
    // (Unity) Methods [END]


    // Private (Methods) [START]
    private void CreateButtons()
    {
        List<AgentSO> heroes = Resources.LoadAll<AgentSO>("SO's/Agents").ToList().Where(agt => agt.subtype == AgentSubTypeEnum.HERO).ToList();

        heroes.ForEach(h => {
            Button_HeroSelection newButton = Instantiate(buttonPrefab).GetComponent<Button_HeroSelection>();

            newButton.heroSO = h;

            newButton.transform.SetParent(buttonsParent);
        });
    }
    // Private (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////