using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using Core.Patterns;

[HideMonoScript]
public class PlayerManager : Singleton<PlayerManager>
{
    // Public (Variables) [START]
    [Required]
    public FormulaSO levelFormula;
    // Public (Variables) [END]

    // Private (Variables) [START]
    private bool isLockingPlayerControl;
    private bool isPlayerAlive;
    private int points;
    private int level;
    private int experience;
    // Private (Variables) [END]

    // Public (Properties) [START]
    public bool IsPlayerAlive { get { return isPlayerAlive; } }
    public bool IsLockingPlayerControl { get { return isLockingPlayerControl; } }
    public int Points { get { return points; } }
    public int Level { get { return level; } }
    public int Experience { get { return experience; } }
    public int ExperienceLastLevel { get { return level > 1 ? Mathf.RoundToInt(levelFormula.CalculateFormulaFromValue(level-1)) : 0; } }
    public int ExperienceToNextLevel { get { return Mathf.RoundToInt(levelFormula.CalculateFormulaFromValue(level)); } }
    // Public (Properties) [END]

    // (Unity) Methods [START]
    private void OnEnable()
    {
        ResetVariables();
    }
    private void Start()
    {
        InitializePlayerPoints();
    }
    private void Update()
    {
        HandlePlayerAliveUpdate();
    }
    // (Unity) Methods [END]

    // Private (Methods) [START]
    private void ResetVariables()
    {
        isPlayerAlive = true;
        points = 0;
    }
    private void InitializePlayerPoints()
    {
        points = MapManager.instance.PlayerInitialPointsQuantity;
    }
    private void HandlePlayerAliveUpdate()
    {
        if (!MapManager.instance.IsAnyPlayerMainEntityAlive)
            isPlayerAlive = false;
    }
    // Private (Methods) [END]

    // Public (Methods) [START]
    public void IncreasePoints(int pQuantity)
    {
        points += Mathf.Abs(pQuantity);
    }
    public void DecreasePoints(int pQuantity)
    {
        if (CanDecreasePoints(pQuantity))
            points -= Mathf.Abs(pQuantity);
    }
    public bool CanDecreasePoints(int pQuantity) => points >= Mathf.Abs(pQuantity);
    public void IncreaseExperience(int pQuantity) {
        int newExperience = experience + Mathf.Abs(pQuantity);

        while (newExperience >= ExperienceToNextLevel)
            level++;

        experience += Mathf.Abs(pQuantity);
    }
    public void LockPlayerControl() => isLockingPlayerControl = true;
    public void UnlockPlayerControl() => isLockingPlayerControl = false;
    // Public (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////