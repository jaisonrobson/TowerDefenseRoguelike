using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
using Core.Physics;
using Core.General;

[HideMonoScript]
public class AgentEnemyDetectionColliderManager : MonoBehaviour
{
    // Private (Variables) [START]
    [BoxGroup("Identity")]
    [PropertyOrder(1)]
    [ShowInInspector]
    [HideInEditorMode]
    [ReadOnly]
    private AlignmentEnum alignment;
    [BoxGroup("Identity")]
    [PropertyOrder(2)]
    [ShowInInspector]
    [HideInEditorMode]
    [ReadOnly]
    private Agent agent;
    private Collider detectionCollider;
    // Private (Variables) [END]

    // Public (Properties) [START]
    public Collider DetectionCollider { get { return detectionCollider; } set { detectionCollider = value; }  }
    // Public (Properties) [END]

    // Unity Methods [START]
    private void OnEnable()
    {
        ResetAlignment();
        ResetAgent();
    }
    private void Update()
    {
        ResetAlignment();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (IsSelfGameObjectCollider(other.transform)) return;

        if (other.gameObject.layer == LayerMask.NameToLayer("Creature"))
        {
            Agent otherAgent = other.gameObject.GetComponentInParent<Agent>();

            if (otherAgent != null)
            {
                if (IsMainGoal(otherAgent))
                    return;

                if (IsAlignmentAnOpponent(otherAgent.Alignment))
                {
                    if (!IsPriorityGoal(otherAgent) && !otherAgent.IsDead)
                    {
                        ReliableOnTriggerExit.NotifyTriggerEnter(other, gameObject, OnTriggerExit);

                        agent.AddPriorityGoal(otherAgent);
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsSelfGameObjectCollider(other.transform)) return;

        if (other.gameObject.layer == LayerMask.NameToLayer("Creature"))
        {
            Agent otherAgent = other.gameObject.GetComponentInParent<Agent>(true);

            if (otherAgent != null)
            {
                if (IsMainGoal(otherAgent))
                    return;

                if (IsAlignmentAnOpponent(otherAgent.Alignment))
                {
                    if (IsPriorityGoal(otherAgent))
                    {
                        ReliableOnTriggerExit.NotifyTriggerExit(other, gameObject);

                        agent.RemovePriorityGoal(otherAgent);
                    }
                }
            }
        }
    }
    // Unity Methods [END]


    // Private Methods [START]
    private bool IsSelfGameObjectCollider(Transform collider) => Utils.IsGameObjectInsideAnother(collider, transform.parent);
    private void ResetAgent() { agent = GetComponentInParent<Agent>(true); }
    private void ResetAlignment() { alignment = transform.parent.GetComponent<Agent>().Alignment; }
    private bool IsAlignmentAnOpponent(AlignmentEnum pAlignment) { return AlignmentManager.instance.IsAlignmentAnOpponent(pAlignment, alignment); }
    private bool IsMainGoal(Agent targetAgent) { return agent.IsMainGoal(targetAgent); }
    private bool IsPriorityGoal(Agent targetAgent) { return agent.IsPriorityGoal(targetAgent); }
    // Private Methods [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////