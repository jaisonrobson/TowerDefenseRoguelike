using System.Windows;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Hierarchy Window Group Header
/// http://diegogiacomelli.com.br/unitytips-hierarchy-window-group-header
/// </summary>
[InitializeOnLoad]
public static class HierarchyWindowGroupHeader
{
    static int Traverse(GameObject obj, int counter)
    {
        if (obj.transform.parent != null)
        {
            return Traverse(obj.transform.parent.gameObject, counter+1);
        }

        return counter;
    }

    static Color GetColorFromIndex(int index)
    {
        Random.InitState(index);
        return Random.ColorHSV();
    }

    static HierarchyWindowGroupHeader()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;
    }

    static void HierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        var gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

        if (gameObject != null && gameObject.name.StartsWith("---", System.StringComparison.Ordinal))
        {
            int indentLevel = Traverse(gameObject, 0);

            EditorGUI.DrawRect(selectionRect, GetColorFromIndex(indentLevel));
            EditorGUI.DropShadowLabel(selectionRect, gameObject.name.Replace("-", ""));
        }
    }
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////