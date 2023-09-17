using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using Sirenix.OdinInspector;
using System;
using System.Linq;
using Core.General;

[System.Serializable]
public struct InvokerToTargetNature
{
    public NatureSO invoker;
    public NatureSO target;
    public int xIndex;
    public int yIndex;

    public InvokerToTargetNature(NatureSO pInvoker, NatureSO pTarget, int pXIndex = 0, int pYIndex = 0)
    {
        invoker = pInvoker;
        target = pTarget;
        xIndex = pXIndex;
        yIndex = pYIndex;
    }
}

[ManageableData]
public class WeaknessesSO : BaseOptionDataSO
{
    // Configuration [START]
    private static NatureSO[] naturesToDisplay;

#if UNITY_EDITOR
    public void OnEnable()
    {
        Generate_WeaknessesMatrix();
        Generate_NaturesMatrix();
    }
#endif
    // Configuration [END]
    [OnInspectorInit("Verify_NewTypesElements_WeaknessesMatrix")]
    [CustomFloatTableMatrix("$Draw_RowsHeadersLabels_WeaknessesMatrix", "$Draw_ColumnsHeadersLabels_WeaknessesMatrix")]
    [NonSerialized]
    [Sirenix.Serialization.OdinSerialize]
    public float[,] weaknessMatrix;

    [PropertySpace(25f)]

    [OnInspectorInit("Verify_NewTypesElements_NaturesMatrix")]
    [ReadOnly]
    [TableMatrix(IsReadOnly = true, DrawElementMethod = "Draw_Element_InvokerTarget_Natures", SquareCells = true)]
    [NonSerialized]
    [Sirenix.Serialization.OdinSerialize]
    public InvokerToTargetNature[,] naturesMatrix;


    // Helper Methods [START]
#if UNITY_EDITOR
    public void Generate_WeaknessesMatrix()
    {
        naturesToDisplay = Utils.FindAssetsByType<NatureSO>().ToArray();

        if (weaknessMatrix == null)
            weaknessMatrix = new float[naturesToDisplay.Length, naturesToDisplay.Length];
    }

    public void Generate_NaturesMatrix()
    {
        naturesToDisplay = Utils.FindAssetsByType<NatureSO>().ToArray();

        if (naturesMatrix == null)
        {
            naturesMatrix = new InvokerToTargetNature[naturesToDisplay.Length, naturesToDisplay.Length];

            PopulateNatureMatrix();
        }
    }

    public void PopulateNatureMatrix()
    {
        for (int i =0; i < naturesToDisplay.Length; i++)
        {
            for (int j = 0; j < naturesToDisplay.Length; j++)
            {
                InvokerToTargetNature itn = new InvokerToTargetNature(naturesToDisplay[j], naturesToDisplay[i], i, j);

                naturesMatrix[i, j] = itn;
            }
        }
    }

    public void Verify_NewTypesElements_NaturesMatrix()
    {
        Generate_NaturesMatrix();

        if (naturesMatrix.GetLength(0) != naturesToDisplay.Length)
        {
            naturesMatrix = Utils.ResizeMatrix(naturesMatrix, naturesToDisplay.Length, naturesToDisplay.Length);

            PopulateNatureMatrix();
        }
    }

    public void Verify_NewTypesElements_WeaknessesMatrix()
    {
        Generate_WeaknessesMatrix();

        if (weaknessMatrix.GetLength(0) != naturesToDisplay.Length)
        {
            weaknessMatrix = Utils.ResizeMatrix(weaknessMatrix, naturesToDisplay.Length, naturesToDisplay.Length);
        }
    }
    public static InvokerToTargetNature Draw_Element_InvokerTarget_Natures(Rect rect, InvokerToTargetNature value)
    {
        Rect fromPos = new Rect(rect);
        fromPos.width = rect.width / 2;
        fromPos.x += rect.width / 3;
        fromPos.y -= (rect.height / 4) * 1.4f;

        Rect middlePos = new Rect(rect);
        middlePos.width = rect.width / 2;
        middlePos.x += rect.width / 2.5f;
        middlePos.y -= (rect.height / 4) * 0.8f;

        Rect toPos = new Rect(rect);
        toPos.width = rect.width / 2;
        toPos.x += rect.width / 3;
        toPos.y -= (rect.height / 4) * 0.2f;

        Rect indexes = new Rect(rect);
        indexes.width = rect.width / 2;
        indexes.x += rect.width / 3;
        indexes.y += (rect.height / 4) * 1f;

        EditorGUI.LabelField(fromPos, value.invoker.name.Substring(0, 5));
        EditorGUI.LabelField(middlePos, ">>>");
        EditorGUI.LabelField(toPos, value.target.name.Substring(0, 5));
        EditorGUI.LabelField(indexes, "["+value.xIndex+","+value.yIndex+"]");
        return value;
    }

    public static float Draw_MatrixFloat(Rect rect, float value)
    {
        return 0f;
    }

    public string Draw_RowsHeadersLabels_WeaknessesMatrix(int rowIndex)
    {
        return naturesToDisplay != null && naturesToDisplay.ElementAtOrDefault(rowIndex) != null ? naturesToDisplay[rowIndex].name : "Error";
    }

    public string Draw_ColumnsHeadersLabels_WeaknessesMatrix(int columnIndex)
    {
        return naturesToDisplay != null && naturesToDisplay.ElementAtOrDefault(columnIndex) != null ? naturesToDisplay[columnIndex].name : "Error";
    }
    #endif
    // Helper Methods [END]

}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////