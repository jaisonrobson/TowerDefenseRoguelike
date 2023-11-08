using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
#endif
using System.Linq;

[ManageableData]
public class StructuresProgressSO : BaseOptionDataSO
{
    [VerticalGroup("identity", PaddingBottom = 10, PaddingTop = 10)]
    [BoxGroup("identity/box", LabelText = "Identity")]
    public StructureTypeEnum type;

    [BoxGroup("identity/box")]
    public SkillSO levelOneSkill;

    [BoxGroup("identity/box")]
    public SkillSO levelTwoSkill;

    [BoxGroup("identity/box")]
    public SkillSO levelThreeSkill;
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////