using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;

#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using Core.General;
#endif

[ManageableData]
public class CursorSO : BaseOptionDataSO
{
    // Public (Variables) [START]
    [ValidateInput("Validate_Unique_Type", "[CursorSO] type already exists in another cursor")]
    public CursorTypeEnum type;
    [PropertySpace(10f)]
    [ListDrawerSettings(Expanded = true)]
    [Tooltip("The texture of the cursor, if it is animated it will have more than one texture.")]
    [ValidateInput("Validate_NotEmpty_Textures", "[CursorSO] must have at least one texture")]
    [ValidateInput("Validate_NotIsAnimated_Count_Textures", "[CursorSO] non animated cursors must have only one texture")]
    [ValidateInput("Validate_NotNull_Textures_Items", "[CursorSO] all texture items must be filled with a valid texture")]
    [OnCollectionChanged("OnCollectionChange_Textures")]
    public List<Texture2D> textures;
    [PropertySpace(0, 10f)]
    [ListDrawerSettings(Expanded = true)]
    [Tooltip("The offset position of the cursor of each texture listed before.")]
    [ValidateInput("Validate_NotEmpty_Offsets", "[CursorSO] must have at least one offset")]
    [ValidateInput("Validate_NotIsAnimated_Count_Offsets", "[CursorSO] non animated cursors must have only one offset")]
    [OnCollectionChanged("OnCollectionChange_Offsets")]
    public List<Vector2> offsets;
    [ToggleButtons("ANIMATED", "STATIC", trueColor: "@new Color(0.51f, 1f, 0.65f, 1f)", falseColor: "@new Color(1f, 0.56f, 0.51f, 1f)")]
    public bool isAnimated = false;
    [Range(0.01f, 1f)]
    public float animationVelocity = 0.01f;
    // Public (Variables) [END]


    // (Inspector Helper) Methods [START]
#if UNITY_EDITOR
    private void OnCollectionChange_Textures(CollectionChangeInfo info, object value)
    {
        if (info.ChangeType == CollectionChangeType.Add || info.ChangeType == CollectionChangeType.Insert)
        {
            if (offsets.Count < textures.Count)
                offsets.Add(Vector2.zero);
        }

        if (info.ChangeType == CollectionChangeType.RemoveIndex)
        {
            if (offsets.Count > textures.Count)
                offsets.RemoveAt(offsets.IndexOf(offsets.Last()));
        }
    }
    private void OnCollectionChange_Offsets(CollectionChangeInfo info, object value)
    {
        if (info.ChangeType == CollectionChangeType.Add || info.ChangeType == CollectionChangeType.Insert)
        {
            if (textures.Count < offsets.Count)
                textures.Add(null);
        }

        if (info.ChangeType == CollectionChangeType.RemoveIndex)
        {
            if (textures.Count > offsets.Count)
                textures.RemoveAt(textures.IndexOf(textures.Last()));
        }
    }
#endif
    // (Inspector Helper) Methods [END]

    // (Validation) Methods [START]
#if UNITY_EDITOR
    private bool Validate_Unique_Type()
    {
        List<CursorSO> existingCursors = Utils.FindAssetsByType<CursorSO>();

        return !existingCursors.Any(c => c != this && c.type == type);
    }
#endif
    private bool Validate_NotIsAnimated_Count_Textures() { return !isAnimated && textures?.Count == 1 || isAnimated; }
    private bool Validate_NotIsAnimated_Count_Offsets() { return !isAnimated && offsets?.Count == 1 || isAnimated; }
    private bool Validate_NotNull_Textures_Items() { return textures != null && textures.All(t => t != null); }
    private bool Validate_NotEmpty_Textures() { return textures != null && textures.Count > 0; }
    private bool Validate_NotEmpty_Offsets() { return offsets != null && offsets.Count > 0; }
    // (Validation) Methods [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////