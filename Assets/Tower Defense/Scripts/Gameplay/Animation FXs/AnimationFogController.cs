using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

[HideMonoScript]
public class AnimationFogController : MonoBehaviour
{
    // Public (Variables) [START]
    public float deathFogAnimationDuration = 5f;
    // Public (Variables) [END]

    // Private (Variables) [START]
    private bool isEnding = false;
    private float timeUntilEnd = 0f;
    // Private (Variables) [END]

    // (Unity) Methods [START]
    void OnEnable()
    {
        InitializeVariables();
    }

    void Update()
    {
        HandleFogEnding();
    }
    // (Unity) Methods [END]

    // Private (Methods) [START]
    private void InitializeVariables()
    {
        isEnding = false;
        timeUntilEnd = 0f;

        List<ParticleSystem> pSs = gameObject.GetComponentsInChildren<ParticleSystem>(true).ToList();

        pSs.ForEach(ps =>
        {
            var main = ps.main;

            main.loop = true;
            main.prewarm = true;
        });

        gameObject.GetComponent<ParticleSystem>().Play(true);
    }
    private void HandleFogEnding()
    {
        if (isEnding && Time.time >= timeUntilEnd)
        {
            gameObject.SetActive(false);
        }
    }
    // Private (Methods) [END]

    // Public (Methods) [START]
    public void EndFog()
    {
        isEnding = true;
        timeUntilEnd = Time.time + deathFogAnimationDuration + 1f;

        List<ParticleSystem> pSs = gameObject.GetComponentsInChildren<ParticleSystem>(true).ToList();

        pSs.ForEach(ps =>
        {
            var main = ps.main;

            main.prewarm = false;
            main.loop = false;
        });
    }
    // Public (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////