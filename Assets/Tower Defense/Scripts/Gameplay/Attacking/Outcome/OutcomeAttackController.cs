using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using Core.Patterns;
using Core.General;

[Serializable]
public struct OutcomeCollisionConfiguration
{
    [Required]
    [AssetsOnly]
    public OutcomeAttackEnemyDetectionColliderController controller;
    [Range(0, 60)]
    [PropertyTooltip("Time in seconds this outcome will be executed before the script start")]
    public float timeInSequence;
    public bool activated;

    public OutcomeCollisionConfiguration(OutcomeAttackEnemyDetectionColliderController pController)
    {
        controller = pController;
        timeInSequence = 0f;
        activated = false;
    }

    public void Activate() => activated = true;
    public void Deactivate() => activated = false;
}

[RequireComponent(typeof(Poolable))]
[HideMonoScript]
public class OutcomeAttackController : AttackController
{
    // Public (Variables) [START]
    [OnInspectorInit("MaintainOutcomeCollisions")]
    public List<OutcomeCollisionConfiguration> outcomeCollisions;
    // Public (Variables) [END]

    // Private (Variables) [START]
    [ReadOnly]
    [ShowInInspector]
    private bool isRunning = false;
    private float startTime = 0f;
    private OutcomeAttackAffector affector;
    // Private (Variables) [END]

    // Public (Properties) [START]
    public bool IsRunning { get { return isRunning; } }
    // Public (Properties) [END]

    // (Unity) Methods [START]
    public override void OnEnable()
    {
        base.OnEnable();

        ResetVariables();
    }
    public override void Update()
    {
        base.Update();

        HandleOutcomeSequencesActivation();
        HandleOutcomeEnding();
    }
    // (Unity) Methods [END]

    // Private (Methods) [START]
    private void ResetVariables()
    {
        isRunning = false;
        outcomeCollisions = Utils.UpdateValueInStructList(outcomeCollisions, oc => { oc.Deactivate(); return oc; }).ToList();
        transform.position = Vector3.zero;
        affector = GetComponent<OutcomeAttackAffector>();
    }
    private void HandleOutcomeSequencesActivation()
    {
        if (isRunning)
        {
            float sequenceTiming = Time.time - startTime;

            outcomeCollisions = Utils.UpdateValueInStructList(outcomeCollisions, oc => {
                if (sequenceTiming > oc.timeInSequence)
                    if (!oc.activated)
                    {
                        oc.Activate();

                        oc.controller.StartOutcomeCollider();
                    }

                return oc;
            }).ToList();
        }
    }
    private void HandleOutcomeEnding()
    {
        if (
            isRunning
            && Time.time > (startTime - affector.Duration)
            && outcomeCollisions.All(oc => oc.activated && oc.controller.Finished)
        )
            Poolable.TryPool(gameObject);
    }
    // Private (Methods) [END]

    // Public (Methods) [START]
    public void StartOutcome()
    {
        transform.position = affector.Origin;
        transform.rotation = affector.InitialRotation;
        startTime = Time.time;
        isRunning = true;
    }
    public override void PoolRetrievalAction(Poolable poolable)
    {
        base.PoolRetrievalAction(poolable);

        ResetVariables();

        outcomeCollisions = Utils.UpdateValueInStructList(outcomeCollisions, oc => {
            oc.controller.PoolRetrievalAction(poolable);

            return oc;
        }).ToList();
    }

    public override void PoolInsertionAction(Poolable poolable)
    {
        base.PoolInsertionAction(poolable);

        ResetVariables();

        outcomeCollisions = Utils.UpdateValueInStructList(outcomeCollisions, oc => {
            oc.controller.PoolInsertionAction(poolable);

            return oc;
        }).ToList();
    }
    // Public (Methods) [END]

    // (Validation) Methods [START]
    private void MaintainOutcomeCollisions()
    {
        if (outcomeCollisions == null)
            outcomeCollisions = new List<OutcomeCollisionConfiguration>();

        GetComponentsInChildren<OutcomeAttackEnemyDetectionColliderController>(true)
            .ToList()
            .ForEach(occ => {
                if (!outcomeCollisions.Any(oc => oc.controller.GetInstanceID() == occ.GetInstanceID()))
                    outcomeCollisions.Add(new OutcomeCollisionConfiguration(occ));
            });
    }
    // (Validation) Methods [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////