using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using Core.Patterns;

[RequireComponent(typeof(GameController))]
[HideMonoScript]
public class GameManager : Singleton<GameManager>
{
    // Private (Variables) [START]
    private bool isRunning;
    private bool isPaused;
    private float time;
    private float startTime;
    // Private (Variables) [END]

    // Public (Properties) [START]
    public bool IsRunning { get { return isRunning; } }
    public bool IsPaused { get { return isPaused; } }
    public bool IsRunningAndNotPaused { get { return isRunning && !isPaused; } }
    public float RunningTime { get { return time; } }
    public string FormattedTime { get { return FormatTime(time); } }
    // Public (Properties) [END]

    // (Unity) Methods [START]
    private void Start()
    {
        ResetVariables();
    }
    private void Update()
    {
        HandleWatchtimeUpdate();
    }
    // (Unity) Methods [END]

    // Private (Methods) [START]
    private void ResetVariables()
    {
        isRunning = false;
        isPaused = false;
        time = 0f;
        startTime = 0f;
    }
    private void HandleWatchtimeUpdate()
    {
        if (IsRunningAndNotPaused)
        {
            time = Time.time - startTime;
        }
    }
    private string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60f);
        int seconds = Mathf.FloorToInt(timeInSeconds - (minutes * 60));

        return string.Format("{0:0}:{1:00}", minutes, seconds);
    }
    // Private (Methods) [END]

    // Public (Methods) [START]
    public void StartGame()
    {
        WaveController.instance.StartWaves();
        startTime = Time.time;

        isRunning = true;
    }
    public void EndGame() => isRunning = false;
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
    }
    public void SetTimeScale(float scale)
    {
        float newScale = Mathf.Clamp(scale, 0.05f, 3f);

        Time.timeScale = newScale;
    }
    public void ResetTimeScale() => Time.timeScale = 1f;
    // Public (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////