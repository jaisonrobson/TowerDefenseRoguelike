using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using Core.Patterns;
using FMODUnity;

[RequireComponent(typeof(StudioEventEmitter))]
[RequireComponent(typeof(Poolable))]
[HideMonoScript]
public class SoundFX : MonoBehaviour, IPoolable
{
    // Private (Variables) [START]
    private StudioEventEmitter fmodSoundEvent;
    private bool isRunning;
    // Private (Variables) [END]

    // Public (Properties) [START]
    public GameObject ObjectToFollow { get; set; }
    // Public (Properties) [END]

    // (Unity) Methods [START]
    protected virtual void Start()
    {
    }
    protected virtual void Update()
    {
        if (isRunning)
        {
            HandleFinishing();
            HandleObjectFollowing();
        }
    }
    // (Unity) Methods [END]

    // Private (Methods) [START]
    private void ResetVariables()
    {
        isRunning = false;
        ObjectToFollow = null;

        fmodSoundEvent = GetComponent<StudioEventEmitter>();
    }
    private void HandleFinishing()
    {
        bool isOneshotSound;
        fmodSoundEvent.EventDescription.isOneshot(out isOneshotSound);

        if (isRunning && !fmodSoundEvent.IsPlaying())
            Poolable.TryPool(gameObject);

        if (isRunning && !isOneshotSound)
        {
            if (ObjectToFollow == null)
                Poolable.TryPool(gameObject);
            else if (ObjectToFollow != null && !ObjectToFollow.activeInHierarchy)
                Poolable.TryPool(gameObject);
        }
    }
    private void HandleObjectFollowing()
    {
        if (ObjectToFollow != null)
        {
            if (ObjectToFollow.activeInHierarchy)
                transform.position = ObjectToFollow.transform.position;
        }
    }
    // Private (Methods) [END]

    // Public (Methods) [START]
    public void StartExecution()
    {
        fmodSoundEvent.Play();

        isRunning = true;
    }
    public void StartExecution(GameObject pObjectToFollow = null)
    {
        ResetVariables();

        ObjectToFollow = pObjectToFollow;

        StartExecution();
    }
    public void StartExecution(Vector3 pPosition)
    {
        ResetVariables();

        transform.position = pPosition;

        StartExecution();
    }
    public virtual void PoolRetrievalAction(Poolable poolable)
    {
    }
    public virtual void PoolInsertionAction(Poolable poolable)
    {
        ResetVariables();

        fmodSoundEvent.Stop();
    }
    // Public (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////