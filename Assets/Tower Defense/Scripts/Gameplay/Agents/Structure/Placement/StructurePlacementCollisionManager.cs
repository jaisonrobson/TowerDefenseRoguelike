using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;

[HideMonoScript]
public class StructurePlacementCollisionManager : MonoBehaviour
{
    //This represents the other structures objects
    private List<GameObject> collidedObjects = new List<GameObject>();

    //This represents the structure areas. (if this structure is contained inside any structure area)
    private List<GameObject> containedInsideObjects = new List<GameObject>();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(GetComponent<BoxCollider>().bounds.max, 1f);
        Gizmos.DrawSphere(GetComponent<BoxCollider>().bounds.min, 1f);
    }

    // Methods [START]
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            collidedObjects.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            collidedObjects.Remove(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 8 && other.gameObject.GetComponent<PlacementArea>().alignment == MapManager.instance.map.playerAlignment.alignment)
        {
            if (containedInsideObjects.Contains(other.gameObject))
            {
                if (!other.bounds.Contains(GetComponent<BoxCollider>().bounds.max) || !other.bounds.Contains(GetComponent<BoxCollider>().bounds.min))
                {
                    containedInsideObjects.Remove(other.gameObject);
                }
            }
            else
            {
                if (other.bounds.Contains(GetComponent<BoxCollider>().bounds.max) && other.bounds.Contains(GetComponent<BoxCollider>().bounds.min))
                {
                    containedInsideObjects.Add(other.gameObject);
                }
            }
        }
    }

    public void PoolInsertionAction()
    {
        containedInsideObjects.Clear();
        collidedObjects.Clear();
    }
    public bool CanPlaceStructure() { return collidedObjects.Count == 0 && containedInsideObjects.Count > 0; }
    // Methods [END]


}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////