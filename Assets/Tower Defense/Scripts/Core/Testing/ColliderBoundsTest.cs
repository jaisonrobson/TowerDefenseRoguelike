using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

[HideMonoScript]
public class ColliderBoundsTest : MonoBehaviour
{
	// (Unity) Methods [START]
    void Start()
    {
        
    }

    void Update()
    {
        Collider c = GetComponent<Collider>();

        if (c != null)
            Debug.Log(c.bounds.size);
    }
    // (Unity) Methods [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////