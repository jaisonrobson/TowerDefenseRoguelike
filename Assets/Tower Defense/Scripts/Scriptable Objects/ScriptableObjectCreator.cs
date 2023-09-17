#if UNITY_EDITOR

    using Sirenix.OdinInspector.Editor;
    using Sirenix.Utilities;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using UnityEditor;
    using UnityEngine;

    // 
    // With our custom RPG Editor window, this ScriptableObjectCreator is a replacement for the [CreateAssetMenu] attribute Unity provides.
    // 
    // For instance, if we call ScriptableObjectCreator.ShowDialog<Item>(..., ...), it will automatically find all 
    // ScriptableObjects in your project that inherits from Item and prompts the user with a popup where he 
    // can choose the type of item he wants to create. We then also provide the ShowDialog with a default path,
    // to help the user create it in a specific directory.
    // 

    public static class ScriptableObjectCreator
    {
        public static void ShowDialog(System.Type[] types, string defaultDestinationPath, Action<UnityEngine.ScriptableObject> onScritpableObjectCreated = null)
        {
            var selector = new ScriptableObjectSelector(types, defaultDestinationPath, onScritpableObjectCreated);

            if (selector.SelectionTree.EnumerateTree().Count() == 1)
            {
                // If there is only one scriptable object to choose from in the selector, then 
                // we'll automatically select it and confirm the selection. 
                selector.SelectionTree.EnumerateTree().First().Select();
                selector.SelectionTree.Selection.ConfirmSelection();
            }
            else
            {
                // Else, we'll open up the selector in a popup and let the user choose.
                selector.ShowInPopup(200);
            }
        }
    // Here is the actual ScriptableObjectSelector which inherits from OdinSelector.
    // You can learn more about those in the documentation: http://sirenix.net/odininspector/documentation/sirenix/odininspector/editor/odinselector(t)
    // This one builds a menu-tree of all types that inherit from T, and when the selection is confirmed, it then prompts the user
    // with a dialog to save the newly created scriptable object.

    private class ScriptableObjectSelector : OdinSelector<Type>
    {
        private System.Type[] types;
        private Action<UnityEngine.ScriptableObject> onScritpableObjectCreated;
        private string defaultDestinationPath;

        public ScriptableObjectSelector(System.Type[] types, string defaultDestinationPath, Action<UnityEngine.ScriptableObject> onScritpableObjectCreated = null)
        {
            this.types = types;
            this.onScritpableObjectCreated = onScritpableObjectCreated;
            this.defaultDestinationPath = defaultDestinationPath;
            this.SelectionConfirmed += this.ShowSaveFileDialog;
        }

        protected override void BuildSelectionTree(OdinMenuTree tree)
        {
            tree.Selection.SupportsMultiSelect = false;
            tree.Config.DrawSearchToolbar = true;
            tree.AddRange(types, x => x.GetNiceName().Replace("SO", ""))
                .AddThumbnailIcons();
        }

        private void ShowSaveFileDialog(IEnumerable<Type> selection)
        {
            var obj = ScriptableObject.CreateInstance(selection.FirstOrDefault());

            string dest = this.defaultDestinationPath.TrimEnd('/');

            if (!Directory.Exists(dest))
            {
                Directory.CreateDirectory(dest);
                AssetDatabase.Refresh();
            }

            dest = EditorUtility.SaveFilePanel("Save object as", dest, "New " + selection.FirstOrDefault().GetNiceName(), "asset");

            if (!string.IsNullOrEmpty(dest) && PathUtilities.TryMakeRelative(Path.GetDirectoryName(Application.dataPath), dest, out dest))
            {
                AssetDatabase.CreateAsset(obj, dest);
                AssetDatabase.Refresh();

                if (this.onScritpableObjectCreated != null)
                {
                    this.onScritpableObjectCreated(obj);
                }
            }
            else
            {
                UnityEngine.Object.DestroyImmediate(obj);
            }
        }
    }
}
#endif


////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////