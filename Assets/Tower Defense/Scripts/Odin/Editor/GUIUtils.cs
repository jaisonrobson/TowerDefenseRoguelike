using Sirenix.Utilities;
using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine;
using Sirenix.Utilities.Editor;
using System.Linq;
using Core.General;

public static class GUIUtils
{
    public static bool SelectButtonList(ref Type selectedType, Type[] typesToDisplay)
    {
        bool result = false;
        IEnumerable<IEnumerable<Type>> newTypesToDisplay = Utils.Chunk(typesToDisplay, 5);

        foreach (IEnumerable<Type> types in newTypesToDisplay)
        {
            Type[] newTypes = types.ToArray();
            var rect = GUILayoutUtility.GetRect(0, 30);

            for (int i = 0; i < newTypes.Length; i++)
            {
                var name = newTypes[i].Name.Replace("SO", "");
                var btnRect = rect.Split(i, newTypes.Length);

                if (GUIUtils.SelectButton(btnRect, name, newTypes[i] == selectedType))
                {
                    selectedType = newTypes[i];
                    result = true;
                }
            }
        }

        return result;
    }

    public static bool SelectButton(Rect rect, string name, bool selected)
    {
        if (GUI.Button(rect, GUIContent.none, GUIStyle.none))
            return true;

        if (Event.current.type == EventType.Repaint)
        {
            var style = new GUIStyle(EditorStyles.miniButtonMid);
            style.stretchHeight = true;
            style.fixedHeight = rect.height;
            style.Draw(rect, GUIHelper.TempContent(name), false, false, selected, false);
        }

        return false;
    }

}


////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////