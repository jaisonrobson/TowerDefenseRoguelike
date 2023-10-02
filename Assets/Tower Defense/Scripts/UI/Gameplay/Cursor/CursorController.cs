using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
using Core.Patterns;
using Core.Physics;

[RequireComponent(typeof(CursorManager))]
[HideMonoScript]
public class CursorController : Singleton<CursorController>
{
    // Private (Variables) [START]
    private LayerMask forbiddenLayers;
    [ShowInInspector]
    [HideInEditorMode]
    [ReadOnly]
    private CursorTypeEnum selectedCursor = CursorTypeEnum.HAND_01;
    [ShowInInspector]
    [HideInEditorMode]
    [ReadOnly]
    private int currentFrame;
    [ShowInInspector]
    [HideInEditorMode]
    [ReadOnly]
    private float frameTimer;
    // Private (Variables) [END]

    // (Unity) Methods [START]
    private void Start()
    {
        InitializeVariables();
    }
    private void Update()
    {
        FilterCursorChange();
        HandleCursorUpdate();
    }
    // (Unity) Methods [END]


    // Public (Methods) [START]
    public void ResetCursorVariables()
    {
        currentFrame = 0;
        frameTimer = 0f;
    }
    public void ResetCursorModeAndCommand()
    {
        PlayerCommandsManager.instance.Command = PlayerCommandEnum.IDLE;
        CursorManager.instance.Mode = CursorModeEnum.IDLE;
    }
    // Public (Methods) [END]

    // Private (Methods) [START]
    private void ResetVariables()
    {
        forbiddenLayers = LayerMask.GetMask("Default", "Water", "UI", "Structure", "Obstacle");
    }
    private void FilterCursorChange()
    {
        bool didFilter = false;

        if (PlayerCommandsManager.instance.IsCasting)
        {
            switch (CursorManager.instance.Mode)
            {
                case CursorModeEnum.CASTING_AIM:
                    didFilter = true;

                    HandleCursorAimMode();
                    break;
                case CursorModeEnum.CASTING_CONSTRUCTION:
                    didFilter = true;

                    HandleCursorConstructionMode();
                    break;
            }
        }

        if (!didFilter)
            HandleCursorIdleMode();
    }
    private void HandleCursorAimMode()
    {
        if (Input.GetMouseButton(0))
            ChangeSelectedCursor(CursorTypeEnum.AIM_02);
        else
            ChangeSelectedCursor(CursorTypeEnum.AIM_01);

        if (Input.GetMouseButtonUp(0) && !OverlayInterfaceManager.instance.IsOverUI())
        {
            PlayerCommandsManager.instance.Command = PlayerCommandEnum.IDLE;
            CursorManager.instance.Mode = CursorModeEnum.IDLE;
            CursorManager.instance.aimCasting.Occurred(SelectionManager.instance.SelectedAgents.FirstOrDefault().gameObject);
        }
    }
    private void HandleCursorConstructionMode()
    {
        if (Input.GetMouseButton(0))
            ChangeSelectedCursor(CursorTypeEnum.HAMMER_02);
        else
            ChangeSelectedCursor(CursorTypeEnum.HAMMER_01);
    }
    private void HandleCursorIdleMode()
    {
        if (Input.GetMouseButton(0))
            ChangeSelectedCursor(CursorTypeEnum.HAND_02);
        else
            ChangeSelectedCursor(CursorTypeEnum.HAND_01);
    }
    private void HandleCursorUpdate()
    {
        CursorSO cursor = CursorManager.instance.GetCursorByType(selectedCursor);

        if (cursor.isAnimated)
        {
            frameTimer -= Time.deltaTime;

            if (frameTimer <= 0f)
            {
                frameTimer += cursor.animationVelocity;
                currentFrame = (currentFrame + 1) % cursor.textures.Count;
            }
        }

        Cursor.SetCursor(cursor.textures[currentFrame], cursor.offsets[currentFrame], CursorMode.Auto);
    }
    private void ChangeSelectedCursor(CursorTypeEnum newCursor)
    {
        if (selectedCursor == newCursor)
            return;

        selectedCursor = newCursor;
        ResetCursorVariables();        
    }
    private void InitializeVariables()
    {
        ResetCursorVariables();
        ResetVariables();
    }
    private bool DidHitForbiddenArea()
    {
        RaycastHit forbiddenHit = Raycasting.ScreenPointToRay(forbiddenLayers);

        if (!Raycasting.IsHitEmpty(forbiddenHit))
            return true;

        return false;
    }
    // Private (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////