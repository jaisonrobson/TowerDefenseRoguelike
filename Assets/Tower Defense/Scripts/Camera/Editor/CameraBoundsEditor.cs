using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

[CustomEditor(typeof(CameraBounds))]
public class CameraBoundsEditor : Editor
{
    private SphereBoundsHandle m_BoundsHandle_1 = new SphereBoundsHandle();
    private SphereBoundsHandle m_BoundsHandle_2 = new SphereBoundsHandle();

    protected virtual void OnSceneGUI()
    {
        CameraBounds cameraBounds = (CameraBounds)target;

        m_BoundsHandle_1.center = new Vector3(cameraBounds.Center.x, cameraBounds.Center.y - cameraBounds.Height, cameraBounds.Center.z);
        m_BoundsHandle_1.radius = cameraBounds.Radius;

        m_BoundsHandle_1.axes = PrimitiveBoundsHandle.Axes.X | PrimitiveBoundsHandle.Axes.Z;

        m_BoundsHandle_1.DrawHandle();

        m_BoundsHandle_2.center = new Vector3(cameraBounds.Center.x, cameraBounds.Center.y + cameraBounds.Height, cameraBounds.Center.z);
        m_BoundsHandle_2.radius = cameraBounds.Radius;

        m_BoundsHandle_2.axes = PrimitiveBoundsHandle.Axes.X | PrimitiveBoundsHandle.Axes.Z;

        m_BoundsHandle_2.DrawHandle();

        /*
        EditorGUI.BeginChangeCheck();

        

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(cameraBounds, "Change bounds");

            Bounds newBounds = new Bounds();
            newBounds.center = m_BoundsHandle.center;
            newBounds.size = m_BoundsHandle.size;

            cameraBounds.bounds = newBounds;
        }
        */
    }
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////