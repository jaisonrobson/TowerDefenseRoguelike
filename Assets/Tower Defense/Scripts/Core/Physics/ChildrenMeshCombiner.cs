using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class ChildrenMeshCombiner : MonoBehaviour
{
    void Start()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();

        meshFilters = meshFilters.Where(mf => mf != transform.GetComponent<MeshFilter>()).ToArray();

        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);
            i++;
        }
        transform.GetComponent<MeshFilter>().mesh = new Mesh();
        transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
        transform.GetComponent<MeshCollider>().sharedMesh = transform.GetComponent<MeshFilter>().mesh;
        transform.gameObject.SetActive(true);
    }
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////