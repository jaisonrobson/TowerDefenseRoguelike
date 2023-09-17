using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using Febucci.UI;
using TMPro;
using Core.Math;

[RequireComponent(typeof(TextMeshProUGUI))]
[RequireComponent(typeof(TextAnimatorPlayer))]
[RequireComponent(typeof(TextAnimator))]
[RequireComponent(typeof(Billboard))]
public class FloatingTextController : MonoBehaviour
{
    // Public (Variables) [START]
    public float movingAnimationTime = 3f;
    public Vector3 initialLocalPosition;
    public Vector3 finalLocalPosition;
    [MinMaxSlider(-3, 3, true)]
    public Vector2 randomness = new Vector2(-2f, 2f);
    // Public (Variables) [END]

    // Private (Variables) [START]
    private TextAnimatorPlayer textAnimPlayer;
    private TextAnimator textAnim;
    private TextMeshProUGUI text;
    [ShowInInspector]
    [HideInEditorMode]
    [ReadOnly]
    private float value = 1f;
    [ShowInInspector]
    [HideInEditorMode]
    [ReadOnly]
    private Color color = new Color32(255, 0, 0, 255);
    [ShowInInspector]
    [HideInEditorMode]
    [ReadOnly]
    private Transform targetToFollow;
    [ShowInInspector]
    [HideInEditorMode]
    [ReadOnly]
    private float targetHeightOffset = 0f;
    [ShowInInspector]
    [HideInEditorMode]
    [ReadOnly]
    private float timeUntilFinishMoving = 0;
    [ShowInInspector]
    [HideInEditorMode]
    [ReadOnly]
    private Vector3 tmpInitialLocalPosition;
    [ShowInInspector]
    [HideInEditorMode]
    [ReadOnly]
    private Vector3 tmpFinalLocalPosition;
    // Private (Variables) [END]

    // Public (Properties) [START]
    public float Value { get { return value; } set { this.value = value; } }
    public Color Color { get { return color; } set { color = value; } }
    // Public (Properties) [END]

    // (Unity) Methods [START]
    private void OnEnable()
    {
        InitializeVariables();
    }
    private void LateUpdate()
    {
        HandleMotionUpdate();
    }
    // (Unity) Methods [END]

    // Private (Methods) [START]
    private void HandleMotionUpdate()
    {
        timeUntilFinishMoving += Time.deltaTime / movingAnimationTime;

        transform.localPosition = Vector3.Lerp(tmpInitialLocalPosition, tmpFinalLocalPosition, timeUntilFinishMoving);

        if (transform.localPosition == tmpFinalLocalPosition)
            textAnimPlayer.StartDisappearingText();
    }
    private void SetupValues()
    {
        if (value == 0f)
            text.text = "MISS";
        else
            text.text = (string)value.ToString();

        text.color = color;

        Vector3 rngPosition = RNG.Vector3(randomness.x, randomness.y);

        tmpInitialLocalPosition = initialLocalPosition + rngPosition;
        tmpInitialLocalPosition = new Vector3(targetToFollow.position.x, targetToFollow.position.y + targetHeightOffset, targetToFollow.position.z) + tmpInitialLocalPosition;

        tmpFinalLocalPosition = finalLocalPosition + rngPosition;
        tmpFinalLocalPosition = new Vector3(targetToFollow.position.x, targetToFollow.position.y + targetHeightOffset, targetToFollow.position.z) + tmpFinalLocalPosition;

        transform.localPosition = tmpInitialLocalPosition;
        timeUntilFinishMoving = 0;
        textAnimPlayer.StopDisappearingText();
    }
    private void InitializeVariables()
    {
        textAnimPlayer = GetComponent<TextAnimatorPlayer>();
        textAnim = GetComponent<TextAnimator>();
        text = GetComponent<TextMeshProUGUI>();
    }
    // Private (Methods) [END]

    // Public (Methods) [START]
    public void SetTarget(Transform target) { targetToFollow = target; }
    public void SetTargetHeightOffset(float offset) { targetHeightOffset = offset + 2f; }
    public void ForceMotionUpdate() { SetupValues(); }
    // Public (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////