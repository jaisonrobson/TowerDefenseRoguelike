using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocusController : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (CameraController.instanceExists)
            CameraController.instance.followTransform = transform;
    }
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////