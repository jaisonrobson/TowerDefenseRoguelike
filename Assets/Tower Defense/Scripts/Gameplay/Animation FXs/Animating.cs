using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Core.Patterns;

public static class Animating
{
    public static void InvokeAnimation(AnimationSO pAnimSO, Vector3 pPosition, Quaternion pRotation, float pDuration = 1f)
    {
        if (pAnimSO == null)
            return;

        GameObject newAnimationPoolable = Poolable.TryGetPoolable(
            pAnimSO.prefab,
            (newPoolable) => {
                newPoolable.transform.position = pPosition;
                newPoolable.transform.rotation = pRotation;
            }
        );

        newAnimationPoolable.GetComponent<AnimationFX>().StartAnimation(pAnimSO, pDuration);
    }

    public static void InvokeTrailAnimation(AnimationSO pAnimSO, GameObject pObjectToFollow, float pDuration = 1f)
    {
        if (pAnimSO == null)
            return;

        GameObject newAnimationPoolable = Poolable.TryGetPoolable(
            pAnimSO.prefab,
            newPoolable =>
            {
                newPoolable.transform.position = pObjectToFollow.transform.position;
                newPoolable.transform.rotation = pObjectToFollow.transform.rotation;
            }
        );

        newAnimationPoolable.GetComponent<AnimationFX>().StartAnimation(pAnimSO, pDuration, pObjectToFollow, true);
    }
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////