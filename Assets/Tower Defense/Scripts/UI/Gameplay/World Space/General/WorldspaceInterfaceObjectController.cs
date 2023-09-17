using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

[HideMonoScript]
public class WorldspaceInterfaceObjectController : MonoBehaviour
{
    [ShowInInspector]
    [HideInEditorMode]
    [ReadOnly]
    protected Transform targetToFollow;
    [ShowInInspector]
    [HideInEditorMode]
    [ReadOnly]
    protected float targetHeightOffset = 0f;

    // (Unity) Methods [START]
    protected virtual void Start() { }

    protected virtual void OnEnable() { }

    protected virtual void Update()
    {
        transform.position = new Vector3(targetToFollow.position.x, targetToFollow.position.y + targetHeightOffset, targetToFollow.position.z);
    }
    // (Unity) Methods [END]


    // Public (Methods) [START]
    public void SetTarget(Transform target) { targetToFollow = target; }
    public void SetTargetHeightOffset(float offset) { targetHeightOffset = offset + 1f; }
    public void Hide() => gameObject.SetActive(false);
    public void Show() => gameObject.SetActive(true);
    // Public (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////