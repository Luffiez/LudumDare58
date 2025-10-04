using System;
using UnityEngine;

public static class  GhostStateManager
{
    public static IGhostState[] States = new IGhostState[5]
    {
        new GhostIdle(),
        new GhostWander(),
        new GhostChase(),
        new GhostFlee(),
        new GhostDie()
    };

    public static IGhostState GetStateOfType(Type state)
    {
        foreach(IGhostState s in States)
            if(s.GetType() == state)
                return s;
        return default;
    }

    public static void Run(this GhostBehaviour behaviour)
    {
        IGhostState currentState = behaviour.State;
        IGhostState nextState = currentState.Run(behaviour);
        if(nextState != currentState)
        {
            currentState.OnStateExit(behaviour);
            nextState.OnStateEnter(behaviour);
            Debug.Log($"{behaviour.name} changed state: {currentState} -> {nextState}");
        }
        behaviour.UpdateState(nextState);
    }
}
