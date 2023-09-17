using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

[CustomEditor(typeof(CameraBounds))]
public class CameraBoundsEditor : Editor
{
    private BoxBoundsHandle m_BoundsHandle = new BoxBoundsHandle();

    protected virtual void OnSceneGUI()
    {
        CameraBounds cameraBounds = (CameraBounds)target;

        m_BoundsHandle.center = cameraBounds.bounds.center;
        m_BoundsHandle.size = cameraBounds.bounds.size;

        EditorGUI.BeginChangeCheck();

        m_BoundsHandle.DrawHandle();

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(cameraBounds, "Change bounds");

            Bounds newBounds = new Bounds();
            newBounds.center = m_BoundsHandle.center;
            newBounds.size = m_BoundsHandle.size;

            cameraBounds.bounds = newBounds;
        }
    }
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////