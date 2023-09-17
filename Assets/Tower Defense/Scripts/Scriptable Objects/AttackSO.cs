using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[ManageableData]
public class AttackSO : BaseOptionDataSO
{
    [VerticalGroup("Identity", PaddingTop = 5f)]

    [BoxGroup("Identity/Box", LabelText = "Identity")]
    [GUIColor(1f, 0.8f, 0.4f, 1f)]
    [EnumToggleButtons]
    public AttackTypeEnum type = AttackTypeEnum.MELEE;

    [BoxGroup("Identity/Box")]
    [Required]
    [AssetsOnly]
    [PropertyTooltip("Represents the attack physical collision and model prefab.")]
    public GameObject prefab;

    [BoxGroup("Identity/Box")]
    [AssetsOnly]
    [PropertyTooltip("Represents the outcome attack prefab after the true attack was done/hit. (ex: secondary explosions)")]
    public GameObject outcomePrefab;

    [BoxGroup("Identity/Box")]
    [Required]
    public NatureSO nature;

    [BoxGroup("Identity/Box")]
    [Required]
    public FormulaSO formula;

    [BoxGroup("Identity/Box")]
    [Required]
    [Min(0f)]
    public float cooldown = 1f;

    [BoxGroup("Identity/Box")]
    [Required]
    [ProgressBarWithFields(10f, 100f, 0.3f, 0.8f, 1f)]
    public int influenceOverAttackVelocity = 50;

    [BoxGroup("Identity/Box")]
    [Required]
    [ProgressBarWithFields(0f, 50f, 1f, 0.3f, 1f)]
    public float minimumAttackDistance = 0f;

    [BoxGroup("Identity/Box")]
    [Required]
    [ProgressBarWithFields(1.5f, 50f, 1f, 0.3f, 1f)]
    public float maximumAttackDistance = 1.5f;

    [VerticalGroup("StatusesApplication", PaddingTop = 7f)]
    [BoxGroup("StatusesApplication/Box", LabelText = "Probability Status Affectors")]
    [ListDrawerSettings(Expanded = true)]
    [Required]
    public ProbabilityStatusAffectionSO[] onHitProbabilityStatusAffectors;

    [BoxGroup("StatusesApplication/Box")]
    [ListDrawerSettings(Expanded = true)]
    [Required]
    public ProbabilityStatusAffectionSO[] selfProbabilityStatusAffectors;

    [VerticalGroup("Animations&Sounds", PaddingTop = 7f)]
    [HorizontalGroup("Animations&Sounds/split", LabelWidth = 125, PaddingRight = 10)]

    [BoxGroup("Animations&Sounds/split/leftBox", LabelText = "Animations")]
    [PropertyTooltip("Animation when the attack spawns\n\nTo be used along with sounds")]
    [Required]
    public AnimationSO initialAnimation;

    [BoxGroup("Animations&Sounds/split/leftBox")]
    [PropertyTooltip("Animation when the attack travels to the target\n\nTo be used along with sounds\n\nThis is an optional field")]
    public AnimationSO trailAnimation;

    [BoxGroup("Animations&Sounds/split/leftBox")]
    [PropertyTooltip("Animation when the attack hits the target\n\nTo be used along with sounds")]
    [Required]
    public AnimationSO finalAnimation;

    [BoxGroup("Animations&Sounds/split/rightBox", LabelText = "Sounds")]
    [PropertyTooltip("Sound when the attack spawns\n\nTo be used along with animations")]
    [Required]
    public SoundSO initialSound;

    [BoxGroup("Animations&Sounds/split/rightBox")]
    [PropertyTooltip("Sound when the attack travels to the target\n\nTo be used along with animations\n\nThis is an optional field")]
    public SoundSO trailSound;

    [BoxGroup("Animations&Sounds/split/rightBox")]
    [PropertyTooltip("Sound when the attack hits the target\n\nTo be used along with animations")]
    [Required]
    public SoundSO finalSound;
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////