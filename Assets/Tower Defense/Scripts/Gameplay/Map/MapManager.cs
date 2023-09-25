using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Core.Patterns;
using System.Linq;
using Core.General;

[Serializable]
public struct MapEntityHook
{
    [Required]
    public Transform hook;
    [ValidateInput("Validate_MustBeValid_AgentSubtype", "The agent subtype must not be NONE.")]
    public AgentSubTypeEnum type;
    public bool getMainPlayerAlignment;
    [ValidateInput("Validate_MustBeValid_Alignment", "The alignment must not be GENERIC.")]
    public AlignmentEnum alignment;
    public int priority;
    [HideInEditorMode]
    [ReadOnly]
    public GameObject spawnedEntity;

    // (Validation) Methods [START]
    private bool Validate_MustBeValid_AgentSubtype() { return type != AgentSubTypeEnum.NONE; }
    private bool Validate_MustBeValid_Alignment() { return getMainPlayerAlignment || alignment != AlignmentEnum.GENERIC; }
    // (Validation) Methods [END]
}

[HideMonoScript]
public class MapManager : Singleton<MapManager>
{
    // Public (Variables) [START]
    public MapSO map;

    [PropertySpace(5f)]
    [Title("Map Structures")]
    [ValidateInput("Validate_MustHaveElements_MapEntityHooks", "Map Entity Hooks must have at least one valid element.")]
    public List<MapEntityHook> mapEntityHooks;

    // Public (Variables) [END]

    // Public (Properties) [START]
    public bool IsAnyPlayerMainEntityAlive{ get { return mapEntityHooks.Any(meh => meh.alignment == map.playerAlignment.alignment && IsStructureAlive(meh.spawnedEntity)); } }
    public List<GameObject> PlayerMainEntities { get { return mapEntityHooks.Where(meh => meh.alignment == map.playerAlignment.alignment).OrderBy(meh => meh.priority).Select(meh => meh.spawnedEntity).ToList(); } }
    public int PlayerInitialPointsQuantity { get { return map.playerInitialPoints; } }
    // Public (Properties) [END]

    // Unity Methods [START]
    void OnEnable()
    {
        InitializeMap();
        InitializeStructures();
    }
    // Unity Methods [END]


    // Private Methods [START]    
    private void InitializeMap()
    {
        string selectedMapName = PlayerPrefs.GetString("selectedMapName");
        MapSO selectedMap = map;

        MapSO[] maps = Resources.LoadAll<MapSO>("SO's/Maps");

        foreach (MapSO mp in maps)
        {
            if (mp.name == selectedMapName)
            {
                selectedMap = mp;
            }
        }

        if (selectedMap == null)
        {
            Debug.LogError("[MapManager] Selected map was not found");
        }
    }
    private void InitializeStructures()
    {
        mapEntityHooks = Utils.UpdateValueInStructList(mapEntityHooks, meh => {
            meh.alignment = meh.getMainPlayerAlignment ? map.playerAlignment.alignment : meh.alignment;
            meh.spawnedEntity = Poolable.TryGetPoolable(GetMapEntity(meh));
            meh.spawnedEntity.transform.SetPositionAndRotation(meh.hook.position, meh.hook.rotation);

            return meh;
        }).ToList();
    }
    private GameObject GetMapEntity(MapEntityHook pMeh)
    {
        GameObject result = null;

        if (pMeh.alignment == map.playerAlignment.alignment)
        {
            map.playerEntities.ToList().ForEach(pe => {
                if (pe.subtype == pMeh.type)
                    result = pe.prefab;
            });
        }

        return result;
    }
    // Private Methods [END]

    // Public Methods [START]
    public List<AlignmentOpponentsSO> GetNonPlayableMapTeams()
    {
        return map.alignmentsOpponents.Where(ao => ao.alignment.alignment != map.playerAlignment.alignment).ToList();
    }
    public bool IsStructureAlive(GameObject pStructure) => pStructure.activeInHierarchy && pStructure.activeSelf;
    // Public Methods [END]

    // (Validation) Methods [START]
    private bool Validate_MustHaveElements_MapEntityHooks() { return mapEntityHooks != null && mapEntityHooks.Count > 0; }
    // (Validation) Methods [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////