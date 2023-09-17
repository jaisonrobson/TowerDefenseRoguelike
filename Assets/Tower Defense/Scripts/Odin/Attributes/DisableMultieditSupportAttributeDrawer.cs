using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using Sirenix.OdinInspector.Editor;

public class DisableMultieditSupportAttributeDrawer : Sirenix.OdinInspector.Editor.OdinAttributeDrawer<DisableMultieditSupportAttribute>
{
    protected override void DrawPropertyLayout(GUIContent label)
    {
        if (this.Property.Tree.UnitySerializedObject.isEditingMultipleObjects)
        {
            SirenixEditorGUI.ErrorMessageBox("No multiedit support for this attribute.");
            return;
        }

        CallNextDrawer(label);
    }
}
#endif

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////