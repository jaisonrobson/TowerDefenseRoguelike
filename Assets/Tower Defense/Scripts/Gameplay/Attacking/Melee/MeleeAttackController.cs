using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using Core.Patterns;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(MeleeAttackEnemyDetectionColliderController))]
[HideMonoScript]
public class MeleeAttackController : AttackController
{
    // Private (Properties) [START]
    [ReadOnly]
    [ShowInInspector]
    private float StartTime { get; set; }
    [ReadOnly]
    [ShowInInspector]
    private float Duration { get; set; }
    // Private (Properties) [END]

    // (Unity) Methods [START]
    public override void OnEnable()
    {
        base.OnEnable();

        ResetVariables();
    }
    public void FixedUpdate()
    {
        HandleAttackPositioning();
        HandleAttackExistanceByDuration();
    }
    // (Unity) Methods [END]

    // Private (Methods) [START]
    private void ResetVariables()
    {
        MeleeAttackAffector maa = GetComponent<MeleeAttackAffector>();

        StartTime = Time.fixedTime;
        Duration = Time.fixedTime + maa.Duration;
    }
    private void HandleAttackPositioning()
    {
        if (Time.fixedTime > Duration)
            return;

        MeleeAttackAffector maa = GetComponent<MeleeAttackAffector>();

        transform.position = maa.Origin;
        transform.rotation = maa.InitialRotation;
    }
    private void HandleAttackExistanceByDuration()
    {
        if (Time.fixedTime > Duration)
            Finished = true;
    }
    // Private (Methods) [END]

    // Public (Methods) [START]
    public override void PoolRetrievalAction(Poolable poolable)
    {
        base.PoolRetrievalAction(poolable);

        ResetVariables();
    }
    public override void PoolInsertionAction(Poolable poolable)
    {
        base.PoolInsertionAction(poolable);
    }
    // Public (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////