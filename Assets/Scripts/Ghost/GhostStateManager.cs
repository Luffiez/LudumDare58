using System;
using System.Xml.Serialization;

namespace Assets.Scripts.Ghost
{
    public static class GhostStateManager
    {
        public static IGhostState[] States = new IGhostState[5]
        {
                new GhostIdle(),
                new GhostWander(),
                new GhostChase(),
                new GhostFlee(),
                new GhostDie()
        };

        public static void Run(this GhostBehaviour behaviour)
        {
            IGhostState currentState = behaviour.State;
            IGhostState nextState = currentState.Run(behaviour);
            if (nextState != currentState)
            {
                currentState.OnStateExit(behaviour);
                nextState.OnStateEnter(behaviour);
            }
            behaviour.UpdateState(nextState);
        }

        public static void RunAt(this GhostBehaviour behaviour, IGhostState state)
        {
            IGhostState currentState = state;
            behaviour.State.OnStateExit(behaviour);
            currentState.OnStateEnter(behaviour);

            IGhostState nextState = currentState.Run(behaviour);
            if (nextState != currentState)
            {
                currentState.OnStateExit(behaviour);
                nextState.OnStateEnter(behaviour);
            }
            behaviour.UpdateState(nextState);
        }
    }
}
