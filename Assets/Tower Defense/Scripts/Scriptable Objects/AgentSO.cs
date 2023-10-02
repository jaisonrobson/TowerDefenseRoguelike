
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
#endif
using System.Linq;

[ManageableData]
public class AgentSO : BaseOptionDataSO
{
    // Configuration [START]
    private static string[] requiredKeys = { "spawn", "death" };

    public AgentSO()
    {
        GenerateDictionaries();
    }
    // Configuration [END]

    //Identity
    [VerticalGroup("base", PaddingBottom = 10, PaddingTop = 10)]    
    [HorizontalGroup("base/tr_1", Width = 0.5f, PaddingLeft = 10, PaddingRight = 10)]
    [BoxGroup("base/tr_1/td_1", LabelText = "Identity")]
    [HorizontalGroup("base/tr_1/td_1/tr_2", LabelWidth = 75, Width = 150, MaxWidth = 150)]

    [LabelWidth(0)]
    [VerticalGroup("base/tr_1/td_1/tr_2/td_1")]
    [PreviewField(100, ObjectFieldAlignment.Center)]
    [HideLabel]
    [Required]
    public Sprite image;

    [VerticalGroup("base/tr_1/td_1/tr_2/td_2")]
    [Required]
    public new string name;

    [VerticalGroup("base/tr_1/td_1/tr_2/td_2")]
    [Multiline(3)]
    public string description;

    [VerticalGroup("base/tr_1/td_1/tr_2/td_2")]
    [Required]
    public GameObject prefab;

    [VerticalGroup("base/tr_1/td_1/tr_2/td_2")]
    [Required]
    public NatureSO nature;

    [HorizontalGroup("base/tr_1/td_1/tr_3", LabelWidth = 75)]

    [VerticalGroup("base/tr_1/td_1/tr_3/td_1")]
    [Title("")]
    [GUIColor(1f, 0.8f, 0.4f, 1f)]
    [EnumToggleButtons]
    public AgentTypeEnum type;

    [VerticalGroup("base/tr_1/td_1/tr_3/td_1")]
    [GUIColor(1f, 0.8f, 0.4f, 1f)]
    public AgentSubTypeEnum subtype = AgentSubTypeEnum.NONE;

    [VerticalGroup("base/tr_1/td_1/tr_3/td_1")]
    [ToggleButtons("MOVABLE", "FIXED", trueColor: "@new Color(0.51f, 1f, 0.65f, 1f)", falseColor: "@new Color(1f, 0.56f, 0.51f, 1f)")]
    [OnValueChanged("Update_NotMovableStructures")]
    [ValidateInput("Validate_NotMovable_Structures", "Structures cannot move.")]
    public bool isMovable;

    [VerticalGroup("base/tr_1/td_1/tr_3/td_1")]
    [ToggleButtons("AGRESSIVE", "PEACEFUL", trueColor: "@new Color(0.51f, 1f, 0.65f, 1f)", falseColor: "@new Color(1f, 0.56f, 0.51f, 1f)")]
    public bool isAggressive = true;

    [VerticalGroup("base/tr_1/td_1/tr_3/td_1")]
    [ToggleButtons("PLAYABLE", "NON PLAYABLE", trueColor: "@new Color(0.51f, 1f, 0.65f, 1f)", falseColor: "@new Color(1f, 0.56f, 0.51f, 1f)")]
    public bool isPlayable;

    //Stats
    [HorizontalGroup("base/tr_1/tr_2", PaddingRight = 10, LabelWidth = 130)]

    [BoxGroup("base/tr_1/tr_2/td_1", LabelText = "Stats")]
    [Min(0)]
    [GUIColor(1f, 0.8f, 0.4f, 1f)]
    public int experienceOnDie;

    [BoxGroup("base/tr_1/tr_2/td_1")]
    [MinValue(1f)]
    [MaxValue(1000f)]
    [GUIColor(1f, 0.5411765f, 0.5411765f, 1f)]
    public float health = 10f;

