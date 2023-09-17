using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Core.Patterns;
public class CameraBounds : Singleton<CameraBounds>
{
    public Bounds bounds { get { return m_Bounds; } set { m_Bounds = value; } }

    [ShowInInspector]
    [SerializeField]
    private Bounds m_Bounds = new Bounds(Vector3.zero, Vector3.one * 25f);
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////