using UnityEngine;

namespace Assets.Scripts.Ghost
{
    public class GhostIdle : IGhostState
    {
        private float minIdleTime = 0.5f;   // Could be moved out to the behaviour
        private float maxIdleTime = 2f;

        float idleTime;
        float idleTimer;

        void IGhostState.OnStateEnter(GhostBehaviour behaviour)
        {
            behaviour.PlayAnimation("Idle");
            idleTime = Random.Range(minIdleTime, maxIdleTime);
        }

        void IGhostState.OnStateExit(GhostBehaviour behaviour)
        {
        }

        IGhostState IGhostState.Run(GhostBehaviour behaviour)
        {
            if (GhostExtensions.IsTargetInRange(behaviour) && GhostExtensions.IsTargetInView(behaviour))
                return new GhostChase();

            idleTimer += Time.deltaTime;
            if (idleTimer >= idleTime)
                return new GhostWander();

            return this;
        }
    }
}
