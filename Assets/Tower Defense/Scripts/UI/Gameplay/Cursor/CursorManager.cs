using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Core.Patterns;
using System.Linq;

[RequireComponent(typeof(CursorController))]
[HideMonoScript]
public class CursorManager : Singleton<CursorManager>
{
    // Public (Variables) [START]
    [Required]
    public ObservableEvent flagPositioning;
    [Required]
    public ObservableEvent aimCasting;
    // Public (Variables) [END]

    // Private (Variables) [START]
    [ShowInInspector]
    [HideInEditorMode]
    [ReadOnly]
    private CursorModeEnum mode = CursorModeEnum.IDLE;
    [ShowInInspector]
    [HideInEditorMode]
    [ReadOnly]
    private List<CursorSO> cursors;
    // Private (Variables) [END]

    // Public (Properties) [START]
    public CursorModeEnum Mode { get { return mode; } set { mode = value; } }
    public List<CursorSO> Cursors { get { return cursors; } }
    // Public (Properties) [END]

    // (Unity) Methods [START]
    private void Start()
    {
        InitializeVariables();
    }
    // (Unity) Methods [END]

    // Public (Methods) [START]
    public CursorSO GetCursorByType(CursorTypeEnum type) => cursors.Where(c => c.type == type).First();
    // Public (Methods) [END]

    // Private (Methods) [START]
    private void InitializeVariables()
    {
        InitializeCursors();
    }
    private void InitializeCursors()
    {
        cursors = Resources.LoadAll<CursorSO>("SO's/Cursors").ToList();
    }
    // Private (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////