using System;
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
        // Create a new instance of the requested state type
        // Not performant, but reusing instances leads to randomness issues
        // Could be optimized by settings upp states per ghost in start
        if (typeof(IGhostState).IsAssignableFrom(state))
            return Activator.CreateInstance(state) as IGhostState;
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
        }
        behaviour.UpdateState(nextState);
    }
}
