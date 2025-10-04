using UnityEngine;

public class GhostIdle : IGhostState
{
    private float minIdleTime = 0.5f;   // Could be moved out to the behaviour
    private float maxIdleTime = 2f;

    float idleTime;
    float idleTimer;

    void IGhostState.OnStateEnter(GhostBehaviour behaviour)
    {
        behaviour.UpdateAnimatorState("Idle");
        idleTime = Random.Range(minIdleTime, maxIdleTime);
    }

    void IGhostState.OnStateExit(GhostBehaviour behaviour)
    {
    }

    IGhostState IGhostState.Run(GhostBehaviour behaviour)
    {
        if (GhostExtensions.IsPlayerInView(behaviour))
            return GhostStateManager.GetStateOfType(typeof(GhostChase));

        idleTimer += Time.deltaTime;
        if(idleTimer >= idleTime)
            return GhostStateManager.GetStateOfType(typeof(GhostWander));

        return this;
    }
}
