using System;

public class GhostWander : IGhostState
{
    void IGhostState.OnStateEnter(GhostBehaviour behaviour)
    {
    }

    void IGhostState.OnStateExit(GhostBehaviour behaviour) 
    {
    }

    IGhostState IGhostState.Run(GhostBehaviour behaviour)
    {
        return this;
    }
}
