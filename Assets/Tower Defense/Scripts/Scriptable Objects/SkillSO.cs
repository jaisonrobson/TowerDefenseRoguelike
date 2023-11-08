using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
#endif
using System.Linq;

[ManageableData]
public class SkillSO : BaseOptionDataSO
{
    [VerticalGroup("identity", PaddingBottom = 10, PaddingTop = 10)]
    [BoxGroup("identity/box", LabelText = "Identity")]

    [PreviewField(100, ObjectFieldAlignment.Center)]
    [Required]
    public Sprite image;

    [BoxGroup("identity/box")]
    [Required]
    public new string name;

    [BoxGroup("identity/box")]
    [ToggleButtons("ACTIVE", "PASSIVE", trueColor: "@new Color(0.51f, 1f, 0.65f, 1f)", falseColor: "@new Color(1f, 0.56f, 0.51f, 1f)")]
    public bool isActive;

    [BoxGroup("identity/box")]
    [AssetsOnly]
    [Required]
    public GameObject prefab;

    [BoxGroup("identity/box")]
    [AssetsOnly]
    public GameObject outcomePrefab;

    [BoxGroup("identity/box")]
    [Required]
    public NatureSO nature;

    [BoxGroup("identity/box")]
    [Required]
    public FormulaSO formula;

    [BoxGroup("identity/box")]
    [Min(1f)]
    public float cooldown;

    [BoxGroup("identity/box")]
    [Min(0f)]
    public float damage;

    [BoxGroup("identity/box")]
    [Min(0f)]
    public float mana;

    [VerticalGroup("Animations&Sounds", PaddingTop = 7f)]
    [HorizontalGroup("Animations&Sounds/split", LabelWidth = 125, PaddingRight = 10)]

    [BoxGroup("Animations&Sounds/split/leftBox", LabelText = "Animations")]
    [Required]
    public AnimationSO initialAnimation;

    [BoxGroup("Animations&Sounds/split/leftBox")]
    public AnimationSO trailAnimation;

    [BoxGroup("Animations&Sounds/split/leftBox")]
    public AnimationSO finalAnimation;

    [BoxGroup("Animations&Sounds/split/rightBox", LabelText = "Sounds")]
    [Required]
    public SoundSO initialSound;

    [BoxGroup("Animations&Sounds/split/rightBox")]
    public SoundSO trailSound;

    [BoxGroup("Animations&Sounds/split/rightBox")]
    public SoundSO finalSound;

}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////