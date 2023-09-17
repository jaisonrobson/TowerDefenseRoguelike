using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

[RequireComponent(typeof(Slider))]
[RequireComponent(typeof(Billboard))]
public class SliderController : WorldspaceInterfaceObjectController
{
    // Private (Variables) [START]
    private Slider slider;
    private Image fill;
    [ShowInInspector]
    [HideInEditorMode]
    [ReadOnly]
    private float value = 1f;
    [ShowInInspector]
    [HideInEditorMode]
    [ReadOnly]
    private float maxValue = 1f;
    [ShowInInspector]
    [HideInEditorMode]
    [ReadOnly]
    private float minValue = 0;
    [ShowInInspector]
    [HideInEditorMode]
    [ReadOnly]
    private Color color = new Color32(255, 0, 0, 255);
    // Private (Variables) [END]

    // Public (Properties) [START]
    public float Value { get { return value; }  set { this.value = value >= minValue && value <= maxValue ? value : this.value; } }
    public float MaxValue { get { return maxValue; } set { maxValue = value; } }
    public float MinValue { get { return minValue; } set { minValue = value; } }
    public Color Color { get { return color; } set { color = value; } }
    // Public (Properties) [END]

    // (Unity) Methods [START]
    protected override void Start()
    {
        base.Start();

        slider = GetComponent<Slider>();
        fill = slider.fillRect.GetComponent<Image>();
    }
    protected override void Update()
    {
        base.Update();

        slider.minValue = minValue;
        slider.maxValue = maxValue;
        slider.value = value;
        fill.color = color;
    }
    // (Unity) Methods [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////