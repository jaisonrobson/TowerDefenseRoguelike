using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using System.Reflection;
using Sirenix.OdinInspector.Editor.ValueResolvers;

public class CustomFloatTableMatrixAttributeDrawer : Sirenix.OdinInspector.Editor.OdinAttributeDrawer<CustomFloatTableMatrixAttribute, float[,]>
{
    private ValueResolver<string> drawRowsHeaderLabelsResolver;
    private ValueResolver<string> drawColumnsHeaderLabelsResolver;

    private Rect[,] rectTable;
    private float[,] original;
    private int selectedXIndex = -1;
    private int selectedYIndex = -1;

    protected override void Initialize()
    {
        this.drawRowsHeaderLabelsResolver = ValueResolver.GetForString(
            this.Property,
            this.Attribute.drawRowsHeaderLabelsAction,
            new NamedValue("rowIndex", typeof(int))
        );

        this.drawColumnsHeaderLabelsResolver = ValueResolver.GetForString(
            this.Property,
            this.Attribute.drawColumnsHeaderLabelsAction,
            new NamedValue("columnIndex", typeof(int))
        );
    }
    protected override void DrawPropertyLayout(GUIContent label)
    {
        original = this.ValueEntry.SmartValue;

        CreateRectMatrixTable();

        this.ValueEntry.SmartValue = original;

        this.ValueEntry.Property.MarkSerializationRootDirty();
    }

    //////////////////////////////////////////////////////////// //////////////////////////////////////////////////////////// 
    //////////////////////////////////////////////////////////// HELPER CODE
    //////////////////////////////////////////////////////////// //////////////////////////////////////////////////////////// 

    private void CreateRectMatrixTable()
    {        
        int tableRowColumnCount = original.GetLength(0) + 1;

        rectTable = new Rect[tableRowColumnCount, tableRowColumnCount];

        float windowSize = Sirenix.Utilities.Editor.GUIHelper.GetCurrentLayoutRect().width -50f;
        Vector2 tableTotatlRectSize = new Vector2(windowSize, 22f * tableRowColumnCount);

        Rect rect = EditorGUILayout.GetControlRect(GUILayout.Height(tableTotatlRectSize.y + 50f));

        Vector2 tableGridsSize = new Vector2(windowSize / tableRowColumnCount, 22f);
        Vector2 rectDrawingActualPosition = new Vector2(50f, rect.position.y + 50f);

        DrawExternalHelperHeaders(rect, tableTotatlRectSize, tableGridsSize);

        for (int i = 0; i < rectTable.GetLength(0); i++)
        {
            for (int j = 0; j < rectTable.GetLength(0); j++)
            {
                Rect newRect = new Rect(rectDrawingActualPosition, tableGridsSize);

                rectTable[i, j] = newRect;

                DrawTableCell(newRect, i, j);
                DrawTableRowHeadersLabels(newRect, i, j);
                DrawTableColumnHeadersLabels(newRect, i, j);                
                DrawTableCellsFields(newRect, i, j);

                rectDrawingActualPosition = new Vector2(rectDrawingActualPosition.x, rectDrawingActualPosition.y + tableGridsSize.y);
            }

            rectDrawingActualPosition = new Vector2(rectDrawingActualPosition.x + tableGridsSize.x, rect.position.y + 50f);
        }
    }

    private void DrawExternalHelperHeaders(Rect rect, Vector2 tableTotatlRectSize, Vector2 tableGridsSize)
    {
        //Draw Rows Header
        Rect rowsRect = new Rect(new Vector2(0f, rect.position.y + 22f + 50f), new Vector2(50f, tableTotatlRectSize.y - 23f));

        SirenixEditorGUI.DrawSolidRect(rowsRect, new Color(0.25f, 0.25f, 0.25f, 1f));
        SirenixEditorGUI.DrawBorders(rowsRect, 1);
        EditorGUI.LabelField(rowsRect.AlignCenterXY(50f), " Invoker \n  (From) \n       Y");

        //Draw Columns Header
        Rect columnsRect = new Rect(
            new Vector2(50f + tableGridsSize.x, rect.position.y),
            new Vector2(tableTotatlRectSize.x - tableGridsSize.x, 50f)
        );

        SirenixEditorGUI.DrawSolidRect(columnsRect, new Color(0.25f, 0.25f, 0.25f, 1f));
        SirenixEditorGUI.DrawBorders(columnsRect, 1);
        EditorGUI.LabelField(columnsRect.AlignCenterXY(100f), "Target\n  (To) \n     X");
    }

    private void DrawTableCell(Rect rect, int i, int j)
    {
        if (i == 0 && j == 0)
            return;
        if (i > 0 && j > 0)
            return;

        if ((selectedXIndex) == i || (selectedYIndex) == j)
        {
            Sirenix.Utilities.Editor.SirenixEditorGUI.DrawSolidRect(rect, new Color32(70, 96, 124, 255));
        }
        else
        {
            Sirenix.Utilities.Editor.SirenixEditorGUI.DrawSolidRect(rect, new Color(0.3f, 0.3f, 0.3f, 1f));
        }
        Sirenix.Utilities.Editor.SirenixEditorGUI.DrawBorders(rect, 1);
    }

    private void DrawTableRowHeadersLabels(Rect rect, int i, int j)
    {
        if (i == 0 && j != 0)
        {
            drawRowsHeaderLabelsResolver.Context.NamedValues.Set("rowIndex", j-1);

            GUIStyle alignmentStyle = new GUIStyle(GUI.skin.label);

            alignmentStyle.alignment = TextAnchor.MiddleCenter;

            EditorGUI.LabelField(rect, drawRowsHeaderLabelsResolver.GetValue(), alignmentStyle);
        }
    }

    private void DrawTableColumnHeadersLabels(Rect rect, int i, int j)
    {
        if (i != 0 && j == 0)
        {
            drawColumnsHeaderLabelsResolver.Context.NamedValues.Set("columnIndex", i - 1);

            GUIStyle alignmentStyle = new GUIStyle(GUI.skin.label);

            alignmentStyle.alignment = TextAnchor.MiddleCenter;

            EditorGUI.LabelField(rect, drawColumnsHeaderLabelsResolver.GetValue(), alignmentStyle);
        }
    }

    private void DrawTableCellsFields(Rect rect, int i, int j)
    {
        if (i > 0 && j > 0)
        {
            GUIStyle defaultStyle = new GUIStyle(GUI.skin.horizontalSlider);

            EditorGUI.BeginDisabledGroup(j == i);

            Rect newRect = rect.SetWidth(rect.width * 0.7f);

            float fieldValue = SirenixEditorFields.RangeFloatField(newRect, null, original[i-1,j-1], 0f, 2f, defaultStyle);

            int ctrlId = EditorGUIUtility.GetControlID(newRect.GetHashCode(), FocusType.Keyboard, newRect);

            if (ctrlId == (EditorGUIUtility.keyboardControl+2))
            {                
                selectedXIndex = i;
                selectedYIndex = j;                
            }

            EditorGUI.LabelField(new Rect(new Vector2(rect.position.x + newRect.width + 3f, newRect.position.y - 3f), new Vector2(rect.width * 0.2f, rect.height)), $"[{i-1},{j-1}]");

            EditorGUI.EndDisabledGroup();

            original[i-1, j-1] = fieldValue;

            if (j == i)
                original[i - 1, j - 1] = 0.5f;
        }
    }
}

#endif

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////