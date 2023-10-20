using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using TheraBytes.BetterUi;

[HideMonoScript]
public class Button_HeroSelection : MonoBehaviour
{
    // Public (Variables) [START]
    public AgentSO heroSO;
    [Required]
    public BetterText text;
    // Public (Variables) [END]

    // (Unity) Methods [START]
    private void Update()
    {
        HandleTextUpdate();
    }
    // (Unity) Methods [END]

    // Private (Methods) [START]
    private void HandleTextUpdate()
    {
        text.text = heroSO.name;
    }
    // Private (Methods) [END]

    // Public (Methods) [START]
    public void OnClick()
    {
        HeroSelectionController.instance.ChangeSelectedHero(heroSO);
    }
    // Public (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////