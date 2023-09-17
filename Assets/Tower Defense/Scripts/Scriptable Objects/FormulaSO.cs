using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
#endif

[System.Serializable]
public struct FloatBarsValue
{
    [HideInInspector]
    public int minLimit;
    [HideInInspector]
    public int maxLimit;
    [HideInInspector]
    public Color barColor;

    [HideLabel]
    [ProgressBar("minLimit", "maxLimit", ColorGetter = "barColor")]
    public int value;
}

[ManageableData]
public class FormulaSO : BaseOptionDataSO
{
    private bool showListsInvalidSizesError = false;
    
    [BoxGroup("Formula Example")]
    [OnInspectorGUI("DrawFormulaPreview", append: true)]
    public float exampleInitialValue = 3f;

    [PropertySpace(10f)]
    [HorizontalGroup("horizontal")]
    [VerticalGroup("horizontal/left")]
    [ListDrawerSettings(Expanded = true)]
    [PropertyTooltip("Each float here, represents a percentage on top of the previous.")]
    [OnValueChanged("OnUpdateDamageOrOperators")]
    [OnCollectionChanged("OnAddFormulaValues")]
    [InfoBox("Formulas must have lenght size major than 'operators' in one element", "showListsInvalidSizesError", InfoMessageType = InfoMessageType.Error)]
    public FloatBarsValue[] formulas;

    [PropertySpace(10f)]
    [VerticalGroup("horizontal/right")]
    [ListDrawerSettings(Expanded = true)]
    [PropertyTooltip("Each string here, represents a mathematical operator to be used by the result sum + float of the next index of this." +
        "\n\nFor example: [sum] operators[0] formulas[1]." +
        "\n\nThe sum start with formulas[0] value." +
        "\n\nAlso: this list is going to have always one member less than damages in order to the math works.")]
    [OnValueChanged("OnUpdateDamageOrOperators")]
    [OnCollectionChanged("OnAddOperatorValues")]
    [InfoBox("Operators must have lenght size minor than 'formulas' in one element", "showListsInvalidSizesError", InfoMessageType = InfoMessageType.Error)]
    public MathematicalOperatorEnum[] operators;

    // Public (Methods) [START]
    public float CalculateFormulaFromValue(float value, int formulaIndex = 0)
    {
        float result = value;

        //Bug prevention on remove/add formula fields
        if (operators == null || formulas == null || formulas.Length == 0)
            return result;
        if (operators.Length < (formulas.Length-1))
            return result;

        if (formulaIndex == 0)
        {
            result = value * ((float)formulas[formulaIndex].value / 100);
        }
        else
        {
            switch (operators[formulaIndex - 1])
            {
                case MathematicalOperatorEnum.ADD:
                    result = value + (value * ((float)formulas[formulaIndex].value / 100));
                    break;

                case MathematicalOperatorEnum.SUBTRACT:
                    result = value - (value * ((float)formulas[formulaIndex].value / 100));
                    break;

                case MathematicalOperatorEnum.DIVIDE:
                    result = value / ((float)formulas[formulaIndex].value / 100);
                    break;

                case MathematicalOperatorEnum.MULTIPLY:
                    result = value * ((float)formulas[formulaIndex].value / 100);
                    break;

                case MathematicalOperatorEnum.EXPONENTIAL:
                    result = (float)System.Math.Exp((double)value * ((float)formulas[formulaIndex].value / 100));
                    break;

                case MathematicalOperatorEnum.LOGARITHM:
                    result = (float)System.Math.Log((double)value * ((float)formulas[formulaIndex].value / 100));
                    break;
            }
        }

        if ((formulas.Length - 1) == formulaIndex)
            return result;
        else
            return CalculateFormulaFromValue(result, formulaIndex + 1);
    }
    // Public (Methods) [END]

