using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_MapSelect : MonoBehaviour
{
    [HideInEditorMode]
    public string sceneName = "";
    [HideInEditorMode]
    public string mapName = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // Methods [START]

    public void LoadMap()
    {
        if (sceneName != "")
        {
            PlayerPrefs.SetString("sceneToLoad", sceneName);
            PlayerPrefs.SetString("selectedMapName", mapName);

            SceneManager.LoadScene("Map_Loader", LoadSceneMode.Single);
        }
        else
        {
            Debug.LogError("Empty map name found.");
        }
    }

    // Methods [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////