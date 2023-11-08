using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using Core.Patterns;
using Core.General;

[HideMonoScript]
public class HeroSelectionController : Singleton<HeroSelectionController>
{
    // Public (Variables) [START]
    [Required]
    [SceneObjectsOnly]
    public Transform heroObjectParent;
    // Public (Variables) [END]

    // Private (Variables) [START]
    private AgentSO selectedHero;
    private GameObject heroVisualInstance;
    // Private (Variables) [END]


    // (Unity) Methods [START]
    private void Update()
    {
        HandleHeroVisualInstanceRotation();
    }
    // (Unity) Methods [END]

    // Private (Methods) [START]
    private void HandleHeroInstantiating()
    {
        if (heroVisualInstance != null)
            Destroy(heroVisualInstance);

        heroVisualInstance = Instantiate(selectedHero.visualPrefab);
        heroVisualInstance.transform.SetParent(heroObjectParent);
        heroVisualInstance.transform.localPosition = Vector3.zero;
        heroVisualInstance.transform.localScale = Vector3.one;
        Utils.SetGameObjectAndChildrenLayers(heroVisualInstance.transform, LayerMask.NameToLayer("UI"));
    }
    private void HandleHeroVisualInstanceRotation()
    {
        if (heroVisualInstance != null)
        {
            heroVisualInstance.transform.Rotate(Vector3.up * Time.deltaTime * 5f);
        }
    }
    // Private (Methods) [END]

    // Public (Methods) [START]
    public void ChangeSelectedHero(AgentSO hero)
    {
        selectedHero = hero;

        HandleHeroInstantiating();

        CreateButtons_HeroSelection.instance.SelectHero(hero);

        PlayerPrefs.SetString("selectedHero", hero.name);

        PlayerPrefs.Save();
    }
    // Public (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////