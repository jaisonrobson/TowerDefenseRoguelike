using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Patterns;
using Sirenix.OdinInspector;
using System.Linq;

[RequireComponent(typeof(SelectionController))]
[HideMonoScript]
public class SelectionManager : Singleton<SelectionManager>
{
    // Public (Variables) [START]
    public LayerMask selectionLayers;
    // Public (Variables) [END]

    // Private (Variables) [START]
    [ShowInInspector]
    [HideInEditorMode]
    [ReadOnly]
    private List<Selectable> selectedAgents;
    // Private (Variables) [END]

    // Public (Properties) [START]
    public List<Selectable> SelectedAgents { get { return new List<Selectable>(selectedAgents); } }
    public bool IsAnythingSelected { get { return selectedAgents.Count > 0; } }
    // Public (Properties) [END]

    // (Unity) Methods [START]
    private void Start()
    {
        InitializeVariables();
    }
    // (Unity) Methods [END]

    // Public (Methods) [START]
    public void AddSelectables(List<Selectable> newSelection)
    {
        if (!selectedAgents.Any(sa => newSelection.Any(ns => ns == sa)) && FilterSelection(newSelection))
            selectedAgents = selectedAgents.Concat(newSelection).ToList();
    }
    public void AddSelectables(Selectable newSelection)
    {
        List<Selectable> ns = new List<Selectable>();

        ns.Add(newSelection);

        AddSelectables(ns);
    }
    public void RemoveSelectable(Selectable sl)
    {
        if (selectedAgents.Contains(sl))
            selectedAgents.Remove(sl);
    }
    public void ClearSelectables()
    {
        selectedAgents.Clear();
    }
    public bool FilterSelection(List<Selectable> newSelection)
    {
        bool condition = newSelection.Count == 1;

        if (condition)
            ClearSelectables();

        return condition;
    }
    // Public (Methods) [END]

    // Private (Methods) [START]
    private void InitializeVariables()
    {
        selectedAgents = new List<Selectable>();
    }
    // Private (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////