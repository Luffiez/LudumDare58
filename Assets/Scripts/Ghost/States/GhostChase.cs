using System;
using UnityEngine;

public class GhostChase : IGhostState
{
    void IGhostState.OnStateEnter(GhostBehaviour behaviour)
    {
        behaviour.PlayAnimation("Chase");
    }

    void IGhostState.OnStateExit(GhostBehaviour behaviour)
    {
    }

    IGhostState IGhostState.Run(GhostBehaviour behaviour)
    {
        if (!GhostExtensions.IsTargetInRange(behaviour))
            return GhostStateManager.GetStateOfType(typeof(GhostIdle));

        if(behaviour.IsAttacked())
            return GhostStateManager.GetStateOfType(typeof(GhostFlee));

        GhostExtensions.Move(behaviour, behaviour.Target.position, behaviour.ChaseSpeed);

        return this;
    }
}
