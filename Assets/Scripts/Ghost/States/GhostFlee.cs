using System;
using UnityEngine;

public class GhostFlee : IGhostState
{
    void IGhostState.OnStateEnter(GhostBehaviour behaviour)
    {
    }

    void IGhostState.OnStateExit(GhostBehaviour behaviour)
    {
    }

    IGhostState IGhostState.Run(GhostBehaviour behaviour)
    {
        if(!GhostExtensions.IsPlayerInRange(behaviour))
            return GhostStateManager.GetStateOfType(typeof(GhostIdle));

        Vector3 direction = (behaviour.transform.position - behaviour.Target.position).normalized;
        GhostExtensions.Move(behaviour, behaviour.transform.position + direction, behaviour.FleeSpeed * Time.deltaTime);

        return this;
    }
}
