using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using FMODUnity;

[ManageableData]
public class SoundSO : BaseOptionDataSO
{
    [BoxGroup("Box1", ShowLabel = false)]
    [Required]
    [PropertyTooltip("A stand alone prefab "
    + "that has it own source of sound and auto execute itself according to the rules down below")]
    public GameObject prefab;

    [Title("")]
    [BoxGroup("Box1")]
    [Required]
    [PropertyTooltip("The event path string to play in fmod studio")]
    public EventReference fmodEvent;

    [Title("")]
    [BoxGroup("Box1")]
    public bool loop = false;

    [BoxGroup("Box1")]
    [PropertyRange(0, 10f)]
    public float delay = 0f;

    [BoxGroup("Box1")]
    [PropertyRange(0, 1f)]
    public float volume = 1f;

    [BoxGroup("Box1")]
    [PropertyTooltip("0 means max priority over other audio sources, 256 less priority")]
    [PropertyRange(0f, 256f)]
    public float priority = 128f;

    [BoxGroup("Box1")]
    [PropertyTooltip("Change randomly the pitch max variation according to the value set here, 0 means no pitch variation, 3 means it can variate between 0 and 3 anytime.")]
    [PropertyRange(0f, 3f)]
    public float pitchVariation = 0f;

    [VerticalGroup("vertical", PaddingTop = 5f)]
    [BoxGroup("vertical/Box2", LabelText = "Optional")]
    public SoundSO chainedSound;
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////