using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
using Pathfinding;
using Pathfinding.RVO;
using Core.Math;
using Core.Patterns;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Seeker))]
[RequireComponent(typeof(SimpleSmoothModifier))]
[RequireComponent(typeof(RaycastModifier))]
[RequireComponent(typeof(AIPath))]
[RequireComponent(typeof(RVOController))]
[RequireComponent(typeof(CreatureFsmAi))]
public class Creature : Agent
{
    // Public (Variables) [START]    
    [TitleGroup("Agent Identity/Main Information")]
    [PropertyOrder(1)]
    [Required]
    [ValidateInput("Validate_IsCreature_Agent", "The agent you selected is not a creature.")]    
    public AgentSO agent;
    // Public (Variables) [END]

    // Private (Variables) [START]    
    private AIPath aiPath;
    // Private (Variables) [END]

    // Unity Methods [START]
    protected override void Start()
    {
        base.Start();

        InitializeLinks();
        ResetCreatureStats();
    }
    protected override void Update()
    {
        base.Update();

        HandleCreatureEvolution();
    }
    // Unity Methods [END]

    // Private Methods [START]
    private void InitializeLinks()
    {
        aiPath = GetComponentInChildren<AIPath>(true);
    }
    
    private void ResetCreatureStats()
    {
        if (aiPath != null)
        {
            aiPath.maxSpeed = Velocity;
            aiPath.maxAcceleration = 1f + Velocity * 0.25f;
            aiPath.slowdownDistance = 1f + Velocity;
        }
    }
    private void HandleCreatureEvolution()
    {
        if (CanEvolve && Master == null)
        {
            CreatureEvolutionManager.instance.TryToEvolveCreature(this);
        }
    }
    // Private Methods [END]

    // Public Methods [START]    
    public override AgentSO GetAgent()
    {
        return agent;
    }
    public override void PoolRetrievalAction(Poolable poolable)
    {
        gameObject.GetComponent<AIPath>().enabled = true;
        gameObject.GetComponent<AIPath>().canMove = true;

        base.PoolRetrievalAction(poolable);
    }
    public override void PoolInsertionAction(Poolable poolable)
    {
        gameObject.GetComponent<AIPath>().canMove = false;
        gameObject.GetComponent<AIPath>().enabled = false;

        base.PoolInsertionAction(poolable);
    }
    // Public Methods [END]

    // Validation Methods [START]
    private bool Validate_IsCreature_Agent() { return agent != null && agent.type == AgentTypeEnum.CREATURE; }
    // Validation Methods [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////