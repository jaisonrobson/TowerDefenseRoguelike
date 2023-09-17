using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Physics
{
    public static class Raycasting
    {
        public static RaycastHit ToMouseWorldPosition(LayerMask layers, Vector3 pOrigin)
        {
            Camera mainCamera = Camera.main;
            Vector3 origin = pOrigin != Vector3.zero ? pOrigin : mainCamera.transform.position;
            Vector3 mousePosition = Input.mousePosition; //Mouse in 2d space
            mousePosition.z = mainCamera.nearClipPlane + 1f; //Math of the camera depth in 3d world

            Vector3 worldMousePosition = mainCamera.ScreenToWorldPoint(mousePosition);

            Vector3 direction = (worldMousePosition - mainCamera.transform.position).normalized;

            if (UnityEngine.Physics.Raycast(origin, direction, out RaycastHit hitInfo, Mathf.Infinity, layers))
            {
                return hitInfo;
            }

            return new RaycastHit();
        }

        public static RaycastHit ToMouseWorldPosition(LayerMask layers)
        {
            return ToMouseWorldPosition(layers, Vector3.zero);
        }

        public static RaycastHit ScreenPointToRay(LayerMask layers)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (UnityEngine.Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, layers))
            {
                return hitInfo;
            }

            return new RaycastHit();
        }

        public static bool IsHitEmpty(RaycastHit hit)
        {
            return hit.point == Vector3.zero;
        }
    }
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////