using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using Sirenix.OdinInspector;

[HideMonoScript]
public class Button_Heroes : MonoBehaviour
{
    // Public (Methods) [START]
    public void OnClick()
    {
        SceneManager.LoadScene("HeroSelection", LoadSceneMode.Single);
    }
    // Public (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////