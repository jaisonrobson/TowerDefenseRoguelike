using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Core.Patterns;
using Sirenix.OdinInspector;

[RequireComponent(typeof(OverlayInterfaceController))]
[HideMonoScript]
public class OverlayInterfaceManager : Singleton<OverlayInterfaceManager>
{
    // Public (Variables) [START]
    [Required]
    [SceneObjectsOnly]
    public Canvas canvas;
    [BoxGroup("Interface Objects")]
    [Title("Panel 2")]
    [Required]
    [SceneObjectsOnly]
    public GameObject panel_2_1;
    [BoxGroup("Interface Objects")]
    [Required]
    [SceneObjectsOnly]
    public GameObject panel_2_2;
    [BoxGroup("Interface Objects")]
    [Title("Panel 7")]
    [Required]
    [SceneObjectsOnly]
    public GameObject panel_7;
    [BoxGroup("Interface Objects")]
    [Required]
    [SceneObjectsOnly]
    public GameObject panel_7_1;
    // Public (Variables) [END]


    // Public (Methods) [START]
    public bool IsOverUI()
    {
        PointerEventData m_PointerEventData = new PointerEventData(EventSystem.current);
        m_PointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();

        canvas.GetComponent<GraphicRaycaster>().Raycast(m_PointerEventData, results);

        return results.Count > 0;
    }
    public void OpenAgentPlacementPanel()
    {
        panel_7.SetActive(true);
        panel_7_1.SetActive(true);
    }
    public void CloseAgentPlacementPanel()
    {
        panel_7_1.SetActive(false);
        panel_7.SetActive(false);
    }
    // Public (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////