using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using Core.Patterns;

[HideMonoScript]
public abstract class AttackAffector : Affector
{
    // Public (Properties) [START]
    [ReadOnly]
    [ShowInInspector]
    public AttackSO Attack { get; set; }
    [ReadOnly]
    [ShowInInspector]
    public float Duration { get; set; }
    [ReadOnly]
    [ShowInInspector]
    public float Damage { get; set; }
    // Public (Properties) [END]

    // Public (Methods) [START]
    public override void PoolRetrievalAction(Poolable poolable)
    {
        base.PoolRetrievalAction(poolable);
    }
    public override void PoolInsertionAction(Poolable poolable)
    {
        base.PoolInsertionAction(poolable);

        Attack = null;
        Damage = 0f;
        Duration = 0f;
    }
    // Public (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////