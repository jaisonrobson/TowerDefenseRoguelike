using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;

[HideMonoScript]
public class AgentSelectionDisplayCreator : MonoBehaviour
{
    public LayerMask layer;

    public Sprite agentSelectionSprite;

    private GameObject agentSelectionDisplayGObj;

    // Unity Methods [START]
    private void OnEnable()
    {
        
    }
    private void Start()
    {
        CreateAgentSelectionDisplayGameObject();
        CreateAgentSelectionDisplayManager();
    }
    private void Update()
    {
        UpdateAgentSelectionOptions();
    }
    // Unity Methods [END]

    // Private Methods [START]
    private void UpdateAgentSelectionOptions()
    {
        if (agentSelectionDisplayGObj == null)
            return;

        AlignmentEnum ae = GetComponent<Agent>().Alignment;

        if (ae == AlignmentEnum.GENERIC)
            return;

        AlignmentSO alignment = AlignmentManager.instance.GetAlignment(ae);

        if (agentSelectionDisplayGObj != null && alignment != null)
        {
            agentSelectionDisplayGObj.GetComponent<SpriteRenderer>().color = alignment.color;

            Vector3 agentSize = GetComponent<Agent>().mainCollider.bounds.size;

            agentSelectionDisplayGObj.transform.localScale = new Vector3(agentSize.x * 1.2f, agentSize.x * 1.2f, 0);
        }
    }
    private void CreateAgentSelectionDisplayGameObject()
    {
        SpriteRenderer sr;
        if (agentSelectionDisplayGObj == null)
        {
            agentSelectionDisplayGObj = new GameObject();
            agentSelectionDisplayGObj.AddComponent<SpriteRenderer>();
        }

        sr = agentSelectionDisplayGObj.GetComponent<SpriteRenderer>();

        agentSelectionDisplayGObj.transform.SetParent(transform, false);
        agentSelectionDisplayGObj.transform.localPosition = new Vector3(0, 0.01f, 0);
        agentSelectionDisplayGObj.transform.rotation = new Quaternion(0, 0, 0, 0);
        agentSelectionDisplayGObj.transform.Rotate(-90f, 0f, 0f);
        agentSelectionDisplayGObj.name = "Agent Selection Display";
        sr.sprite = agentSelectionSprite;

        agentSelectionDisplayGObj.layer = (int)Mathf.Log(layer.value, 2);
    }
    private void CreateAgentSelectionDisplayManager()
    {
        AgentSelectionDisplayManager asdm = gameObject.AddComponent<AgentSelectionDisplayManager>();

        asdm.AgentSelectionDisplayGObj = agentSelectionDisplayGObj;
    }
    // Private Methods [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////