using UnityEngine;

namespace Assets.Scripts.Ghost
{
    public class GhostFlee : IGhostState
    {
        float ghostChaseTime = 1f;
        float ghostChaseTimer;

        void IGhostState.OnStateEnter(GhostBehaviour behaviour)
        {
            behaviour.PlayAnimation("Flee");
            behaviour.PlaySuctionParticles();
            ghostChaseTimer = 0;
            //behaviour.MoveBurst(GhostExtensions.GetDirectionFromTarget(behaviour));
        }

        void IGhostState.OnStateExit(GhostBehaviour behaviour)
        {
            behaviour.StopSuctionParticles();
        }

        IGhostState IGhostState.Run(GhostBehaviour behaviour)
        {
             if (!GhostExtensions.IsTargetInRange(behaviour))
                return GhostStateManager.GetStateOfType(typeof(GhostIdle));

            float speedModifier = 1;
            // Player in range
            if (behaviour.IsAttacked())
            {
                // reset timer when attacked
                ghostChaseTimer = 0;
            }
            else
            {
                if (ghostChaseTimer >= ghostChaseTime)
                    return GhostStateManager.GetStateOfType(typeof(GhostChase));
                else
                {
                    ghostChaseTimer += Time.deltaTime;
                    speedModifier = 1.5f;
                }
            }


            if (!GhostExtensions.WillHitWall(behaviour, GhostExtensions.GetDirectionToTarget(behaviour)))
            {
                Vector3 direction = GhostExtensions.GetDirectionFromTarget(behaviour);
                GhostExtensions.Move(behaviour, behaviour.transform.position + direction, behaviour.FleeSpeed * speedModifier);
            }

            return this;
        }

    }
}

