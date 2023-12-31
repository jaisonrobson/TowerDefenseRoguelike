using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using Core.Patterns;
using Core.Math;

[HideMonoScript]
public class FogManager : Singleton<FogManager>
{
    // Public (Variables) [START]
    [BoxGroup("Main", true)]
    [Required]
    [SceneObjectsOnly]
    public AnimationFogController fogOne;
    [BoxGroup("Main")]
    [Required]
    [SceneObjectsOnly]
    public AnimationFogController fogTwo;
    [BoxGroup("Main")]
    [Required]
    [SceneObjectsOnly]
    public AnimationFogController fogThree;
    [BoxGroup("Extra", true)]
    [ListDrawerSettings(Expanded = true)]
    public List<GameObject> fogOneProps;
    [BoxGroup("Extra")]
    [ListDrawerSettings(Expanded = true)]
    public List<GameObject> fogTwoProps;
    [BoxGroup("Extra")]
    [ListDrawerSettings(Expanded = true)]
    public List<GameObject> fogThreeProps;
    // Public (Variables) [END]

    // Private (Variables) [START]
    private float fogRingAreaThickness = 40f;
    [SerializeField]
    [HideInEditorMode]
    [ReadOnly]
    private int actualFogDiscoveryStage = 0;
    // Private (Variables) [END]


    // Public (Properties) [START]
    public int ActualFogDiscoveryStage { get { return actualFogDiscoveryStage; } }
    // Public (Properties) [END]

    // (Unity) Methods [START]
    private void OnEnable()
    {
        InitializeVariables();
    }

    void Update()
    {
        HandleFogVisibility();
    }
    // (Unity) Methods [END]

    // Private (Methods) [START]
    private void InitializeVariables()
    {
        actualFogDiscoveryStage = 0;
        if (fogOneProps == null)
            fogOneProps = new List<GameObject>();

        if (fogTwoProps == null)
            fogTwoProps = new List<GameObject>();

        if (fogThreeProps == null)
            fogThreeProps = new List<GameObject>();

        fogOneProps.ForEach(prp => prp.SetActive(false));
        fogTwoProps.ForEach(prp => prp.SetActive(false));
        fogThreeProps.ForEach(prp => prp.SetActive(false));
    }
    private void HandleFogVisibility()
    {
        switch (actualFogDiscoveryStage)
        {
            case 1:
                break;
            case 2:
                fogOne.EndFog();
                fogOneProps.ForEach(prp => prp.SetActive(true));
                break;
            case 3:
                fogTwo.EndFog();
                fogTwoProps.ForEach(prp => prp.SetActive(true));
                break;
            case 4:
                fogThree.EndFog();
                fogThreeProps.ForEach(prp => prp.SetActive(true));
                break;
        }
    }
    // Private (Methods) [END]


    // Public (Methods) [START]
    public void StartFog()
    {
        fogOne.gameObject.SetActive(true);
        fogTwo.gameObject.SetActive(true);
        fogThree.gameObject.SetActive(true);

        actualFogDiscoveryStage  = 1;        
    }
    public void DiscoverNewFog() => actualFogDiscoveryStage++;
    public Vector3 GenerateRandomSpawnPosition()
    {
        float wallRadius = (40 * (actualFogDiscoveryStage + 1) - (40 * actualFogDiscoveryStage)) * 0.5f;
        float ringRadius = wallRadius + (40 * actualFogDiscoveryStage);

        Vector3 v = RNG.GetRandomPositionInTorus(ringRadius, wallRadius);
        v.y = 1f;

        return v;
    }
    // Public (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////