using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Patterns;
using Sirenix.OdinInspector;

[RequireComponent(typeof(PlayerCommandsManager))]
[HideMonoScript]
public class PlayerCommandsController : Singleton<PlayerCommandsController>
{
    // (Unity) Methods [START]
    private void Update()
    {
        HandlePlayerInputCommands();
    }
    // (Unity) Methods [END]


    // Private (Methods) [START]
    private void HandlePlayerInputCommands()
    {
        HandlePlayerCasting();
    }
    private void HandlePlayerCasting()
    {
        if (PlayerCommandsManager.instance.IsTryingToCast)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                PlayerCommandsManager.instance.Command = PlayerCommandEnum.IDLE;
            }
        }
    }
    // Private (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////