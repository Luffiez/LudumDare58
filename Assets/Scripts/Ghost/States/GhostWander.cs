using System;
using UnityEngine;

namespace Assets.Scripts.Ghost
{
    public class GhostWander : IGhostState
    {
        private float directionChangeInterval = 2f;
        private float timeSinceLastChange = 0f;
        private Vector2 currentDirection = Vector2.zero;
        private System.Random instanceRandom;
        private float timeOffset;


        void IGhostState.OnStateEnter(GhostBehaviour behaviour)
        {
            behaviour.PlayAnimation("Wander");
            // Use ghost's instance ID to seed the random generator for unique movement
            instanceRandom = new System.Random(behaviour.GetInstanceID());
            // Add a random offset to desynchronize direction changes
            timeOffset = (float)instanceRandom.NextDouble() * directionChangeInterval;
            timeSinceLastChange = timeOffset;
            PickNewDirection(behaviour);
        }

        void IGhostState.OnStateExit(GhostBehaviour behaviour)
        {
        }

        IGhostState IGhostState.Run(GhostBehaviour behaviour)
        {
            if (GhostExtensions.IsTargetInRange(behaviour) && GhostExtensions.IsTargetInView(behaviour))
                return GhostStateManager.GetStateOfType(typeof(GhostChase));

            timeSinceLastChange += Time.deltaTime;

            if (timeSinceLastChange >= directionChangeInterval)
            {
                PickNewDirection(behaviour);
                timeSinceLastChange = 0f;
            }
            else if (IsBlocked(behaviour))
            {
                PickNewDirection(behaviour);
            }

            GhostExtensions.Move(behaviour, currentDirection, behaviour.Speed);
            return this;
        }

        private void PickNewDirection(GhostBehaviour behaviour)
        {
            Vector2[] directions = {
                Vector2.up, Vector2.down, Vector2.left, Vector2.right,
                new Vector2(1,1).normalized, new Vector2(-1,1).normalized,
                new Vector2(1,-1).normalized, new Vector2(-1,-1).normalized
            };

            for (int i = 0; i < directions.Length; i++)
            {
                Vector2 dir = directions[instanceRandom.Next(directions.Length)];
                if (!GhostExtensions.WillHitWall(behaviour, currentDirection))
                {
                    currentDirection = dir;
                    return;
                }
            }
            currentDirection = Vector2.zero;
        }

        private bool IsBlocked(GhostBehaviour behaviour)
        {
            return GhostExtensions.WillHitWall(behaviour, currentDirection);
        }
    }
}
