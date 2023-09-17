using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Core.Patterns;

public static class Attacking
{
	public static void InvokeAttack(AttackSO attack, Agent invoker, Agent target)
    {
        if (attack == null || invoker == null || target == null)
            return;

        float duration = Mathf.Clamp(invoker.CalculateAttackVelocity(attack), 1f, Mathf.Infinity);

        AttackOrigin attackOriginConfiguration = invoker.GetAttackOriginOfAttack(attack);
        Transform attackOrigin = attackOriginConfiguration.attackOrigin;

        GameObject newAttack = Poolable.TryGetPoolable(
            attack.prefab,
            (Poolable pNewAttackPoolable) => {
                pNewAttackPoolable.transform.position = attackOrigin.position;

                pNewAttackPoolable.gameObject.GetComponent<Affector>().Alignment = invoker.Alignment;
                pNewAttackPoolable.gameObject.GetComponent<Affector>().Invoker = invoker;
                pNewAttackPoolable.gameObject.GetComponent<Affector>().Target = target;
                pNewAttackPoolable.gameObject.GetComponent<AttackAffector>().Attack = attack;
                pNewAttackPoolable.gameObject.GetComponent<AttackAffector>().Damage = invoker.Damage;
                pNewAttackPoolable.gameObject.GetComponent<AttackAffector>().Duration = duration;

                //Remover esse trecho quando houver um modelo real no prefab (caso contrario vai substituir material do modelo)
                if (pNewAttackPoolable.gameObject.GetComponent<MeshRenderer>() != null)
                {
                    List<AlignmentMaterialsSO> amSOs = Resources.LoadAll<AlignmentMaterialsSO>("SO's/Alignment Materials").ToList();
                    AlignmentMaterialsSO alignmentMaterial = amSOs.Where(am => am.alignment.alignment == invoker.Alignment).FirstOrDefault();
                    pNewAttackPoolable.gameObject.GetComponent<MeshRenderer>().material = alignmentMaterial.ghost_structures;
                }

                switch (attack.type)
                {
                    case AttackTypeEnum.RANGED:
                        pNewAttackPoolable.gameObject.GetComponent<RangedAttackAffector>().Origin = attackOrigin.position;
                        pNewAttackPoolable.gameObject.GetComponent<RangedAttackAffector>().Destination = target.transform.position;
                        pNewAttackPoolable.gameObject.GetComponent<RangedAttackAffector>().Speed = invoker.CalculateAttackVelocityPerSecond(attack);
                        break;
                    case AttackTypeEnum.MELEE:
                        Vector3 localInitialOrigin = Vector3.Lerp(attackOrigin.position, target.transform.position, 0.5f);
                        Vector3 direction = (target.transform.position - attackOrigin.position).normalized;

                        Quaternion localInitialRotation = Quaternion.LookRotation(direction) * attack.prefab.transform.rotation;
                        localInitialRotation = Quaternion.Euler(0, localInitialRotation.eulerAngles.y, 0);

                        pNewAttackPoolable.gameObject.GetComponent<MeleeAttackAffector>().Origin = localInitialOrigin;
                        pNewAttackPoolable.gameObject.GetComponent<MeleeAttackAffector>().InitialRotation = localInitialRotation;
                        break;
                    case AttackTypeEnum.IMMEDIATE:
                        break;
                }
            }
        );

        newAttack.transform.position = attackOrigin.position;

        Transform animationOrigin = invoker.GetAnimationOriginOfAttack(attack).animationOrigin;

        Animating.InvokeAnimation(attack.initialAnimation, animationOrigin.position, animationOrigin.rotation, duration);
        AudioPlaying.InvokeSound(attack.initialSound, invoker.transform.position);

        if (attack.trailAnimation != null)
        {
            Animating.InvokeTrailAnimation(attack.trailAnimation, newAttack, duration);

            if (attack.trailSound != null)
                AudioPlaying.InvokeSound(attack.trailSound, newAttack);
        }
    }

    public static void InvokeOutcome(Agent pInvoker, Vector3 pPosition, Vector3 pDirection, AlignmentEnum pAlignment, LayerMask pAffectedsMask, AttackSO pAttack, float pDamage)
    {
        

        GameObject newOutcomeAttack = Poolable.TryGetPoolable(
            pAttack.outcomePrefab,
            (Poolable newOutcomeAttackPoolable) => {
                float duration = newOutcomeAttackPoolable.gameObject.GetComponent<OutcomeAttackAffector>().GetOutComeTotalDuration();

                newOutcomeAttackPoolable.gameObject.GetComponent<Affector>().Alignment = pAlignment;
                newOutcomeAttackPoolable.gameObject.GetComponent<Affector>().Invoker = pInvoker;
                newOutcomeAttackPoolable.gameObject.GetComponent<AttackAffector>().Attack = pAttack;
                newOutcomeAttackPoolable.gameObject.GetComponent<AttackAffector>().Damage = pDamage;
                newOutcomeAttackPoolable.gameObject.GetComponent<AttackAffector>().Duration = duration;
                newOutcomeAttackPoolable.gameObject.GetComponent<OutcomeAttackAffector>().Origin = pPosition;

                Quaternion localInitialRotation = Quaternion.LookRotation(pDirection);
                localInitialRotation = Quaternion.Euler(0, localInitialRotation.eulerAngles.y, 0);

                newOutcomeAttackPoolable.gameObject.GetComponent<OutcomeAttackAffector>().InitialRotation = localInitialRotation;

                List<MeshRenderer> mrs = newOutcomeAttackPoolable.gameObject.GetComponentsInChildren<MeshRenderer>(true).ToList();

                if (mrs.Count > 0)
                {
                    List<AlignmentMaterialsSO> amSOs = Resources.LoadAll<AlignmentMaterialsSO>("SO's/Alignment Materials").ToList();
                    AlignmentMaterialsSO alignmentMaterial = amSOs.Where(am => am.alignment.alignment == pAlignment).FirstOrDefault();

                    mrs.ForEach(mr => mr.material = alignmentMaterial.ghost_structures);
                }

                Transform animationOrigin = pInvoker.GetAnimationOriginOfAttack(pAttack).animationOrigin;

                Animating.InvokeAnimation(pAttack.finalAnimation, pPosition, animationOrigin.rotation, duration);
                AudioPlaying.InvokeSound(pAttack.finalSound, pPosition);
            }
        );

        newOutcomeAttack.GetComponent<OutcomeAttackController>().StartOutcome();
    }

}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////