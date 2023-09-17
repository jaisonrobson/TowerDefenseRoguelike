using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using TheraBytes.BetterUi;

[HideMonoScript]
public class Panel_3_2_1_StructureEvolutionOption_Controller : MonoBehaviour
{
    // Private (Variables) [START]
    private AgentSO evolutionOption;
    // Private (Variables) [END]

    // Public (Variables) [START]
    [Required]
    public BetterImage structureImage;
    [Required]
    public BetterText structureNameText;
    [Required]
    public BetterText healthText;
    [Required]
    public BetterText damageText;
    [Required]
    public BetterText attackVelocityText;
    [Required]
    public BetterText attackRangeText;
    [Required]
    public BetterText areaVisibilityText;
    [Required]
    public BetterText subspawnQuantityText;
    // Public (Variables) [END]

    // Public (Properties) [START]
    public AgentSO EvolutionOption { get { return evolutionOption; } set { evolutionOption = value; } }
    // Public (Properties) [END]

    // (Unity) Methods [START]
    private void Update()
    {
        HandleInformationRefresh();
    }
    // (Unity) Methods [END]


    // Private (Methods) [START]
    private void HandleInformationRefresh()
    {
        structureImage.sprite = evolutionOption.image;
        structureNameText.text = evolutionOption.name;
        healthText.text = evolutionOption.health.ToString();
        damageText.text = evolutionOption.damage.ToString();
        attackVelocityText.text = evolutionOption.attackVelocity.ToString();
        attackRangeText.text = evolutionOption.attackRange.ToString();
        areaVisibilityText.text = evolutionOption.visibilityArea.ToString();


        subspawnQuantityText.text = GetSubspawnsTotalQuantity().ToString();
    }
    private int GetSubspawnsTotalQuantity()
    {
        int result = 0;

        evolutionOption.subspawns.ForEach(ss => result += ss.maxAlive);

        return result;
    }
    // Private (Methods) [END]

    // Public (Methods) [START]
    public void SelectEvolutionOption()
    {
        StructureEvolutionManager.instance.EvolveSelectedEvolution(evolutionOption);
    }
    // Public (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////