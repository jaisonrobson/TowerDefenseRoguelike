using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using Core.Patterns;

[RequireComponent(typeof(RangedAttackController))]
[HideMonoScript]
public class RangedAttackAffector : AttackAffector
{
    // Public (Variables) [START]
    public AttackTravellingTypeEnum travellingType = AttackTravellingTypeEnum.LINEAR;
    // Public (Variables) [END]

    // Public (Properties) [START]
    public Vector3 Origin { get; set; }
    public Vector3 Destination { get; set; }
    public float Speed { get; set; }
    // Public (Properties) [END]

    // Public (Methods) [START]
    public override void PoolRetrievalAction(Poolable poolable)
    {
        base.PoolRetrievalAction(poolable);

        GetComponent<RangedAttackController>().PoolRetrievalAction(poolable);
    }
    public override void PoolInsertionAction(Poolable poolable)
    {
        base.PoolInsertionAction(poolable);

        Origin = Vector3.zero;
        Destination = Vector3.zero;
        Speed = 0f;

        GetComponent<RangedAttackController>().PoolInsertionAction(poolable);
    }
    // Public (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////