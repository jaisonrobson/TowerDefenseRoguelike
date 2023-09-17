using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Patterns;
using Sirenix.OdinInspector;

[HideMonoScript]
public class WorldSpaceInterfaceManager : Singleton<WorldSpaceInterfaceManager>
{
    // Public (Variables) [START]
    [Required]
    [PropertySpace(0f, 15f)]
    public Canvas canvas;        
    
    [Required]
    [AssetsOnly]
    [BoxGroup("Prefabs")]
    [Title("Slider")]
    public GameObject sliderSmall;
    [Required]
    [AssetsOnly]
    [BoxGroup("Prefabs")]
    public GameObject sliderMedium;
    [Required]
    [AssetsOnly]
    [BoxGroup("Prefabs")]
    public GameObject sliderLarge;
    [Required]
    [AssetsOnly]
    [BoxGroup("Prefabs")]
    public GameObject sliderExtraLarge;

    [PropertySpace(5f)]

    [Required]
    [AssetsOnly]
    [BoxGroup("Prefabs")]
    [Title("Text Floating Value")]
    public GameObject textFloatingValueSmall;
    [Required]
    [AssetsOnly]
    [BoxGroup("Prefabs")]
    public GameObject textFloatingValueMedium;
    [Required]
    [AssetsOnly]
    [BoxGroup("Prefabs")]
    public GameObject textFloatingValueLarge;
    [Required]
    [AssetsOnly]
    [BoxGroup("Prefabs")]
    public GameObject textFloatingValueExtraLarge;

    [PropertySpace(5f)]

    [Required]
    [AssetsOnly]
    [BoxGroup("Prefabs")]
    [Title("Buttons")]
    public GameObject buttonLevelUp;

    [PropertySpace(5f)]

    [Required]
    [AssetsOnly]
    [BoxGroup("Prefabs")]
    [Title("Horizontal Layouts")]
    public GameObject horizontalLayoutSmallStatuses;

    // Public (Variables) [END]

    // Public (Methods) [START]
    public GameObject GetSlider(SizeEnum size)
    {
        GameObject result;

        switch (size)
        {
            case SizeEnum.MEDIUM:
                result = sliderMedium;
                break;
            case SizeEnum.LARGE:
                result = sliderLarge;
                break;
            case SizeEnum.EXTRALARGE:
                result = sliderExtraLarge;
                break;
            default:
                result = sliderSmall;
                break;
        }

        return result;
    }
    public GameObject GetTextFloatingValue(SizeEnum size)
    {
        GameObject result;

        switch (size)
        {
            case SizeEnum.MEDIUM:
                result = textFloatingValueMedium;
                break;
            case SizeEnum.LARGE:
                result = textFloatingValueLarge;
                break;
            case SizeEnum.EXTRALARGE:
                result = textFloatingValueExtraLarge;
                break;
            default:
                result = textFloatingValueSmall;
                break;
        }

        return result;
    }

    public GameObject GetLevelUpButton() => buttonLevelUp;
    // Public (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////