    [BoxGroup("base/tr_1/tr_2/td_1")]
    [Min(0)]
    [GUIColor(0.7215f, 0.5137f, 0.9215f, 1f)]
    public float damage;

    [BoxGroup("base/tr_1/tr_2/td_1")]
    [GUIColor(0.3f, 0.8f, 0.8f, 1f)]
    [ProgressBarWithFields(0.3f, 5f, 0.3f, 0.8f, 1f)]
    public float attackVelocity = 0.3f;

    [BoxGroup("base/tr_1/tr_2/td_1")]
    [GUIColor(1f, 0.3f, 1f, 1f)]
    [ProgressBarWithFields(1.5f, 50f, 1f, 0.3f, 1f)]
    public float attackRange = 1.5f;

    [BoxGroup("base/tr_1/tr_2/td_1")]
    [GUIColor(0f, 0.8f, 0f, 1f)]
    [ProgressBarWithFields(3f, 10f, 0f, 0.8f, 0f)]
    public float velocity = 3f;

    [BoxGroup("base/tr_1/tr_2/td_1")]
    [GUIColor(1f, 1f, 1f, 1f)]
    [ProgressBarWithFields(3f, 50f, 0.8f, 0.8f, 0.8f)]
    public float visibilityArea = 3f;

    [BoxGroup("base/tr_1/tr_2/td_1")]
    [GUIColor(0.61f, 0.73f, 0.33f, 1f)]
    [MinMaxSlider(0, 100)]
    public Vector2Int evasion = new Vector2Int(0, 10);

    [VerticalGroup("second", PaddingBottom = 15)]
    [HorizontalGroup("second/split")]

    [VerticalGroup("second/split/left")]
    [HorizontalGroup("second/split/left/horizontal", PaddingLeft = 10, PaddingRight = 10)]
    [BoxGroup("second/split/left/horizontal/Box_1", LabelText = "Animations")]
    [HideLabel]
    [Required]
    [OnCollectionChanged(After = "Update_AfterCollectionChange_AnimationsRequiredKeys")]
    [OnValueChanged("Update_RequiredKeysOnDictionaries")]
    [ValidateInput("Validate_NotNull_AnimationsDictionaryValues", "Animations must have all values of the dictionary filled.")]
    public Dictionary<string, AnimationSO> animations;

    [VerticalGroup("second/split/right")]
    [HorizontalGroup("second/split/right/horizontal", PaddingRight = 10)]
    [BoxGroup("second/split/right/horizontal/Box_2", LabelText = "Sounds")]
    [HideLabel]
    [Required]
    [OnCollectionChanged(After = "Update_AfterCollectionChange_SoundsRequiredKeys")]
    [OnValueChanged("Update_RequiredKeysOnDictionaries")]
    [ValidateInput("Validate_NotNull_SoundsDictionaryValues", "Sounds must have all values of the dictionary filled.")]
    public Dictionary<string, SoundSO> sounds;

    //Optional
    [VerticalGroup("third", PaddingBottom = 10)]
    [HorizontalGroup("third/tr_1", PaddingLeft = 10, PaddingRight = 10)]
    [BoxGroup("third/tr_1/td_1", LabelText = "Optional")]
    [ListDrawerSettings(Expanded = true)]
    [PropertyTooltip("The other agents which this own agent can evolve into after the experience gauge of this own is filled.\n\nThe player will decide which of the options presented here this own agent will evolve into.")]
    [Required]
    public List<AgentSO> evolutionTree;

    [BoxGroup("third/tr_1/td_1")]
    [ListDrawerSettings(Expanded = true)]
    [PropertyTooltip("The attack movements that this own agent does.")]
    [Required]
    [ValidateInput("Validate_NotDistant_Attacks", "There is an impossible attack in the list because of its distance requirements against the agent distance range.")]
    public List<AttackSO> attacks;

    [BoxGroup("third/tr_1/td_1")]
    [ListDrawerSettings(Expanded = true)]
    [PropertyTooltip("The list of status immunities of the agent")]
    [OnCollectionChanged(After = "Update_AfterCollectionChange_StatusImmunities")]
    [OnInspectorGUI("Update_NotAgentType_Structure")]
    [ValidateInput("Validate_NotAgentType_Structure", "Structures must not have status immunities declared sinces it are all immune automatically.")]
    [Required]
    public List<StatusSO> statusImmunities;

