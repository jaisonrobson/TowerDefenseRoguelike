using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;

[HideMonoScript]
public class AgentEnemyDetectionCreator : MonoBehaviour
{
    public CollisionAreaTypeEnum colliderAreaType;
    public LayerMask layer;

    public Mesh areaDisplayMesh;

    private GameObject edcGObj;
    private GameObject edGObjAreaDisplay;
    private float areaSize = 3f;
    private bool moldedCollider = false;
    

    // Unity Methods [START]
    private void OnEnable()
    {
        InitializeVariables();
        CreateEDCGameObject();
        CreateEDAreaDisplayGameObject();

        PrepareEDCGameObject();
        PrepareEDAreaDisplayGameObject();
        MoldCollider();
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        UpdateAreaDisplayMaterial();
    }
    // Unity Methods [END]

    // Private Methods [START]
    private void InitializeVariables()
    {
        this.areaSize = GetComponent<Agent>().VisibilityArea;
    }
    private void UpdateAreaDisplayMaterial()
    {
        if (edGObjAreaDisplay == null)
            return;

        List<AlignmentMaterialsSO> amSOs = Resources.LoadAll<AlignmentMaterialsSO>("SO's/Alignment Materials").ToList();
        AlignmentEnum ae = GetComponent<Agent>().Alignment;
        AlignmentMaterialsSO alignmentMaterial = amSOs.Where(am => am.alignment.alignment == ae).FirstOrDefault();
        if (edGObjAreaDisplay != null && alignmentMaterial != null)
        {
            edGObjAreaDisplay.GetComponent<MeshRenderer>().material = alignmentMaterial.area_highlighting;
            edGObjAreaDisplay.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        }
    }
    private void MoldCollider()
    {
        if (moldedCollider)
            return;

        moldedCollider = true;

        switch (colliderAreaType)
        {
            case CollisionAreaTypeEnum.CIRCULAR:
                MoldCircularCollider();
                break;
            case CollisionAreaTypeEnum.RECTANGLE:
                MoldRectangularCollider();
                break;
        }
    }
    private void CreateEDCGameObject()
    {
        if (edcGObj == null)
        {
            edcGObj = new GameObject();
            edcGObj.AddComponent<Rigidbody>();
        }

        edcGObj.transform.SetParent(this.transform, false);
        edcGObj.transform.localPosition = new Vector3(0, 0, 0);
        edcGObj.transform.rotation = new Quaternion(0, 0, 0, 0);
        edcGObj.name = "Enemy Detection";
        edcGObj.layer = (int)Mathf.Log(layer.value, 2);
        edcGObj.GetComponent<Rigidbody>().useGravity = false;
        edcGObj.GetComponent<Rigidbody>().isKinematic = true;
    }
    private void CreateEDAreaDisplayGameObject()
    {
        MeshFilter mf;
        if (edGObjAreaDisplay == null)
        {
            edGObjAreaDisplay = new GameObject();
            edGObjAreaDisplay.AddComponent<MeshRenderer>();
            mf = edGObjAreaDisplay.AddComponent<MeshFilter>();            
        }
        else
        {
            mf = edGObjAreaDisplay.GetComponent<MeshFilter>();
        }

        edGObjAreaDisplay.transform.SetParent(edcGObj.transform, false);
        edGObjAreaDisplay.transform.localPosition = new Vector3(0, 0, 0);
        edGObjAreaDisplay.transform.rotation = new Quaternion(0, 0, 0, 0);
        edGObjAreaDisplay.transform.localScale = new Vector3(areaSize * 2, areaSize * 2, areaSize * 2);
        edGObjAreaDisplay.name = "Enemy Detection Area Display";
        mf.mesh = areaDisplayMesh;
    }
    private void PrepareEDCGameObject()
    {
        if (edcGObj != null && edcGObj.GetComponent<AgentEnemyDetectionColliderManager>() == null)
        {
            edcGObj.AddComponent<AgentEnemyDetectionColliderManager>();
        }
    }
    private void PrepareEDAreaDisplayGameObject()
    {
        if (edGObjAreaDisplay != null && edcGObj.GetComponent<AgentEnemyDetectionAreaDisplayManager>() == null)
        {
            AgentEnemyDetectionAreaDisplayManager aedadm = edcGObj.AddComponent<AgentEnemyDetectionAreaDisplayManager>();
            aedadm.AreaDisplayGameObject = edGObjAreaDisplay;
        }
    }
    private void MoldCircularCollider()
    {
        edcGObj.AddComponent<SphereCollider>();

        edcGObj.GetComponent<SphereCollider>().isTrigger = true;
        edcGObj.GetComponent<SphereCollider>().radius = areaSize;
        edcGObj.GetComponent<AgentEnemyDetectionColliderManager>().DetectionCollider = edcGObj.GetComponent<SphereCollider>();
    }
    private void MoldRectangularCollider()
    {
        edcGObj.AddComponent<BoxCollider>();

        edcGObj.GetComponent<BoxCollider>().isTrigger = true;
        edcGObj.GetComponent<BoxCollider>().center = new Vector3(0, areaSize / 2, areaSize / 2);
        edcGObj.GetComponent<BoxCollider>().size = new Vector3(areaSize, areaSize, areaSize);
        edcGObj.GetComponent<AgentEnemyDetectionColliderManager>().DetectionCollider = edcGObj.GetComponent<BoxCollider>();
    }
    // Private Methods [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////