using System;
using UnityEngine;

public class GhostChase : IGhostState
{
    void IGhostState.OnStateEnter(GhostBehaviour behaviour)
    {
        behaviour.UpdateAnimatorState("Chase");
    }

    void IGhostState.OnStateExit(GhostBehaviour behaviour)
    {
    }

    IGhostState IGhostState.Run(GhostBehaviour behaviour)
    {
        if (!GhostExtensions.IsPlayerInRange(behaviour))
            return GhostStateManager.GetStateOfType(typeof(GhostIdle));

        if(behaviour.Target.GetComponent<DummyTarget>()?.State == DummyTarget.DummyState.Sucking)
            return GhostStateManager.GetStateOfType(typeof(GhostFlee));

        GhostExtensions.Move(behaviour, behaviour.Target.position, behaviour.ChaseSpeed);

        return this;
    }
}
