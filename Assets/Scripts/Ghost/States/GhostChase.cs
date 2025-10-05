using System;
using UnityEngine;

namespace Assets.Scripts.Ghost
{
    public class GhostChase : IGhostState
    {
        void IGhostState.OnStateEnter(GhostBehaviour behaviour)
        {
            behaviour.PlayAnimation("Chase");
        }

        void IGhostState.OnStateExit(GhostBehaviour behaviour)
        {
        }

        IGhostState IGhostState.Run(GhostBehaviour behaviour)
        {
            if (!GhostExtensions.IsTargetInRange(behaviour))
                return new GhostIdle();

            if (behaviour.IsAttacked())
                return new GhostFlee();

            if (GhostExtensions.WillHitWall(behaviour, GhostExtensions.GetDirectionToTarget(behaviour)))
            {
                GhostExtensions.Move(behaviour, behaviour.Target.position, -behaviour.ChaseSpeed);
                return this;
            }

            GhostExtensions.Move(behaviour, behaviour.Target.position, behaviour.ChaseSpeed);

            return this;
        }
    }
}