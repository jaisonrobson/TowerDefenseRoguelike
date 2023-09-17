using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

[ManageableData]
public class NatureSO : BaseOptionDataSO
{
    [BoxGroup("Box1", ShowLabel = false)]
    [HorizontalGroup("Box1/split", LabelWidth = 75)]

    [VerticalGroup("Box1/split/left")]
    [Required]
    [HideLabel]
    [PreviewField(100, Alignment = ObjectFieldAlignment.Center)]
    public Sprite image;

    [VerticalGroup("Box1/split/right")]
    [Required]
    public new string name;
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////