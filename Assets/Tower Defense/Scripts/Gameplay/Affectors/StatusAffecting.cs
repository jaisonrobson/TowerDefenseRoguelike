using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Core.Patterns;
using Core.Math;

public static class StatusAffecting
{
    public static void TryInvokeStatus(ProbabilityStatusAffectionSO probabilityStatusAffection, Agent invoker, Agent target)
    {
        if (probabilityStatusAffection == null || invoker == null || target == null)
            return;

        if (target.IsStructure)
            return;

        if (target.IsStatusAlreadyAffectingAgent(probabilityStatusAffection.statusAffector))
            return;

        if (RNG.Int(0, 100) > probabilityStatusAffection.probability)
            return;

        if (target.IsAgentImmuneToStatus(probabilityStatusAffection.statusAffector.status.status))
            return;

        switch (probabilityStatusAffection.statusAffector.status.status)
        {
            case StatusEnum.FREEZE:
            case StatusEnum.BURN:
            case StatusEnum.PARALYZE:
            case StatusEnum.DROWN:
            case StatusEnum.CONFUSION:
            case StatusEnum.POISON:
            case StatusEnum.ASLEEP:
            case StatusEnum.GROUNDED:
            case StatusEnum.HEALBLOCK:
            case StatusEnum.TAUNT:
                Poolable.TryGetPoolable(
                    probabilityStatusAffection.statusAffector.prefab,
                    (Poolable pNewStatusAffectorPoolable) =>
                    {
                        pNewStatusAffectorPoolable.GetComponent<StatusAffector>().Invoker = invoker;
                        pNewStatusAffectorPoolable.GetComponent<StatusAffector>().Target = target;
                        pNewStatusAffectorPoolable.GetComponent<StatusAffector>().Alignment = invoker.Alignment;
                    }
                );
                break;
            default:
                break;
        }
    }
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////