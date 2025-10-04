public class GhostDie : IGhostState
{
    void IGhostState.OnStateEnter(GhostBehaviour behaviour)
    {
        behaviour.UpdateAnimatorState("Die");
    }

    void IGhostState.OnStateExit(GhostBehaviour behaviour)
    {
    }

    IGhostState IGhostState.Run(GhostBehaviour behaviour)
    {
        return this;
    }
}
