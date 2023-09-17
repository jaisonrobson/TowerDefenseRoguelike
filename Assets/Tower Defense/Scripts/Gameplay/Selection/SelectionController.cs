using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Core.Patterns;
using Core.Physics;

[RequireComponent(typeof(SelectionManager))]
[HideMonoScript]
public class SelectionController : Singleton<SelectionController>
{
    // (Unity) Methods [START]
    private void Update()
    {
        if (!PlayerManager.instance.IsLockingPlayerControl)
        {
            HandleInput();
        }
    }
    // (Unity) Methods [END]

    // Private (Methods) [START]
    private void HandleInput()
    {
        HandleDeselection();
        HandleSelection();
    }
    private void HandleDeselection()
    {
        if (Input.GetKey(KeyCode.Escape))
            SelectionManager.instance.ClearSelectables();
    }
    private void HandleSelection()
    {
        if (OverlayInterfaceManager.instance.IsOverUI() || PlayerCommandsManager.instance.IsTryingToCast || PlayerCommandsManager.instance.HasInputDelay)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit = Raycasting.ScreenPointToRay(SelectionManager.instance.selectionLayers);

            if (!Raycasting.IsHitEmpty(hit))
                SelectionManager.instance.AddSelectables(hit.collider.gameObject.GetComponentInParent<Selectable>());
            else
                SelectionManager.instance.ClearSelectables();
        }
    }
    // Private (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////