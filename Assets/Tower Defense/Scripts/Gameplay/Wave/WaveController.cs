using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
using Core.Patterns;
using Pathfinding;
#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
#endif
using Core.Math;

[HideMonoScript]
public class WaveController : OdinSingleton<WaveController>
{
    /// <summary>
    /// The spawn points that the enemies will be spawned.
    /// A random point between this list is selected each time a enemy is spawned.
    /// </summary>
    [Required]
    [SceneObjectsOnly]
    [ListDrawerSettings(Expanded = true)]
    [ValidateInput("Validate_MustHaveElements_SpawnPoints", "The spawn points list, must have at least one element.")]
    [ValidateInput("Validate_ValidElements_SpawnPoints_Aligned", "[Spawn Points] must have all alignment types stated by MapSO or at least one generic type declared.")]
    public List<SpawnPointManager> spawnPoints = new List<SpawnPointManager>();

    /// <summary>
    /// The time that the system will wait until starts the next wave counting.
    /// 
    /// * There is a different field for each simultaneous wave sequence.
    /// </summary>
    /// 
    [MinValue(0f)]
    [Required]
    [ListDrawerSettings(Expanded = true)]
    [OnCollectionChanged(After = "After_CollectionChange_TimeBetweenWaves")]
    [ValidateInput("Validate_MustHaveCollectionSize_TimeBetweenWaves", "[TimeBetweenWaves] must have collection size the same as the configured MapSO Aligned Waves sequences.")]
    public List<float> timeBetweenWaves = new List<float>();

    /// <summary>
    /// The waves that already finished. (its monsters got already spawned)
    /// The int shows the wave order, starting from 0 (zero) for the first wave and so on.
    /// 
    /// * There is a different field for each simultaneous wave sequence.
    /// </summary>
    [SerializeField, HideInEditorMode, DisableInPlayMode]
    private Dictionary<int, WaveSO>[] wavesHistory;

    /// <summary>
    /// The index on the map waves of the actual wave that is running
    /// 
    /// * There is a different field for each simultaneous wave sequence.
    /// </summary>
    [SerializeField, HideInEditorMode, DisableInPlayMode]
    private int[] runningWave;

    /// <summary>
    /// Did the player pressed the start button?
    /// </summary>
    [SerializeField, HideInEditorMode, DisableInPlayMode]
    private bool isRunning = false;

    /// <summary>
    /// Is the wave sequence running
    /// 
    /// * There is a different field for each simultaneous wave sequence.
    /// </summary>
    [SerializeField, HideInEditorMode, DisableInPlayMode]
    private bool[] isRunningSequence;

    /// <summary>
    /// The time since the player pressed the start button until the last monster of the last wave is killed.
    /// </summary>
    [SerializeField, HideInEditorMode, DisableInPlayMode]
    private float runningTime = 0f;

    /// <summary>
    /// The time since the start of the actual wave.
    /// This gets reset at every new wave.
    /// 
    /// * There is a different field for each simultaneous wave sequence.
    /// </summary>
    [SerializeField, HideInEditorMode, DisableInPlayMode]
    private float[] actualWaveRunningTime;

    /// <summary>
    /// The waiting time the wave did before start
    /// 
    ///  * This gets reset at every new wave.
    ///  * There is a different field for each simultaneous wave sequence.
    /// </summary>
    [SerializeField, HideInEditorMode, DisableInPlayMode]
    private float[] actualWaveWaitedTime;

    /// <summary>
    /// This stores the next index on the wave of the agent and the spawn time of the next agent that will be spawned at the running wave.
    /// 
    /// * There is a different field for each simultaneous wave sequence.
    /// </summary>
    [SerializeField, HideInEditorMode, DisableInPlayMode]
    private int[] nextIndexToSpawn;

    /// <summary>
    /// Tells if the wave is waiting the configured time before start the next one.
    /// 
    /// * There is a different field for each simultaneous wave sequence.
    /// </summary>
    [SerializeField, HideInEditorMode, DisableInPlayMode]
    private bool[] isUnderWaitTime;

    private List<GameObject> spawnedAgents;

    //Aux variable
    private int actualAgentSpawningIndex;
    private Transform lastRandomSpawnPointPicked;

    // Public (Properties) [START]
    public bool IsRunning { get { return isRunning; } }
    public bool IsAnyAgentAlive { get { return spawnedAgents.Any(sa => sa.activeSelf && sa.activeInHierarchy); } }
    // Public (Properties) [END]

