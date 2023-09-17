using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[ManageableData]
public class StatusAffectorSO : BaseOptionDataSO
{
    [BoxGroup("Box1", ShowLabel = false)]
    [Required]
    public StatusSO status;

    [BoxGroup("Box1", ShowLabel = false)]
    [Required]
    public AnimationSO animation;

    [BoxGroup("Box1", ShowLabel = false)]
    [Required]
    public SoundSO sound;

    [BoxGroup("Box1")]
    [PropertyTooltip("The prefab that holds the scripting configuration for this status.")]
    [Required]
    [AssetsOnly]
    public GameObject prefab;

    [BoxGroup("Box1")]
    [Required]
    [PropertyTooltip("The total duration of the affector.")]
    [ProgressBarWithFields(1, 180, 1f, 1f, 1f)]
    public int duration = 1;

    [BoxGroup("Box1")]
    [Required]
    [PropertyTooltip("The damage percentage each turn will deal to the affected.")]
    [ProgressBarWithFields(0, 100, 1f, 1f, 1f)]
    public int damage = 0;

    [BoxGroup("Box1")]
    [Required]
    [PropertyTooltip("The number of turns will be divided by the total duration equally.")]
    [ProgressBarWithFields(1, 50, 1f, 1f, 1f)]
    public int turnsQuantity = 1;

    [BoxGroup("Box1")]
    [PropertyTooltip("The influence the status exerts on the affected in percentage.")]
    [PropertyRange(0, 100)]
    [ProgressBarWithFields(0, 100, 1f, 1f, 1f)]
    public int influence = 0;

    [BoxGroup("Box1")]
    [PropertyTooltip("Any kind of special condition that needs to be met by the status.")]
    [ProgressBarWithFields(0, 100, 1f, 1f, 1f)]
    public int specialCondition = 0;
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////