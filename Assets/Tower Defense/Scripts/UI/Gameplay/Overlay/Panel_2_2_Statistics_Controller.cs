using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using TheraBytes.BetterUi;

[HideMonoScript]
public class Panel_2_2_Statistics_Controller : MonoBehaviour
{
    // Public (Variables) [START]
    [FoldoutGroup("Quantitative UI Information")]
    [Required]
    public BetterSlider healthSlider;
    [FoldoutGroup("Quantitative UI Information")]
    [Required]
    public BetterText healthText;
    [FoldoutGroup("Quantitative UI Information")]
    [Required]
    public BetterText damageText;
    [FoldoutGroup("Quantitative UI Information")]
    [Required]
    public BetterText attackVelocityText;
    [FoldoutGroup("Quantitative UI Information")]
    [Required]
    public BetterText attackRangeText;
    [FoldoutGroup("Quantitative UI Information")]
    [Required]
    public BetterText velocityText;
    [FoldoutGroup("Quantitative UI Information")]
    [Required]
    public BetterText visibilityText;
    [FoldoutGroup("Quantitative UI Information")]
    [Required]
    public BetterText evasionText;
    [FoldoutGroup("UI Items")]
    [Required]
    public GameObject healthItem;
    [FoldoutGroup("UI Items")]
    [Required]
    public GameObject damageItem;
    [FoldoutGroup("UI Items")]
    [Required]
    public GameObject attackVelocityItem;
    [FoldoutGroup("UI Items")]
    [Required]
    public GameObject attackRangeItem;
    [FoldoutGroup("UI Items")]
    [Required]
    public GameObject velocityItem;
    [FoldoutGroup("UI Items")]
    [Required]
    public GameObject visibilityItem;
    [FoldoutGroup("UI Items")]
    [Required]
    public GameObject evasionItem;
    [FoldoutGroup("UI Items")]
    // Public (Variables) [END]


    // (Unity) Methods [START]
    private void Update()
    {
        HandleStatisticsUpdate();
        HandleStatisticsVisibility();
    }
    // (Unity) Methods [END]


    // Private (Methods) [START]
    private void HandleStatisticsUpdate()
    {
        Agent agt = SelectionManager.instance.SelectedAgents.FirstOrDefault()?.GetComponent<Agent>();

        if (agt != null)
        {
            healthSlider.minValue = 0f;
            healthSlider.value = agt.ActualHealth;
            healthSlider.maxValue = agt.MaxHealth;
            healthText.text = agt.ActualHealth.ToString("0.#") + " / " + agt.MaxHealth.ToString("0.#");

            damageText.text = agt.Damage.ToString("0.#");

            attackVelocityText.text = agt.AttackVelocity.ToString("0.#");

            attackRangeText.text = agt.AttackRange.ToString("0.#");

            velocityText.text = agt.Velocity.ToString("0.#");

            visibilityText.text = agt.VisibilityArea.ToString("0.#");

            evasionText.text = agt.Evasion.ToString();
        }
    }
    private void HandleStatisticsVisibility()
    {
        Agent agt = SelectionManager.instance.SelectedAgents.FirstOrDefault()?.GetComponent<Agent>();

        if (agt != null)
        {
            AgentFsmAi afai = agt.gameObject.GetComponent<AgentFsmAi>();

            switch (agt.GetAgent().type)
            {
                case AgentTypeEnum.CREATURE:
                    healthItem.SetActive(true);

                    damageItem.SetActive(afai.IsAggressive);
                    attackVelocityItem.SetActive(afai.IsAggressive);
                    attackRangeItem.SetActive(afai.IsAggressive);
                    velocityItem.SetActive(true);
                    visibilityItem.SetActive(true);
                    evasionItem.SetActive(true);
                    break;
                case AgentTypeEnum.STRUCTURE:
                    healthItem.SetActive(true);

                    damageItem.SetActive(afai.IsAggressive);
                    attackVelocityItem.SetActive(afai.IsAggressive);
                    attackRangeItem.SetActive(afai.IsAggressive);
                    velocityItem.SetActive(false);
                    visibilityItem.SetActive(true);
                    evasionItem.SetActive(false);
                    break;
            }
        }
    }
    // Private (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////