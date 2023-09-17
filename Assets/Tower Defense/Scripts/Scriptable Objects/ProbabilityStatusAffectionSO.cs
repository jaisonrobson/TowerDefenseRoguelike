using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

[ManageableData]
public class ProbabilityStatusAffectionSO : BaseOptionDataSO
{
    [BoxGroup("Box1", ShowLabel = false)]
    [Required]
    public StatusAffectorSO statusAffector;

    [BoxGroup("Box1")]
    [ProgressBarWithFields(0, 100, 1f, 1f, 1f)]
    public int probability = 100;
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////