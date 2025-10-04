public class GhostIdle : IGhostState
{
    void IGhostState.OnStateEnter(GhostBehaviour behaviour)
    {
    }

    void IGhostState.OnStateExit(GhostBehaviour behaviour)
    {
    }

    IGhostState IGhostState.Run(GhostBehaviour behaviour)
    {
        if (GhostExtensions.IsPlayerInRange(behaviour))
            return GhostStateManager.GetStateOfType(typeof(GhostChase));
        return this;
    }
}
