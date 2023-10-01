using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using TheraBytes.BetterUi;

[HideMonoScript]
public class Panel_1_2_1_PlayerExperienceBar_Controller : MonoBehaviour
{

    // Public (Variables) [START]
    [Required]
    [SceneObjectsOnly]
    public BetterSlider playerExperienceSlider;
    // Public (Variables) [END]

    // (Unity) Methods [START]
    void Start()
    {
        
    }

    void Update()
    {
        HandleExperienceSliderRefreshing();
    }
    // (Unity) Methods [END]


    // Private (Methods) [START]
    private void HandleExperienceSliderRefreshing()
    {
        playerExperienceSlider.minValue = PlayerManager.instance.ExperienceLastLevel;
        playerExperienceSlider.maxValue = PlayerManager.instance.ExperienceToNextLevel;
        playerExperienceSlider.value = PlayerManager.instance.Experience;
    }
    // Private (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////