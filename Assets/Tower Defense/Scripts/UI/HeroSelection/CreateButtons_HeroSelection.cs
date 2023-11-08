using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using Core.Patterns;
using TheraBytes.BetterUi;

[HideMonoScript]
public class CreateButtons_HeroSelection : Singleton<CreateButtons_HeroSelection>
{
    // Public (Variables) [START]
    [Required]
    [AssetsOnly]
    public GameObject buttonPrefab;

    [Required]
    [SceneObjectsOnly]
    public Transform buttonsParent;
    // Public (Variables) [END]

    // Private (Variables) [START]
    private List<Button_HeroSelection> instantiatedButtons;
    // Private (Variables) [END]

    // (Unity) Methods [START]
    void Start()
    {
        CreateButtons();
        HandleHeroInitialSelection();
    }

    void Update()
    {
        
    }
    // (Unity) Methods [END]


    // Private (Methods) [START]
    private void CreateButtons()
    {
        instantiatedButtons = new List<Button_HeroSelection>();

        List<AgentSO> heroes = Resources.LoadAll<AgentSO>("SO's/Agents").ToList().Where(agt => agt.subtype == AgentSubTypeEnum.HERO).ToList();

        heroes.ForEach(h => {
            Button_HeroSelection newButton = Instantiate(buttonPrefab).GetComponent<Button_HeroSelection>();

            newButton.heroSO = h;

            newButton.transform.SetParent(buttonsParent);

            instantiatedButtons.Add(newButton);
        });
    }
    private void HandleHeroInitialSelection()
    {
        string heroName = PlayerPrefs.GetString("selectedHero");

        instantiatedButtons.ForEach(ib => {
            if (ib.heroSO.name == heroName)
                HeroSelectionController.instance.ChangeSelectedHero(ib.heroSO);
        });
    }
    // Private (Methods) [END]

    // Public (Methods) [START]
    public void SelectHero(AgentSO hero)
    {
        instantiatedButtons.ForEach(ib => {
            if (ib.heroSO == hero)
                ib.GetComponent<BetterButton>().interactable = false;
            else
                ib.GetComponent<BetterButton>().interactable = true;
        });
    }
    // Public (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////