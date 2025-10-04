public class GhostDie : IGhostState
{
    void IGhostState.OnStateEnter(GhostBehaviour behaviour)
    {
        behaviour.PlayAnimation("Die");
    }

    void IGhostState.OnStateExit(GhostBehaviour behaviour)
    {
    }

    IGhostState IGhostState.Run(GhostBehaviour behaviour)
    {
        return this;
    }
}
