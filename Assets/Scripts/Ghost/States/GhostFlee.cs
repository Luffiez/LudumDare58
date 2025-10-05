using System;
using UnityEngine;

namespace Assets.Scripts.Ghost
{
    public class GhostFlee : IGhostState
    {
        void IGhostState.OnStateEnter(GhostBehaviour behaviour)
        {
            behaviour.PlayAnimation("Flee");
            behaviour.PlaySuctionParticles();

            behaviour.MoveBurst(-GhostExtensions.GetDirectionToTarget(behaviour));
        }

        void IGhostState.OnStateExit(GhostBehaviour behaviour)
        {
            behaviour.StopSuctionParticles();
        }

        IGhostState IGhostState.Run(GhostBehaviour behaviour)
        {
            if (!GhostExtensions.IsTargetInRange(behaviour))
                return GhostStateManager.GetStateOfType(typeof(GhostIdle));

            Vector3 direction = -GhostExtensions.GetDirectionToTarget(behaviour);

            if (!GhostExtensions.WillHitWall(behaviour, direction))
                GhostExtensions.Move(behaviour, behaviour.transform.position + direction, behaviour.FleeSpeed);

            return this;
        }

    }
}

