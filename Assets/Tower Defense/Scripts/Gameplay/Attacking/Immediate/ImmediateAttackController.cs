using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using Core.Patterns;

[HideMonoScript]
public class ImmediateAttackController : AttackController
{
    // Private (Properties) [START]
    private float StartTime { get; set; }
    private ImmediateAttackAffector IAA { get; set; }
    // Private (Properties) [END]

    // (Unity) Methods [START]
    public override void OnEnable()
    {
        base.OnEnable();

        ResetVariables();
    }
    public void FixedUpdate()
    {
        HandleAttacking();

        HandleAttackExistanceByDuration();
    }
    // (Unity) Methods [END]

    // Private (Methods) [START]
    private void ResetVariables()
    {
        IAA = GetComponent<ImmediateAttackAffector>();

        StartTime = Time.time;
    }
    private void HandleAttacking()
    {
        if (!IsAgentAlreadyAffected(IAA.Target))
        {
            AddAffectedAgent(IAA.Target);

            IAA.Target.OnReceiveDamageByDirectAttack(IAA.Alignment, IAA.Damage, IAA.Attack, IAA.Invoker);

            IAA.Attack.onHitProbabilityStatusAffectors
                .ToList()
                .ForEach(sap => StatusAffecting.TryInvokeStatus(sap, IAA.Invoker, IAA.Target));

            Transform animationOrigin = IAA.Invoker.GetAnimationOriginOfAttack(IAA.Attack).animationOrigin;

            Animating.InvokeAnimation(IAA.Attack.finalAnimation, IAA.Target.transform.position, animationOrigin.rotation, IAA.Duration);
            AudioPlaying.InvokeSound(IAA.Attack.finalSound, IAA.Target.transform.position);

            Finished = true;
        }
    }
    private void HandleAttackExistanceByDuration()
    {
        if (Time.time > (StartTime + IAA.Duration))
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