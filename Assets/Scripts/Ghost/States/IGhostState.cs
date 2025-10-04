public interface IGhostState 
{
    public IGhostState Run(GhostBehaviour behaviour);
    public void OnStateEnter(GhostBehaviour behaviour);
    public void OnStateExit(GhostBehaviour behaviour);
}
