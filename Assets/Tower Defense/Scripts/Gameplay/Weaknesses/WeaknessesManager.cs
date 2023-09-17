using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Patterns;

public class WeaknessesManager : Singleton<WeaknessesManager>
{
    // Private (Variables) [START]
    private WeaknessesSO weaknesses;
    // Private (Variables) [END]

    // Public (Properties) [START]
    public WeaknessesSO Weaknesses { get { return weaknesses; } }
    // Public (Properties) [END]

    // (Unity) Methods [START]
    private void Start()
    {
        InitializeVariables();        
    }
    // (Unity) Methods [END]


    // Private (Methods) [START]
    private void InitializeVariables()
    {
        weaknesses = MapManager.instance.map.weaknesses;
    }
    // Private (Methods) [END]

    // Public (Methods) [START]
    public float GetWeakness(NatureSO invokerNature, NatureSO targetNature)
    {
        for (int i = 0; i < weaknesses.naturesMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < weaknesses.naturesMatrix.GetLength(1); j++)
            {
                if (weaknesses.naturesMatrix[i,j].invoker == invokerNature && weaknesses.naturesMatrix[i, j].target == targetNature)
                {
                    return weaknesses.weaknessMatrix[i, j];
                }
            }
        }

        return 0f;
    }
    // Public (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////