using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

[Searchable]
public class DataManagerEditorWindow : OdinMenuEditorWindow
{
    public static string DEFAULTSOASSETSPATH = "Assets/Tower Defense/Resources/SO's";

    private static Type[] typesToDisplay = TypeCache.GetTypesWithAttribute<ManageableDataAttribute>().OrderBy(m => m.Name).ToArray();

    private Type selectedType = typesToDisplay[0];

    protected override void OnGUI()
    {
        //Custom GUI drawing
        //Draws the SO Organization Buttons
        if (GUIUtils.SelectButtonList(ref selectedType, typesToDisplay))
            this.ForceMenuTreeRebuild(); //If a button is pressed force the Odin tree interface to rebuild

        //Default GUI drawing
        base.OnGUI();
    }

    [MenuItem("Tools/Data Manager")]
    private static void OpenEditor() => GetWindow<DataManagerEditorWindow>();

    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree();
        tree.Config.DrawSearchToolbar = true;

        tree.AddAllAssetsAtPath("", DataManagerEditorWindow.DEFAULTSOASSETSPATH + "/", selectedType, true, true);

        tree.SortMenuItemsByName(false);

        return tree;
    }

    protected override void OnBeginDrawEditors()
    {
        if (this.MenuTree == null)
            return;

        var selected = this.MenuTree.Selection.FirstOrDefault();
        var toolbarHeight = this.MenuTree.Config.SearchToolbarHeight;

        // Draws a toolbar with the name of the currently selected menu item.
        SirenixEditorGUI.BeginHorizontalToolbar(toolbarHeight);
        {
            if (selected != null)
            {
                GUILayout.Label(selected.Name);
            }

            if (SirenixEditorGUI.ToolbarButton(new GUIContent("Create Item")))
            {
                ScriptableObjectCreator.ShowDialog(typesToDisplay, DataManagerEditorWindow.DEFAULTSOASSETSPATH, obj =>
                {
                    base.TrySelectMenuItemWithObject(obj); // Selects the newly created item in the editor
                });
            }
        }
        SirenixEditorGUI.EndHorizontalToolbar();
    }
}


////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////