using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Core.Patterns;
public class CameraBounds : Singleton<CameraBounds>
{    
    public float Radius { get { return m_Radius; } set { m_Radius = value; } }
    public float Height { get { return m_Height; } }
    public Vector3 Center { get { return m_Center; } }

    [ShowInInspector]
    [SerializeField]
    private float m_Radius = 100f;

    [ShowInInspector]
    [SerializeField]
    private float m_Height = 30f;

    [ShowInInspector]
    [SerializeField]
    private Vector3 m_Center = new Vector3(0, 0, 0);
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////