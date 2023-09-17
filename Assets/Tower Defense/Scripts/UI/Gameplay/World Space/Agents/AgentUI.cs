using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Core.Patterns;

using TheraBytes.BetterUi;

[HideMonoScript]
public class AgentUI : MonoBehaviour
{
    // Public (Variables) [START]
    public SizeEnum healthSliderSize;
    public SizeEnum textFloatingValueSize;
    // Public (Variables) [END]

    // Private (Variables) [START]
    private SliderController spawnedHealthSlider;
    private StatusesHorizontalLayoutController spawnedStatusesHorizontalLayout;
    private Agent agent;
    // Private (Variables) [END]    

    // (Unity) Methods [START]
    private void Start()
    {
        InitializeVariables();        
    }
    private void Update()
    {
        CreateHealthSlider();
        CreateStatusesHorizontalLayout();

        UpdateHealthSlider();
        HandleStatusesHorizontalLayoutVisibility();
    }
    // (Unity) Methods [END]

    // Private (Methods) [START]
    private void InitializeVariables()
    {
        agent = GetComponent<Agent>();
    }
    private void CreateStatusesHorizontalLayout()
    {
        if (agent.GetComponent<PlayableStructure>() != null && !agent.GetComponent<PlayableStructure>().IsPlaced)
            return;

        if (spawnedStatusesHorizontalLayout == null)
        {
            spawnedStatusesHorizontalLayout = Poolable.TryGetPoolable(WorldSpaceInterfaceManager.instance.horizontalLayoutSmallStatuses).GetComponent<StatusesHorizontalLayoutController>();
            spawnedStatusesHorizontalLayout.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            spawnedStatusesHorizontalLayout.SetTarget(transform);
            spawnedStatusesHorizontalLayout.SetTargetHeightOffset(agent.mainCollider.bounds.max.y + 1.5f);
            spawnedStatusesHorizontalLayout.agent = agent;
        }
    }
    private void HandleStatusesHorizontalLayoutVisibility()
    {
        if (spawnedStatusesHorizontalLayout != null)
        {
            spawnedStatusesHorizontalLayout.Show();
        }
    }
    private void CreateHealthSlider()
    {
        if (agent.GetComponent<PlayableStructure>() != null && !agent.GetComponent<PlayableStructure>().IsPlaced)
            return;

        if (spawnedHealthSlider == null)
        {
            spawnedHealthSlider = Poolable.TryGetPoolable(WorldSpaceInterfaceManager.instance.GetSlider(healthSliderSize)).GetComponent<SliderController>();
            spawnedHealthSlider.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            spawnedHealthSlider.MinValue = 0f;
            spawnedHealthSlider.MaxValue = agent.MaxHealth;
            spawnedHealthSlider.Value = agent.MaxHealth;
            spawnedHealthSlider.Color = AlignmentManager.instance.GetAlignment(agent.Alignment).color;
            spawnedHealthSlider.SetTarget(transform);
            spawnedHealthSlider.SetTargetHeightOffset(agent.mainCollider.bounds.max.y);
        }
    }
    private void UpdateHealthSlider()
    {
        if (spawnedHealthSlider != null)
        {
            if (agent.ActualHealth == agent.MaxHealth)
                spawnedHealthSlider.Hide();
            else
            {
                spawnedHealthSlider.Show();

                spawnedHealthSlider.Value = agent.ActualHealth;
            }
        }   
    }
    // Private (Methods) [END]

    // Public (Methods) [START]
    public void GenerateFloatingText(float value)
    {
        FloatingTextController sft = Poolable.TryGetPoolable(WorldSpaceInterfaceManager.instance.GetTextFloatingValue(textFloatingValueSize)).GetComponent<FloatingTextController>();
        sft.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        sft.Value = value;
        sft.Color = AlignmentManager.instance.GetAlignment(agent.Alignment).color;
        sft.SetTarget(transform);
        sft.SetTargetHeightOffset(agent.mainCollider.bounds.max.y);
        sft.ForceMotionUpdate();
    }
    public void TryPool()
    {
        HealthSliderTryPool();
        StatusesHorizontalLayoutTryPool();
    }
    public void HealthSliderTryPool()
    {
        if (spawnedHealthSlider != null)
            Poolable.TryPool(spawnedHealthSlider.gameObject);

        spawnedHealthSlider = null;
    }
    public void StatusesHorizontalLayoutTryPool()
    {
        if (spawnedStatusesHorizontalLayout != null)
            Poolable.TryPool(spawnedStatusesHorizontalLayout.gameObject);

        spawnedStatusesHorizontalLayout = null;
    }
    // Public (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////