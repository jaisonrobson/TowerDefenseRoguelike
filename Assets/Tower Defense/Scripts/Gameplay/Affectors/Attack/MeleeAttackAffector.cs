using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using Core.Patterns;

[RequireComponent(typeof(MeleeAttackController))]
[HideMonoScript]
public class MeleeAttackAffector : AttackAffector
{
    // Public (Properties) [START]
    public Vector3 Origin { get; set; }
    public Quaternion InitialRotation { get; set; }
    // Public (Properties) [END]

    // Public (Methods) [START]
    public override void PoolRetrievalAction(Poolable poolable)
    {
        base.PoolRetrievalAction(poolable);

        GetComponent<MeleeAttackController>().PoolRetrievalAction(poolable);
    }
    public override void PoolInsertionAction(Poolable poolable)
    {
        base.PoolInsertionAction(poolable);

        Origin = Vector3.zero;
        InitialRotation = Quaternion.identity;

        GetComponent<MeleeAttackController>().PoolInsertionAction(poolable);
    }
    // Public (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////