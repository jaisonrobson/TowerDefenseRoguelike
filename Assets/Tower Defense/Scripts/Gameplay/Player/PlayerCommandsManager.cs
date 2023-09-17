using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Patterns;
using Sirenix.OdinInspector;

[RequireComponent(typeof(PlayerCommandsController))]
[HideMonoScript]
public class PlayerCommandsManager : Singleton<PlayerCommandsManager>
{

    // Private (Variables) [START]
    [ShowInInspector]
    [HideInEditorMode]
    [ReadOnly]
    private PlayerCommandEnum command;
    private float inputDelayTimer = 0;
    // Private (Variables) [END]

    // Public (Properties) [START]
    public PlayerCommandEnum Command { get { return command; } set { inputDelayTimer = 0.02f; command = value; } }
    // Public (Properties) [END]


    // (Unity) Methods [START]
    private void LateUpdate()
    {
        HandleInputDelay();
    }
    // (Unity) Methods [END]

    // Public (Methods) [START]
    public bool IsIdle => command == PlayerCommandEnum.IDLE && !HasInputDelay;
    public bool IsTryingToCast => command == PlayerCommandEnum.CASTING;
    public bool IsCasting => command == PlayerCommandEnum.CASTING && !HasInputDelay;
    public bool HasInputDelay => inputDelayTimer > 0f;
    // Public (Methods) [END]

    // Private (Methods) [START]
    private void HandleInputDelay()
    {
        inputDelayTimer -= Time.deltaTime;
    }
    // Private (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////