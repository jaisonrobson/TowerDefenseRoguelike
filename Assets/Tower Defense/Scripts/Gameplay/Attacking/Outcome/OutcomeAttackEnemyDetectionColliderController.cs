using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using Core.Patterns;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
[HideMonoScript]
public class OutcomeAttackEnemyDetectionColliderController : MonoBehaviour, IPoolable
{
    // Public (Variables) [START]
    [PropertyTooltip("The duration/existance of the collision before it gets disabled")]
    public float duration = 1f;
    [Range(0, 100)]
    [PropertyTooltip("The percentage the calculation will take account on the damage value before passing the value to the enemy")]
    public int damagePercentage = 100;
    // Public (Variables) [END]

    // Private (Variables) [START]
    [ShowInInspector]
    [ReadOnly]
    private List<GameObject> affectedAgents;
    private float startTime;
    // Private (Variables) [END]

    // Public (Properties) [START]
    [ShowInInspector]
    [ReadOnly]
    public bool Finished { get; private set; }
    // Public (Properties) [END]

    // (Unity) Methods [START]
    private void OnEnable()
    {
        ResetVariables();
    }
    private void Update()
    {
        HandleExistanceByTime();
    }
    private void OnTriggerEnter(Collider other) => HandleAttacking(other.gameObject);
    private void OnTriggerStay(Collider other) => HandleAttacking(other.gameObject);
    // (Unity) Methods [END]

    // Private (Methods) [START]
    private void HandleExistanceByTime()
    {
        if (Time.time > (startTime + duration))
        {
            Finished = true;
            
            gameObject.SetActive(false);
        }
    }
    private void HandleAttacking(GameObject other)
    {
        if (other != gameObject)
        {
            OutcomeAttackAffector oaa = GetComponentInParent<OutcomeAttackAffector>();

            if (oaa != null && oaa.IsInLayerMask(other.layer))
            {
                Agent enemy = other.GetComponentInChildren<Agent>();

                if (enemy == null)
                    enemy = other.GetComponentInParent<Agent>();

                if (enemy != null)
                {
                    if (oaa.IsAlignmentAnOpponent(enemy.Alignment) || enemy.IsAgentUnderStatusConfusion)
                    {
                        OutcomeAttackController oac = GetComponentInParent<OutcomeAttackController>();

                        if (oac != null && oac.IsRunning)
                        {
                            if (!IsAgentAlreadyAffected(enemy.gameObject))
                            {
                                affectedAgents.Add(enemy.gameObject);

                                enemy.OnReceiveDamageByDirectAttack(oaa.Alignment, oaa.Damage * (damagePercentage / 100), oaa.Attack, oaa.Invoker);

                                oaa.Attack.onHitProbabilityStatusAffectors
                                    .ToList()
                                    .ForEach(sap => StatusAffecting.TryInvokeStatus(sap, oaa.Invoker, enemy));
                            }
                        }
                    }
                }
            }
        }
    }
    private void ResetVariables()
    {
        if (affectedAgents == null)
            affectedAgents = new List<GameObject>();
        else
            affectedAgents.Clear();

        startTime = Time.time;

        Finished = false;
    }
    // Private (Methods) [END]

    // Public (Methods) [START]
    public bool IsAgentAlreadyAffected(GameObject agent) => affectedAgents.Any(aa => aa.GetInstanceID() == agent.GetInstanceID());
    public void StartOutcomeCollider()
    {
        gameObject.SetActive(true);
    }
    public virtual void PoolRetrievalAction(Poolable poolable)
    {
        gameObject.SetActive(false);

        ResetVariables();
    }

    public virtual void PoolInsertionAction(Poolable poolable)
    {
        gameObject.SetActive(false);

        ResetVariables();
    }
    // Public (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////