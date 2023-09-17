using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using TheraBytes.BetterUi;

[Serializable]
public struct HorizontalLayoutElement
{
    public int id;
    public GameObject element;
}

[HideMonoScript]
public class HorizontalLayoutController : WorldspaceInterfaceObjectController
{
    // Public (Variables) [START]
    [Min(0)]
    public float spacingSize = 0.5f;
    [Min(1f)]
    public float elementSize = 2f;
    [Required]
    [AssetsOnly]
    public GameObject horizontalLayoutElement;
    // Public (Variables) [END]

    // Protected (Variables) [START]
    protected List<HorizontalLayoutElement> spawnedElements;
    // Protected (Variables) [END]

    // (Unity) Methods [START]
    protected override void Start()
    {
        base.Start();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        InitializeVariables();
    }

    protected override void Update()
    {
        base.Update();
    }
    // (Unity) Methods [END]


    // Private (Methods) [START]
    private void InitializeVariables()
    {
        InitializeSpawnedElementsList();
    }
    private void InitializeSpawnedElementsList()
    {
        if (spawnedElements == null)
            spawnedElements = new List<HorizontalLayoutElement>();
        else
        {
            spawnedElements.ForEach(element =>  Destroy(element.element));

            spawnedElements.Clear();
        }
    }
    // Private (Methods) [END]

    // Protected (Methods) [START]
    protected bool IsElementAlreadyRegistered(int pId) => spawnedElements != null && spawnedElements.Any(se => se.id == pId);
    protected void RefreshLayoutSize()
    {
        GetComponent<BetterAxisAlignedLayoutGroup>().spacing = spacingSize;

        Rect rect = GetComponent<RectTransform>().rect;

        rect.Set(
            rect.x,
            rect.y,
            (elementSize * spawnedElements.Count) + (spacingSize * (spawnedElements.Count - 1)),
            rect.height
        );
    }
    protected void RecalculateLayoutElementSizeValues(GameObject element)
    {
        element.GetComponent<BetterLayoutElement>().minWidth = elementSize;
        element.GetComponent<BetterLayoutElement>().MinWidthSizer.OptimizedSize = elementSize;
        element.GetComponent<BetterLayoutElement>().preferredWidth = elementSize;
        element.GetComponent<BetterLayoutElement>().PreferredWidthSizer.OptimizedSize = elementSize;

        element.GetComponent<BetterLayoutElement>().MinWidthSizer.OverrideLastCalculatedSize(elementSize);
        element.GetComponent<BetterLayoutElement>().PreferredWidthSizer.OverrideLastCalculatedSize(elementSize);
    }
    // Protected (Methods) [END]


    // Public (Methods) [START]
    public void AddElement(int pId)
    {
        if (IsElementAlreadyRegistered(pId))
            return;

        GameObject newElement = Instantiate(horizontalLayoutElement);

        newElement.transform.SetParent(transform);

        RecalculateLayoutElementSizeValues(newElement);
        RefreshLayoutSize();

        HorizontalLayoutElement hle = new()
        {
            id = pId,
            element = newElement
        };

        spawnedElements.Add(hle);

        newElement.SetActive(false);
    }

    public void RemoveElement(int pId)
    {
        if (!IsElementAlreadyRegistered(pId))
            return;

        HorizontalLayoutElement hle = spawnedElements.Find(se => se.id == pId);

        Destroy(hle.element);

        spawnedElements.Remove(hle);
    }
    // Public (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////