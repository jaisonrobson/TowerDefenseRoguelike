using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[ManageableData]
public class StatusSO : BaseOptionDataSO
{
    [BoxGroup("Box1", ShowLabel = false)]
    [HorizontalGroup("Box1/split", LabelWidth = 75)]

    [VerticalGroup("Box1/split/left")]    
    [Required]
    [PreviewField(100, Alignment = ObjectFieldAlignment.Center)]
    [HideLabel]
    public Sprite image;

    [VerticalGroup("Box1/split/right")]
    [Required]
    public StatusEnum status;

    [VerticalGroup("Box1/split/right")]
    [Required]
    public NatureSO nature;

    [VerticalGroup("Box1/split/right")]
    public string description;
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////