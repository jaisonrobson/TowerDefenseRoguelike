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
    /// Did the player pressed the start button?
    /// </summary>
    [SerializeField, HideInEditorMode, DisableInPlayMode]
    private bool isRunning = false;

    /// <summary>
    /// The time since the player pressed the start button until the last monster of the last wave is killed.
    /// </summary>
    [SerializeField, HideInEditorMode, DisableInPlayMode]
    private float runningTime = 0f;

    /// <summary>
    /// The index on the map waves of the actual wave that is running
    ///
    /// </summary>
    [SerializeField, HideInEditorMode, DisableInPlayMode]
    private int runningWave;

    /// <summary>
    /// The time since the start of the actual wave.
    /// This gets reset at every new wave.
    /// 
    /// </summary>
    [SerializeField, HideInEditorMode, DisableInPlayMode]
    private float actualWaveRunningTime;

    /// <summary>
    /// This stores the next index on the wave of the agent and the spawn time of the next agent that will be spawned at the running wave.
    /// 
    /// * There is a different field for each simultaneous wave sequence.
    /// </summary>
    [SerializeField, HideInEditorMode, DisableInPlayMode]
    private int agentGroupIndexToSpawn;

    private List<GameObject> spawnedAgents;

    private int finalizedFogOneWaves;
    private int finalizedFogTwoWaves;
    private int finalizedFogThreeWaves;

    [SerializeField, HideInEditorMode, DisableInPlayMode]
    private List<WaveSO> fogOneWaves;
    [SerializeField, HideInEditorMode, DisableInPlayMode]
    private List<WaveSO> fogTwoWaves;
    [SerializeField, HideInEditorMode, DisableInPlayMode]
    private List<WaveSO> fogThreeWaves;



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
    private void HandleWavesOperation()
    {
        if (!isRunning) return;

        if (DidAllWaves())
            isRunning = false;
    }
    private bool DidAllWaves() { return DidAllWavesFogOne() && DidAllWavesFogTwo() && DidAllWavesFogThree(); }
    private bool DidAllWavesFogOne() => finalizedFogOneWaves >= fogOneWaves.Count;
    private bool DidAllWavesFogTwo() => finalizedFogTwoWaves >= fogTwoWaves.Count;
    private bool DidAllWavesFogThree() => finalizedFogThreeWaves >= fogThreeWaves.Count;
    private WaveSO GetRunningWave()
    {
        switch (FogManager.instance.ActualFogDiscoveryStage)
        {
            case 1:
                return fogOneWaves.ElementAt(runningWave);
            case 2:
                return fogTwoWaves.ElementAt(runningWave);
            case 3:
                return fogThreeWaves.ElementAt(runningWave);
        }

        return null;
    }
    private AlignedWaveSO GetRunningAlignedWave()
    {
        int waveCounter = 0;
        AlignedWaveSO result = null;

        switch (FogManager.instance.ActualFogDiscoveryStage)
        {
            case 1:
                MapManager.instance.map.fogOneWaves.ToList().ForEach(aw =>
                {
                    if (waveCounter == runningWave)
                    {
                        result = aw;
                        return;
                    }
                    
                    aw.waves.ToList().ForEach(wave =>
                    {
                        if (waveCounter == runningWave)
                        {
                            result = aw;

                            return;
                        }
                        else
                            waveCounter++;
                    });
                });
                break;
            case 2:
                MapManager.instance.map.fogTwoWaves.ToList().ForEach(aw =>
                {
                    if (waveCounter == runningWave)
                    {
                        result = aw;
                        return;
                    }

                    aw.waves.ToList().ForEach(wave =>
                    {
                        if (waveCounter == runningWave)
                        {
                            result = aw;

                            return;
                        }
                        else
                            waveCounter++;
                    });
                });
                break;
            case 3:
                MapManager.instance.map.fogThreeWaves.ToList().ForEach(aw =>
                {
                    if (waveCounter == runningWave)
                    {
                        result = aw;
                        return;
                    }

                    aw.waves.ToList().ForEach(wave =>
                    {
                        if (waveCounter == runningWave)
                        {
                            result = aw;

                            return;
                        }
                        else
                            waveCounter++;
                    });
                });
                break;
        }

        return result;
    }

    private int GetFogWavesCount()
    {
        int result = 0;
        switch (FogManager.instance.ActualFogDiscoveryStage)
        {
            case 1:
                return fogOneWaves.Count;
            case 2:
                return fogTwoWaves.Count;
            case 3:
                return fogThreeWaves.Count;
        }
        return result;
    }
    private void IncrementTimers()
    {
        IncrementTotalTimer();
        IncrementWavesTimers();
    }
    private void IncrementTotalTimer() { runningTime += Time.deltaTime; }
    private void IncrementWavesTimers()
    {
        actualWaveRunningTime += Time.deltaTime;
    }
    private void SpawnAgents()
    {
        if (actualWaveRunningTime >= GetRunningWave().spawnTimes.ElementAt(agentGroupIndexToSpawn))
        {
            SpawnAgent();
            IncrementNextAgentGroupIndexToSpawn();
        }
    }
    private void SpawnAgent()
    {
        GetRunningWave().agentsGroup.ElementAt(agentGroupIndexToSpawn).agents.ToList().ForEach(agt =>
        {
            GameObject agent = Poolable.TryGetPoolable(agt.prefab, OnRetrievePoolableAgent);

            agent.gameObject.GetComponent<Agent>().DoSpawnFXs();

            spawnedAgents.Add(agent);
        });
    }
    public void OnRetrievePoolableAgent(Poolable agent)
    {
        Vector3 spawnPoint = FogManager.instance.GenerateRandomSpawnPosition();

        agent.transform.position = spawnPoint;
        agent.gameObject.GetComponent<AIPath>().Teleport(spawnPoint);
        agent.gameObject.GetComponent<Agent>().Alignment = GetRunningAlignedWave().alignment.alignment;
    }
    private void IncrementNextAgentGroupIndexToSpawn()
    {
        int nextAgentGroupIndexToSpawn = agentGroupIndexToSpawn + 1;

        if (nextAgentGroupIndexToSpawn >= GetRunningWave().agentsGroup.Length)
            IncrementRunningWave();
        else
            agentGroupIndexToSpawn++;
    }
    private void IncrementRunningWave()
    {
        agentGroupIndexToSpawn = 0;
        actualWaveRunningTime = 0f;
        runningWave++;
        
        if (runningWave >= GetFogWavesCount())
        {
            runningWave = 0;

            if (FogManager.instance.ActualFogDiscoveryStage < 3)
                FogManager.instance.DiscoverNewFog();
            else
                isRunning = false;
        }
    }

    private void ResetWavesOperations()
    {
        runningTime = 0f;
        runningWave = 0;
        agentGroupIndexToSpawn = 0;
        actualWaveRunningTime = 0f;
        finalizedFogOneWaves = 0;
        finalizedFogTwoWaves = 0;
        finalizedFogThreeWaves = 0;

        ResetFogOneWaves();
        ResetFogTwoWaves();
        ResetFogThreeWaves();
    }
    private void ResetFogOneWaves()
    {
        fogOneWaves = new List<WaveSO>();

        MapManager.instance.map.fogOneWaves.ToList().ForEach(alignedWaves => fogOneWaves.AddRange(alignedWaves.waves.ToList()));
    }
    private void ResetFogTwoWaves()
    {
        fogTwoWaves = new List<WaveSO>();

        MapManager.instance.map.fogTwoWaves.ToList().ForEach(alignedWaves => fogTwoWaves.AddRange(alignedWaves.waves.ToList()));
    }
    private void ResetFogThreeWaves()
    {
        fogThreeWaves = new List<WaveSO>();

        MapManager.instance.map.fogThreeWaves.ToList().ForEach(alignedWaves => fogThreeWaves.AddRange(alignedWaves.waves.ToList()));
    }
    // Private Methods [END]

    // Public Methods [START]
    public void StartWaves()
    {
        FogManager.instance.StartFog();

        isRunning = true;
    }
    // Public Methods [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////