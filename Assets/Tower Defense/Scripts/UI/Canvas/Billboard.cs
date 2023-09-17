using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public new Transform camera;
    void Start()
    {
        if (camera == null)
            camera = Camera.main.transform;
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + camera.forward);
    }
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////