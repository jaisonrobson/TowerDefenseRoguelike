using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TheraBytes.BetterUi;

public class CreateButtons_MapSelect : MonoBehaviour
{
    [SceneObjectsOnly]
    [Required]
    public GameObject parent;

    [AssetsOnly]
    [Required]
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        CreateButtons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    // Methods [START]

    public void CreateButtons()
    {
        MapSO[] maps = Resources.LoadAll<MapSO>("SO's/Maps");

        foreach (MapSO map in maps )
        {
            GameObject newMapButton = Instantiate(prefab, parent.transform);

            newMapButton.GetComponent<Button_MapSelect>().sceneName = map.sceneName;
            newMapButton.GetComponent<Button_MapSelect>().mapName = map.name;

            BetterTextMeshProUGUI textComponent = newMapButton.GetComponentInChildren<BetterTextMeshProUGUI>();

            if (textComponent != null)
            {
                textComponent.text = map.GetName();
            }
        }
    }

    // Methods [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////