    // Unity Methods [START]
    private void Start()
    {
        ResetVariables();
    }
    private void Update()
    {
        HandleWavesOperation();

        if (isRunning && GameManager.instance.IsRunningAndNotPaused)
        {
            IncrementTimers();
            SpawnAgents();
        }
    }
    // Unity Methods [END]

    // Private Methods [START]
    private void ResetVariables()
    {
        isRunning = false;
        spawnedAgents = new List<GameObject>();

        ResetWavesOperations();
    }
    private List<SpawnPointManager> GetSpawnPointsGrouped(AlignmentEnum alignment, bool includeGeneric = false)
    {
        return spawnPoints.Where(sp => sp.alignment == alignment || (includeGeneric && sp.alignment == 0)).ToList();
    }
    private void HandleWavesOperation()
    {
        if (!isRunning) return;

        if (DidAllWavesSequences())
            isRunning = false;
    }
    private bool DidAllWavesInSequence(int sequenceIndex) { return !isRunningSequence[sequenceIndex]; }
    private bool DidAllWavesSequences() { return isRunningSequence.All(isRun => isRun == false); }
    private AlignedWaveSO[][] GetMapAlignedWaves() { return MapManager.instance.map.alignedWaves; }
    private WaveSO GetRunningWave(int index) { return GetMapAlignedWaves()[index][runningWave[index]].wave; }
    private AlignedWaveSO GetRunningAlignedWave(int index) { return GetMapAlignedWaves()[index][runningWave[index]]; }
    private void IncrementTimers()
    {
        IncrementTotalTimer();
        IncrementWavesTimers();
    }
    private void IncrementTotalTimer() { runningTime += Time.deltaTime; }
    private void IncrementWavesTimers()
    {
        for (int i = 0; i < actualWaveRunningTime.Length; i++)
        {
            if (DidAllWavesInSequence(i)) continue;

            if (isUnderWaitTime[i])
            {
                actualWaveWaitedTime[i] += Time.deltaTime;
            }
            else
            {
                actualWaveRunningTime[i] += Time.deltaTime;
            }
        }
    }
    private SpawnPointManager PickRandomSpawnPoint(AlignmentEnum alignment)
    {
        //Try to detect firstly the alignment specific points
        List<SpawnPointManager> alignedPoints = GetSpawnPointsGrouped(alignment);

        //In case no point of specific alignment is detected, try to get generic points
        if (alignedPoints.Count <= 0)
            alignedPoints = GetSpawnPointsGrouped(alignment, true);

        //If no point at all is detected, throw error.
        if (alignedPoints.Count <= 0)
            throw new System.Exception("No spawn points detected for specific alignment [" + alignment.ToString() + "].");

        return alignedPoints.ElementAt(RNG.Int(alignedPoints.Count()));
    }
    private void SpawnAgents()
    {
        for (int i = 0; i < GetMapAlignedWaves().Length; i++)
        {
            actualAgentSpawningIndex = i;

            if (DidAllWavesInSequence(actualAgentSpawningIndex)) continue;

            if (isUnderWaitTime[actualAgentSpawningIndex]) IncrementRunningWave(actualAgentSpawningIndex);

            if (!isRunningSequence[actualAgentSpawningIndex]) return;

            if (!isUnderWaitTime[actualAgentSpawningIndex] && actualWaveRunningTime[actualAgentSpawningIndex] >= GetRunningWave(actualAgentSpawningIndex).spawnTimes.ElementAt(nextIndexToSpawn[actualAgentSpawningIndex]))
            {
                SpawnAgent(actualAgentSpawningIndex);

                IncrementNextIndexToSpawn(actualAgentSpawningIndex);
            }
        }
    }
    private void SpawnAgent(int index)
    {
        GameObject agent = Poolable.TryGetPoolable(GetRunningWave(index).agents.ElementAt(nextIndexToSpawn[index]).prefab, OnRetrievePoolableAgent);

        agent.gameObject.GetComponent<Agent>().DoSpawnFXs();

        spawnedAgents.Add(agent);
    }
    public void OnRetrievePoolableAgent(Poolable agent)
    {
        lastRandomSpawnPointPicked = PickRandomSpawnPoint(GetRunningAlignedWave(actualAgentSpawningIndex).alignment.alignment).transform;

        agent.transform.SetPositionAndRotation(lastRandomSpawnPointPicked.position, lastRandomSpawnPointPicked.rotation);
        agent.gameObject.GetComponent<AIPath>().Teleport(lastRandomSpawnPointPicked.position);
        agent.gameObject.GetComponent<Agent>().Alignment = GetRunningAlignedWave(actualAgentSpawningIndex).alignment.alignment;
    }
    private void IncrementNextIndexToSpawn(int index)
    {
        int newIndexToSpawn = nextIndexToSpawn[index] + 1;

        if (newIndexToSpawn >= GetRunningWave(index).agents.Count())
        {
            IncrementRunningWave(index);
        }
        else
        {
            nextIndexToSpawn[index]++;
        }
    }
    private void IncrementRunningWave(int index)
    {
        if (CanAdvanceWave(index))
        {
            isUnderWaitTime[index] = false;
            nextIndexToSpawn[index] = 0;
            actualWaveRunningTime[index] = 0f;
            actualWaveWaitedTime[index] = 0f;
            wavesHistory[index].Add(runningWave[index], GetRunningWave(index));
            int newRunningWave = runningWave[index] + 1;

            if (newRunningWave >= GetMapAlignedWaves()[index].Length)
            {
                isRunningSequence[index] = false;
            }
            else
            {
                runningWave[index]++;
            }
        }
        else
        {
            isUnderWaitTime[index] = true;
        }
    }

