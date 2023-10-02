using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Pathfinding;
using Sirenix.OdinInspector;
using Core.Math;

public class CreatureFsmAi : AgentFsmAi
{
    // Private (Variables) [START]
    private Creature creature;
    // Private (Variables) [END]

    // Protected (Variables) [START]
    [BoxGroup("Agent FSM Identity")]
    [ShowInInspector]
    [HideInEditorMode]
    [ReadOnly]
    protected AIPath pathfinding;
    [BoxGroup("Agent FSM Identity")]
    [ShowInInspector]
    [HideInEditorMode]
    [ReadOnly]
    protected bool isGettingDistanceFromTarget = false;
    // Protected (Variables) [END]

    // Public (Properties) [START]
    public bool IsCreatureParalyzed { get { return agent.IsAgentUnderStatusParalyze; } }
    public bool IsCreatureSleeping { get { return agent.IsAgentUnderStatusAsleep; } }
    public bool IsCreatureGrounded { get { return agent.IsAgentUnderStatusGrounded; } }
    public bool IsCreatureTaunted { get { return agent.IsAgentUnderStatusTaunt; } }
    public bool IsCreatureDrowning { get { return agent.IsAgentUnderStatusDrown; } }
    public bool IsCreatureConfused { get { return agent.IsAgentUnderStatusConfusion; } }
    // Public (Properties) [END]

    // (Unity) Methods [START]
    protected override void OnEnable()
    {
        base.OnEnable();
        creature = agent.GetComponent<Creature>();

        pathfinding = AgentGOBJ.GetComponent<AIPath>();
        pathfinding.destination = transform.position;

        currentState = new FSMStateCreatureIdle(Anim, creature, pathfinding);
    }
    protected override void Update()
    {
        base.Update();

        if (GameManager.instance.IsRunningAndNotPaused)
        {
            UpdateWalkAnimation();

            UpdateAIDestination();

            UpdateAIGoal();

            UpdateAIPathfindingMinimumDistance();
        }
    }
    // (Unity) Methods [END]

    // Private (Methods) [START]
    private void UpdateWalkAnimation()
    {
        if (IsAgentDead || !IsMovable || IsMovementPrevented)
            return;

        if (pathfinding != null && currentState.name == AgentStateEnum.WALK)
        {
            Vector3 relVelocity = transform.InverseTransformDirection(pathfinding.velocity);
            relVelocity.y = 0f;

            float maxAnimationVelocity = pathfinding.maxSpeed < 5f ? 1f : 2f;

            Anim.SetFloat("walkSpeed", Mathf.Clamp(relVelocity.magnitude / Anim.transform.lossyScale.x, 0f, maxAnimationVelocity));
        }
    }
    private void UpdateAIDestination()
    {
        if (IsAgentDead || !IsMovable || IsMovementPrevented || IsCreatureConfused)
            return;

        if (pathfinding != null)
        {
            List<PriorityGoal> creaturePriorityEnemies = agent.GetAgentViablePriorityEnemies();

            if (creaturePriorityEnemies.Count > 0 && IsAggressive)
            {
                    PriorityGoal nearestPriorityEnemy = agent.GetAgentNearestViablePriorityEnemy();

                    Agent enemyAgent = nearestPriorityEnemy.goal.GetComponent<Agent>();
                    if (enemyAgent == null)
                        enemyAgent = nearestPriorityEnemy.goal.GetComponentInParent<Agent>();
                    if (enemyAgent == null)
                        enemyAgent = nearestPriorityEnemy.goal.GetComponentInChildren<Agent>();

                    if (!IsMakingAnyAttack() && !IsAllAttacksUnderCooldown)
                    {
                        TimedAttack ta = GetNearestNotInCooldownAttack();

                        Vector3 directionBetweenEnemy = (nearestPriorityEnemy.goal.transform.position - transform.position).normalized;

                        Vector3 destination = nearestPriorityEnemy.goal.transform.position + (-directionBetweenEnemy * ta.attack.minimumAttackDistance);

                        isGettingDistanceFromTarget = true;

                        pathfinding.destination = destination;
                    }
                    else
                    {
                        isGettingDistanceFromTarget = false;

                        if (enemyAgent != null)
                            pathfinding.destination = GetGoalDestination(enemyAgent.mainCollider.bounds);
                        else
                            pathfinding.destination = nearestPriorityEnemy.goal.transform.position;
                    }
            }
            else if (agent.MainGoals.Count > 0)
            {
                isGettingDistanceFromTarget = false;
                
                Collider goalCollider = agent.MainGoals.First().goal.mainCollider;

                if (goalCollider != null)
                {
                    pathfinding.destination = GetGoalDestination(goalCollider.bounds);
                }
                else
                    pathfinding.destination = agent.MainGoals.First().goal.transform.position;
            }
        }
    }
    private void UpdateAIGoal()
    {
        if (IsAgentDead || IsCreatureConfused || IsCreatureTaunted)
            return;

        if (IsAttackPrevented)
        {
            agent.ActualGoal = null;

            return;
        }

        List<PriorityGoal> creaturePriorityEnemies = agent.GetAgentViablePriorityEnemies();

        if (creaturePriorityEnemies.Count > 0 && IsAggressive)
        {
            PriorityGoal nearestPriorityEnemy = agent.GetAgentNearestViablePriorityEnemy();

            agent.ActualGoal = nearestPriorityEnemy.goal;

            return;
        }
        
        if (agent.MainGoals.Count > 0)
        {
            agent.ActualGoal = agent.MainGoals.First().goal;
        }
    }
    private void UpdateAIPathfindingMinimumDistance()
    {
        if (IsAgentDead)
            return;

        if (pathfinding != null)
        {
            if (isGettingDistanceFromTarget)
            {
                pathfinding.endReachedDistance = 1f;
            }
            else
            {
                TimedAttack nearestAttack = GetNearestNotInCooldownAttack();

                if (nearestAttack.attack != null)
                    pathfinding.endReachedDistance = nearestAttack.attack.maximumAttackDistance * 0.9f;
            }
        }
    }
    private Vector3 GetGoalDestination(Bounds b)
    {
        Vector3 result = b.min;
        Vector3 aux;
        
        aux = new Vector3(b.max.x, 0f, b.max.z);
        if (Vector3.Distance(gameObject.transform.position, result) > Vector3.Distance(gameObject.transform.position, aux))
            result = aux;

        aux = new Vector3(b.max.x, 0f, b.min.z);
        if (Vector3.Distance(gameObject.transform.position, result) > Vector3.Distance(gameObject.transform.position, aux))
            result = aux;

        aux = new Vector3(b.min.x, 0f, b.max.z);
        if (Vector3.Distance(gameObject.transform.position, result) > Vector3.Distance(gameObject.transform.position, aux))
            result = aux;

        return result;
    }
    // Private (Methods) [END]

    // Protected (Methods) [START]
    protected override void GoToIdleState()
    {
        currentState = new FSMStateCreatureIdle(Anim, creature, pathfinding);
    }
    // Protected (Methods) [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////