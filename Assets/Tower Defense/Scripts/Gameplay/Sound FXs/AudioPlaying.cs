using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using Core.Patterns;

public static class AudioPlaying
{
    public static void InvokeSound(SoundSO pSoundSO, GameObject pObjectToFollow)
    {
        if (pSoundSO == null || pObjectToFollow == null)
            return;

        GameObject newAudioPoolable = Poolable.TryGetPoolable(pSoundSO.prefab);

        newAudioPoolable.GetComponent<SoundFX>().StartExecution(pObjectToFollow);
    }

    public static void InvokeSound(SoundSO pSoundSO, Vector3 pPosition)
    {
        if (pSoundSO == null)
            return;

        GameObject newAudioPoolable = Poolable.TryGetPoolable(pSoundSO.prefab);

        newAudioPoolable.GetComponent<SoundFX>().StartExecution(pPosition);
    }
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////