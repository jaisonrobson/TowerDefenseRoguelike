
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Reflection;

public class BaseOptionDataSO : SerializedScriptableObject
{
    #if UNITY_EDITOR
    [FieldlessHeader("Properties")]
    #endif
    public string jokerHiddenTitleField;

    [HideInInspector]
    public string assetPath = "";

    [HideInInspector]
    public string newName = "";

    public void SetAssetName()
    {
        string[] assetPathing = assetPath.Split("/");
        string assetName = assetPathing[assetPathing.Length - 1].Replace(".asset", "");

        #if UNITY_EDITOR
        AssetDatabase.SaveAssetIfDirty(AssetDatabase.GUIDFromAssetPath(AssetDatabase.GetAssetPath(this)));

        if (!assetName.Equals(newName))
                AssetDatabase.RenameAsset(assetPath, newName);
        #endif
    }
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////