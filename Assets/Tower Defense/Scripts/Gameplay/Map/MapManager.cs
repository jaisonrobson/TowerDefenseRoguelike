using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Core.Patterns;
using System.Linq;
using Core.General;

[HideMonoScript]
public class MapManager : Singleton<MapManager>
{
    // Public (Variables) [START]
    public MapSO map;

    [PropertySpace(5f)]
    [HideInEditorMode]
    public GameObject hero;
    [Required]
    public Transform heroHook;
    // Public (Variables) [END]

    // Public (Properties) [START]
    public bool IsHeroAlive{ get { return IsStructureAlive(hero); } }
    public int PlayerInitialPointsQuantity { get { return map.playerInitialPoints; } }
    // Public (Properties) [END]

    // Unity Methods [START]
    void OnEnable()
    {
        InitializeMap();
        InitializeHero();
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
    private void InitializeHero()
    {
        string heroName = PlayerPrefs.GetString("selectedHero");

        AgentSO heroSO = Resources.LoadAll<AgentSO>("SO's/Agents").ToList().Where(agt => agt.subtype == AgentSubTypeEnum.HERO && agt.name == heroName).ToList().First();

        hero = Poolable.TryGetPoolable(heroSO.prefab);
        hero.GetComponent<Agent>().Alignment = map.playerAlignment.alignment;
        hero.transform.SetPositionAndRotation(heroHook.position, heroHook.rotation);
    }
    // Private Methods [END]

    // Public Methods [START]
    public List<AlignmentOpponentsSO> GetNonPlayableMapTeams()
    {
        return map.alignmentsOpponents.Where(ao => ao.alignment.alignment != map.playerAlignment.alignment).ToList();
    }
    public bool IsStructureAlive(GameObject pStructure) => pStructure.activeInHierarchy && pStructure.activeSelf;
    // Public Methods [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////