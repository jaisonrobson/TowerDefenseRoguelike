using UnityEngine;
#if UNITY_EDITOR
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using System;
using Core.Math;
public class ProgressBarWithFieldsAttributeDrawer<T> : OdinAttributeDrawer<ProgressBarWithFieldsAttribute, T> where T : struct
{
    private T internalValue = default;

    // (ODIN) Methods [START]
    protected override void DrawPropertyLayout(GUIContent label)
    {
        internalValue = ValueEntry.SmartValue;

        DrawProgressBarWithFields(label);

        ValueEntry.SmartValue = internalValue;

        ValueEntry.Property.MarkSerializationRootDirty();
    }

    protected override bool CanDrawAttributeValueProperty(InspectorProperty property)
    {
        if (typeof(T) == typeof(float) || typeof(T) == typeof(int))
            return true;

        return false;
    }
    // (ODIN) Methods [END]

    private void DrawProgressBarWithFields(GUIContent label)
    {
        GUILayout.Space(1f);

        SirenixEditorGUI.BeginHorizontalPropertyLayout(label);

        Rect progressBarRect = GUILayoutUtility.GetRect(50f, 20f, GUILayout.ExpandWidth(true));

        ProgressBarConfig pbc = ProgressBarConfig.Default;
        pbc.DrawValueLabel = true;
        pbc.ForegroundColor = new Color(Attribute.r, Attribute.g, Attribute.b, 1f);

        internalValue = ConvertionTools.ConvertFromDouble<T>(SirenixEditorFields.ProgressBarField(progressBarRect, ConvertionTools.ConvertToDouble(internalValue), Attribute.min, Attribute.max, pbc));

        GUILayout.Space(5f);

        Rect fieldRect = GUILayoutUtility.GetRect(60f, 20f, GUILayout.ExpandWidth(false));

        internalValue = ConvertionTools.ConvertFromFloat<T>(SirenixEditorFields.FloatField(fieldRect, ConvertionTools.ConvertToFloat(internalValue)));

        internalValue = ConvertionTools.ConvertFromFloat<T>(Mathf.Clamp(ConvertionTools.ConvertToFloat(internalValue), Attribute.min, Attribute.max));

        SirenixEditorGUI.EndHorizontalPropertyLayout();

        GUILayout.Space(1f);
    }
}

#endif

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////