    private bool CanAdvanceWave(int index) { return actualWaveRunningTime[index] >= GetRunningWave(index).spawnTimes.ElementAt(GetRunningWave(index).spawnTimes.Length - 1) && actualWaveWaitedTime[index] >= timeBetweenWaves[index]; }
    private void ResetWavesOperations()
    {
        runningTime = 0f;
        wavesHistory = new Dictionary<int, WaveSO>[GetMapAlignedWaves().Length];
        runningWave = new int[GetMapAlignedWaves().Length];
        nextIndexToSpawn = new int[GetMapAlignedWaves().Length];
        actualWaveRunningTime = new float[GetMapAlignedWaves().Length];
        isUnderWaitTime = new bool[GetMapAlignedWaves().Length];
        isRunningSequence = new bool[GetMapAlignedWaves().Length];
        actualWaveWaitedTime = new float[GetMapAlignedWaves().Length];

        for (int i = 0; i < GetMapAlignedWaves().Length; i++)
        {
            wavesHistory[i] = new Dictionary<int, WaveSO>();
            wavesHistory[i].Clear();
            runningWave[i] = 0;
            nextIndexToSpawn[i] = 0;
            isUnderWaitTime[i] = false;
            actualWaveRunningTime[i] = 0f;
            actualWaveWaitedTime[i] = 0f;
            isRunningSequence[i] = true;
        }
    }
    // Private Methods [END]

    // Public Methods [START]
    public void StartWaves()
    {
        isRunning = true;
    }
    // Public Methods [END]

    // Validation Methods [START]
#if UNITY_EDITOR
    private void After_CollectionChange_TimeBetweenWaves(CollectionChangeInfo info, object value)
    {
        MapManager mapManager = FindObjectOfType<MapManager>();

        if (mapManager.map == null)
            throw new System.Exception("MapManager do not contain a MapSO configured, failed to change [timeBetweenWaves] collection size.");

        switch (info.ChangeType)
        {
            case CollectionChangeType.Insert:
            case CollectionChangeType.Add:
                if (timeBetweenWaves.Count > mapManager.map.alignedWaves.Length)
                    timeBetweenWaves.RemoveAt(timeBetweenWaves.Count - 1);
                break;
            case CollectionChangeType.RemoveIndex:
            case CollectionChangeType.RemoveValue:
                if (timeBetweenWaves.Count < mapManager.map.alignedWaves.Length)
                    timeBetweenWaves.Add(0);
                break;
        }
    }
    private bool Validate_MustHaveCollectionSize_TimeBetweenWaves()
    {
        MapManager mapManager = FindObjectOfType<MapManager>();

        if (mapManager.map == null)
            return true;

        return timeBetweenWaves.Count == mapManager.map.alignedWaves.Length;
    }
    private bool Validate_MustHaveElements_SpawnPoints() { return spawnPoints != null && spawnPoints.Count > 0; }
    private bool Validate_ValidElements_SpawnPoints_Aligned()
    {
        if (spawnPoints == null || spawnPoints.Count == 0) return true;

        MapManager mapManager = FindObjectOfType<MapManager>();

        if (mapManager.map == null)
            return true;

        return mapManager.map.alignmentsOpponents.ToList().All(
            ao =>
                ao.alignment.alignment != mapManager.map.playerAlignment.alignment
                && spawnPoints.Any(sp => sp.alignment == ao.alignment.alignment || sp.alignment == 0)
                || ao.alignment.alignment == mapManager.map.playerAlignment.alignment
        );
    }
#endif
    // Validation Methods [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////