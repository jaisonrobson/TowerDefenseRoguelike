using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using Core.Patterns;
using Core.Math;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(RangedAttackEnemyDetectionColliderController))]
[HideMonoScript]
public class RangedAttackController : AttackController
{
    // Private (Properties) [START]
    private float StartTime { get; set; }
    private float TravelDuration { get; set; }
    // Private (Properties) [END]

    // Public (Properties) [START]
    public float ExistanceDuration { get; private set; }
    public bool IsRangedAttackDurationEnded { get { return Time.fixedTime > ExistanceDuration && !Mathf.Approximately(0f, ExistanceDuration); } }
    // Public (Properties) [END]

    // (Unity) Methods [START]
    public override void OnEnable()
    {
        base.OnEnable();

        ResetVariables();
    }
    public void FixedUpdate()
    {
        HandleAttackMovement();
        HandleAttackExistanceByDuration();
    }
    // (Unity) Methods [END]

    // Private (Methods) [START]
    private void ResetVariables()
    {
        RangedAttackAffector raa = GetComponent<RangedAttackAffector>();

        StartTime = Time.fixedTime;
        ExistanceDuration = Time.fixedTime + raa.Duration + raa.Speed; //The ranged attack must exist at least during the travel path completion
        TravelDuration = Time.fixedTime + raa.Speed;
    }
    private void HandleAttackMovement()
    {
        if (Time.fixedTime > ExistanceDuration)
            return;

        RangedAttackAffector raa = GetComponent<RangedAttackAffector>();

        switch (GetComponent<RangedAttackAffector>().travellingType)
        {
            case AttackTravellingTypeEnum.ARCH:
                transform.position = Slerp.EvaluateSlerpPointsVector3(raa.Origin, raa.Destination, Mathf.Abs((raa.Origin - raa.Destination).y) * 2, StartTime, TravelDuration);
                break;
            default:
                float travellingFractionTime = Time.fixedTime / ExistanceDuration;
                
                transform.position = Vector3.Lerp(raa.Origin, raa.Destination, travellingFractionTime);
                break;
        }
    }
    private void HandleAttackExistanceByDuration()
    {
        if (Time.fixedTime > ExistanceDuration)
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