using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using TheraBytes.BetterUi;

[HideMonoScript]
public class Panel_4_2_1_2_AudioSettings_Controller : MonoBehaviour
{
    // Public (Variables) [START]
    [Required]
    [SceneObjectsOnly]
    public BetterSlider generalAudio;
    [Required]
    [SceneObjectsOnly]
    public BetterSlider ambienceAudio;
    [Required]
    [SceneObjectsOnly]
    public BetterSlider soundtrackAudio;
    [Required]
    [SceneObjectsOnly]
    public BetterSlider UIAudio;
    [Required]
    [SceneObjectsOnly]
    public BetterSlider SFXGeneralAudio;
    [Required]
    [SceneObjectsOnly]
    public BetterSlider SFXCreaturesAudio;
    [Required]
    [SceneObjectsOnly]
    public BetterSlider SFXStructuresAudio;
    [Required]
    [SceneObjectsOnly]
    public BetterSlider SFXMiscelanousAudio;

    FMOD.Studio.Bus master;
    FMOD.Studio.Bus ambience;
    FMOD.Studio.Bus soundtrack;
    FMOD.Studio.Bus ui;
    FMOD.Studio.Bus SFX;
    FMOD.Studio.Bus SFXCreatures;
    FMOD.Studio.Bus SFXStructures;
    FMOD.Studio.Bus SFXMiscelanous;
    // Public (Variables) [END]

    // (Unity) Methods [START]
    private void Awake()
    {
        
    }
    void Start()
    {
        InitializeBuses();
        InitializeUI();
    }
    // (Unity) Methods [END]


    // Private (Methods) [START]
    private void InitializeBuses()
    {
        master = FMODUnity.RuntimeManager.GetBus("bus:/");
        ambience = FMODUnity.RuntimeManager.GetBus("bus:/Ambience");
        soundtrack = FMODUnity.RuntimeManager.GetBus("bus:/Soundtrack");
        ui = FMODUnity.RuntimeManager.GetBus("bus:/UI");
        SFX = FMODUnity.RuntimeManager.GetBus("bus:/SFX");
        SFXCreatures = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Creatures");
        SFXStructures = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Structures");
        SFXMiscelanous = FMODUnity.RuntimeManager.GetBus("bus:/SFX/Miscelanous");
    }
    private void InitializeUI()
    {
        ResetSliders();
    }

    private void ResetSliders()
    {
        float volume;

        master.getVolume(out volume);
        generalAudio.value = volume;

        ambience.getVolume(out volume);
        ambienceAudio.value = volume;

        soundtrack.getVolume(out volume);
        soundtrackAudio.value = volume;

        ui.getVolume(out volume);
        UIAudio.value = volume;

        SFX.getVolume(out volume);
        SFXGeneralAudio.value = volume;

        SFXCreatures.getVolume(out volume);
        SFXCreaturesAudio.value = volume;

        SFXStructures.getVolume(out volume);
        SFXStructuresAudio.value = volume;

        SFXMiscelanous.getVolume(out volume);
        SFXMiscelanousAudio.value = volume;
    }
    // Private (Methods) [END]


    // Public (Methods) [START]
    public void UpdateMasterVolume(float newValue) => master.setVolume(newValue);
    public void UpdateAmbienceVolume(float newValue) => ambience.setVolume(newValue);
    public void UpdateSoundtrackVolume(float newValue) => soundtrack.setVolume(newValue);
    public void UpdateUIVolume(float newValue) => ui.setVolume(newValue);
    public void UpdateSFXVolume(float newValue) => SFX.setVolume(newValue);
    public void UpdateSFXCreaturesVolume(float newValue) => SFXCreatures.setVolume(newValue);
    public void UpdateSFXStructuresVolume(float newValue) => SFXStructures.setVolume(newValue);
    public void UpdateSFXMiscelanousVolume(float newValue) => SFXMiscelanous.setVolume(newValue);
    // Public (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////