    // Private (Methods) [START]
#if UNITY_EDITOR
    private int[] getRangeLimitsToFormula(int formulaIndex)
    {
        int[] result = new int[2];
        result[0] = 1;
        result[1] = 100;

        if (!(operators.Length == (formulas.Length - 1)) || operators.Length == 0 || formulaIndex == 0)
            return result;

        switch (operators[formulaIndex - 1])
        {
            case MathematicalOperatorEnum.ADD:
                result[0] = 1;
                result[1] = 100;

                break;

            case MathematicalOperatorEnum.DIVIDE:
                result[0] = 100;
                result[1] = 1000;

                break;

            case MathematicalOperatorEnum.MULTIPLY:
                result[0] = 100;
                result[1] = 1000;

                break;

            case MathematicalOperatorEnum.SUBTRACT:
                result[0] = 1;
                result[1] = 100;

                break;

            case MathematicalOperatorEnum.EXPONENTIAL:
                result[0] = 1;
                result[1] = 100;

                break;

            case MathematicalOperatorEnum.LOGARITHM:
                result[0] = 1;
                result[1] = 100;

                break;
        }

        return result;
    }
    private Color getColorToFormula(int formulaIndex)
    {
        Color result = new Color32(153, 208, 205, 255);

        if (!(operators.Length == (formulas.Length - 1)) || operators.Length == 0 || formulaIndex == 0)
            return result;

        switch (operators[formulaIndex - 1])
        {
            case MathematicalOperatorEnum.ADD:
                result = new Color32(157, 255, 137, 255);

                break;

            case MathematicalOperatorEnum.DIVIDE:
                result = new Color32(255, 231, 153, 255);

                break;

            case MathematicalOperatorEnum.MULTIPLY:
                result = new Color32(161, 153, 255, 255);

                break;

            case MathematicalOperatorEnum.SUBTRACT:
                result = new Color32(255, 153, 153, 255);

                break;

            case MathematicalOperatorEnum.EXPONENTIAL:
                result = new Color32(216, 95, 210, 255);

                break;

            case MathematicalOperatorEnum.LOGARITHM:
                result = new Color32(254, 254, 254, 255);

                break;
        }

        return result;
    }
    private void DrawFormulaPreview()
    {
        int swatchesCount = 7;
        float size = 13f;
        float spacing = 5f;
        float baseY = 5f;
        float baseX = 0f;

        float rectHeight = (swatchesCount * size) + (swatchesCount * spacing) + baseY * 3;

        Rect box = UnityEditor.EditorGUILayout.GetControlRect(GUILayout.Height(rectHeight), GUILayout.ExpandWidth(true));

        DrawHorizontalLine(box, baseY, baseX);

        DrawColorSwatches(box, spacing, size, baseY + 5f, baseX + 5f);

        DrawColorSwatchesLabels(box, spacing, size, baseY + 10f, baseX + 50f);

        DrawVerticalLine(box, baseY, baseX + 175f);

        DrawExampleFormulaMathResult(box, spacing, size, baseY + 5f, baseX + 185f);

        DrawVerticalLine(box, baseY, baseX + 350f);

        DrawFormulaTable(box, spacing, size, baseY + 10f, baseX + 360f, 1, 1);

        DrawVerticalLine(box, baseY, baseX + 490f);

        DrawFormulaTable(box, spacing, size, baseY + 10f, baseX + 500f, 10, 5);

        DrawVerticalLine(box, baseY, baseX + 630f);

        DrawFormulaTable(box, spacing, size, baseY + 10f, baseX + 640f, 50, 25);
    }
    private void DrawFormulaTable(Rect box, float spacing, float size, float baseY, float baseX, int initialValue = 1, int jumpScale = 1)
    {
        for (int i = 0; i < 7; i++)
            DrawFormulaTableElement(box, spacing, size, baseY, baseX, i+1, (i * jumpScale) + initialValue);
    }
    private void DrawFormulaTableElement(Rect box, float spacing, float size, float baseY, float baseX, int number = 1, int value = 1)
    {
        DrawLabel(box, spacing, size, baseY, baseX, value + ":", number);
        DrawLabel(box, spacing, size, baseY, baseX + 35f, CalculateFormulaFromValue(value).ToString(), number);
    }
    private void DrawExampleFormulaMathResult(Rect box, float spacing, float size, float baseY, float baseX)
    {
        Rect labelMathResult = new Rect(box.x + baseX, box.y + spacing + baseY, 125, size);
        UnityEditor.EditorGUI.LabelField(labelMathResult, "Result: " + CalculateFormulaFromValue(exampleInitialValue));
    }
    private void DrawVerticalLine(Rect box, float baseY, float baseX)
    {
        Rect verticalLine = new Rect(box.x + baseX, box.y + baseY, 1f, box.height - baseY);
        UnityEditor.EditorGUI.DrawRect(verticalLine, new Color32(90, 90, 90, 255));
    }
    private void DrawHorizontalLine(Rect box, float baseY, float baseX)
    {
        Rect horizontalLine = new Rect(box.x + baseX, box.y + baseY, box.width, 1f);
        UnityEditor.EditorGUI.DrawRect(horizontalLine, new Color32(90, 90, 90, 255));
    }
    private void DrawColorSwatchesLabels(Rect box, float spacing, float size, float baseY, float baseX)
    {
        DrawLabel(box, spacing, size, baseY, baseX, "Sum / Initial value", 1);
        DrawLabel(box, spacing, size, baseY, baseX, "Addition", 2);
        DrawLabel(box, spacing, size, baseY, baseX, "Subtraction", 3);
        DrawLabel(box, spacing, size, baseY, baseX, "Division", 4);
        DrawLabel(box, spacing, size, baseY, baseX, "Multiplication", 5);
        DrawLabel(box, spacing, size, baseY, baseX, "Exponential", 6);
        DrawLabel(box, spacing, size, baseY, baseX, "Logarithm", 7);
    }
    private void DrawLabel(Rect box, float spacing, float size, float baseY, float baseX, string text, int labelNumber = 1)
    {
        int number = labelNumber - 1;
        Rect labelRect = new Rect(box.x + baseX, box.y + (spacing * number) + ( size * number ) + baseY, 125, size);
        UnityEditor.EditorGUI.LabelField(labelRect, text);
    }
    private void DrawColorSwatches(Rect box, float spacing, float size, float baseY, float baseX)
    {
        DrawColorSwatch(box, spacing, size, baseY, baseX, new Color32(153, 208, 205, 255), 1);
        DrawColorSwatch(box, spacing, size, baseY, baseX, new Color32(157, 255, 137, 255), 2);
        DrawColorSwatch(box, spacing, size, baseY, baseX, new Color32(255, 153, 153, 255), 3);
        DrawColorSwatch(box, spacing, size, baseY, baseX, new Color32(255, 231, 153, 255), 4);
        DrawColorSwatch(box, spacing, size, baseY, baseX, new Color32(161, 153, 255, 255), 5);
        DrawColorSwatch(box, spacing, size, baseY, baseX, new Color32(216, 95, 210, 255), 6);
        DrawColorSwatch(box, spacing, size, baseY, baseX, new Color32(254, 254, 254, 255), 7);
    }
    private void DrawColorSwatch(Rect box, float spacing, float size, float baseY, float baseX, Color32 color, int colorNumber = 1)
    {
        Rect colorRect = new Rect(box.x + baseX, box.y + (spacing * colorNumber) + (size * (colorNumber - 1)) + baseY, size, size);
        Sirenix.Utilities.Editor.SirenixEditorGUI.DrawSolidRect(colorRect, color);
    }
#endif
    // Private (Methods) [END]

#if UNITY_EDITOR
    // Validation Methods [START]
    [OnInspectorGUI]
    private void OnUpdateDamageOrOperators()
    {   
        //Validation
        showListsInvalidSizesError = !(operators.Length == (formulas.Length - 1));

        //Formula fields updates according to mathematical operators
        for (int index = 0; index < formulas.Length; index++)
        {
            int[] limits = getRangeLimitsToFormula(index);
            Color color = getColorToFormula(index);

            formulas[index].minLimit = limits[0];
            formulas[index].maxLimit = limits[1];
            formulas[index].barColor = color;
        }
    }
    private void OnAddFormulaValues(CollectionChangeInfo info, object value)
    {
        if (info.ChangeType == CollectionChangeType.Add || info.ChangeType == CollectionChangeType.Insert)
        {
            int[] limits = getRangeLimitsToFormula(formulas.Length - 1);
            Color color = getColorToFormula(formulas.Length - 1);

            formulas[formulas.Length-1].minLimit = limits[0];
            formulas[formulas.Length - 1].maxLimit = limits[1];
            formulas[formulas.Length - 1].barColor = color;
            formulas[formulas.Length - 1].value = limits[0];

            if (formulas.Length > 1 && operators.Length < formulas.Length -1)
                operators = operators.Concat(new[] { MathematicalOperatorEnum.ADD }).ToArray();
        }

        if (info.ChangeType == CollectionChangeType.RemoveIndex)
            if (operators.Length > formulas.Length-1)
                operators = operators.SkipLast(1).ToArray();
    }
    private void OnAddOperatorValues(CollectionChangeInfo info, object value)
    {
        if (info.ChangeType == CollectionChangeType.Add || info.ChangeType == CollectionChangeType.Insert)
            if (formulas.Length-1 < operators.Length)
                formulas = formulas.Concat(new[] { new FloatBarsValue() }).ToArray();

        if (info.ChangeType == CollectionChangeType.RemoveIndex)
            if (formulas.Length-1 > operators.Length)
                formulas = formulas.SkipLast(1).ToArray();
    }
    [OnInspectorInit]
    private void InitializeFormulas()
    {
        if (formulas == null)
            formulas = new FloatBarsValue[1];

        if (formulas.Length == 0)
            formulas = formulas.Concat(new[] { new FloatBarsValue() }).ToArray();
    }
    // Validation Methods [END]
#endif
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////