    // Helper Methods [START]
    private void GenerateDictionaries()
    {
        if (animations == null)
        {
            animations = new Dictionary<string, AnimationSO>();

            requiredKeys.All(key => {
                animations.Add(key, null);

                return true;
            });
        }

        if (sounds == null)
        {
            sounds = new Dictionary<string, SoundSO>();

            requiredKeys.All(key => {
                sounds.Add(key, null);

                return true;
            });
        }
    }
    private void Update_RequiredKeysOnDictionaries()
    {
        GenerateDictionaries();

        requiredKeys.All(key => {
            if (!animations.ContainsKey(key))
                animations.Add(key, null);

            if (!sounds.ContainsKey(key))
                sounds.Add(key, null);

            return true;
        });
    }
    private void Update_NotMovableStructures()
    {
        if (type == AgentTypeEnum.STRUCTURE && isMovable == true)
            isMovable = false;
    }

#if UNITY_EDITOR
    private void Update_NotAgentType_Structure()
    {
        if (type == AgentTypeEnum.STRUCTURE)
            if (statusImmunities != null)
                statusImmunities.Clear();
    }

    private void Update_AfterCollectionChange_AnimationsRequiredKeys(CollectionChangeInfo info, object value)
    {
        switch (info.ChangeType)
        {
            case CollectionChangeType.RemoveKey:
                {
                    if (requiredKeys.Contains((string)info.Key))
                    {
                        animations.Add((string)info.Key, null);
                    }
                    else if (sounds.ContainsKey((string)info.Key))
                    {
                        sounds.Remove((string)info.Key);
                    }

                    break;
                }
            case CollectionChangeType.SetKey:
                {
                    if (!requiredKeys.Contains((string)info.Key) && !sounds.ContainsKey((string)info.Key))
                    {
                        sounds.Add((string)info.Key, null);
                    }

                    break;
                }
        }
    }
    private void Update_AfterCollectionChange_SoundsRequiredKeys(CollectionChangeInfo info, object value)
    {
        switch (info.ChangeType)
        {
            case CollectionChangeType.RemoveKey:
                {
                    if (requiredKeys.Contains((string)info.Key))
                    {
                        sounds.Add((string)info.Key, null);
                    }
                    else if (animations.ContainsKey((string)info.Key))
                    {
                        animations.Remove((string)info.Key);
                    }

                    break;
                }
            case CollectionChangeType.SetKey:
                {
                    if (!requiredKeys.Contains((string)info.Key) && !animations.ContainsKey((string)info.Key))
                    {
                        animations.Add((string)info.Key, null);
                    }

                    break;
                }
        }
    }
    private void Update_AfterCollectionChange_StatusImmunities(CollectionChangeInfo info, object value)
    {
        switch (info.ChangeType)
        {
            case CollectionChangeType.Add:
                {
                    List<StatusSO> statuses = statusImmunities.Where(si => si == (StatusSO)info.Value).ToList();

                    if (statuses.Count > 1)
                        statusImmunities.Remove((StatusSO)info.Value);
                    break;
                }
        }
    }
#endif
    // Helper Methods [END]

    // Validation Methods [START]
    private bool Validate_NotMovable_Structures() { return !(type == AgentTypeEnum.STRUCTURE && isMovable == true); }
    private bool Validate_NotNull_AnimationsDictionaryValues() { return !animations.ContainsValue(null); }
    private bool Validate_NotNull_SoundsDictionaryValues() { return !sounds.ContainsValue(null); }
    private bool Validate_NotDistant_Attacks() { return !attacks.Any(atSO => atSO.minimumAttackDistance > attackRange); }
    private bool Validate_NotAgentType_Structure() => statusImmunities.Count == 0 || statusImmunities.Count > 0 && type != AgentTypeEnum.STRUCTURE;
    // Validation Methods [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////