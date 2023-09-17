using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

[ManageableData]
public class AnimationSO : BaseOptionDataSO
{
    [BoxGroup("Box1", showLabel: false)]
    [PropertyTooltip("The prefab with particle animations or anything else that does the animation.")]
    [Required]
    public GameObject prefab;

    [BoxGroup("Box1")]
    [PropertyTooltip("The natural scale of the animation, used in animation rescaling methods.")]
    [OdinSerialize]
    public Vector3 animationSize;

    [PropertyRange(0f, 10f)]
    [BoxGroup("Box1")]
    [PropertyTooltip("Time until the animation particles starts.")]
    public float delay = 0f;

    [PropertyRange(0f, 10f)]
    [BoxGroup("Box1")]
    [PropertyTooltip("Time until the chained animation starts.")]
    public float delayBeforeChainedAnimation = 0f;

    [VerticalGroup("vertical", PaddingTop = 5f)]

    [BoxGroup("vertical/Box2", LabelText = "Optional")]
    [PropertyTooltip("If not null, this chained animation is to be called after the execution of this own.")]
    public AnimationSO chainedAnimation;
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////