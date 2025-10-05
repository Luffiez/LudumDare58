using Assets.Scripts.Ghost;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Ghost
{
    public static class GhostExtensions
    {
        public static HashSet<GhostBehaviour> GhostsBeingAttacked = new();
        static int boundsLayer = 6;

        public static bool IsTargetInRange(this GhostBehaviour behaviour)
        {
            float distanceToPlayer = Vector2.Distance(behaviour.transform.position, behaviour.Target.position);
            return distanceToPlayer <= behaviour.detectionRange;
        }

        public static bool IsTargetInView(this GhostBehaviour behaviour)
        {
            Vector2 directionToPlayer = (behaviour.Target.position - behaviour.transform.position).normalized;
            RaycastHit2D[] hits = Physics2D.RaycastAll(behaviour.transform.position, directionToPlayer, behaviour.detectionRange);

            foreach (var hit in hits)
            {
                if (hit.collider.CompareTag("Player"))
                    return true;
            }
            return false;
        }

        internal static void Move(GhostBehaviour behaviour, Vector3 position, float speed) => 
            Move(behaviour, GetDirectionToTarget(behaviour), speed);

        internal static void Move(GhostBehaviour behaviour, Vector2 direction, float speed) => 
            behaviour.RigidBody.AddForce(direction * speed * Time.deltaTime);

        public static bool WillHitWall(GhostBehaviour behaviour, Vector2 direction)
        {
            RaycastHit2D hit = Physics2D.Raycast(behaviour.transform.position, direction.normalized, behaviour.WallDistanceCheck, behaviour.BoundsLayer);
            Debug.DrawLine(behaviour.transform.position, (Vector2)behaviour.transform.position + direction.normalized * behaviour.WallDistanceCheck, hit.collider != null ? Color.red : Color.white);
            return hit.collider != null;
        }

        public static bool IsAttacked(this GhostBehaviour behaviour) => 
            GhostsBeingAttacked.Contains(behaviour);


        public static Vector2 GetDirectionToTarget(GhostBehaviour behaviour) =>
            (behaviour.Target.position - behaviour.transform.position).normalized;

        public static Vector2 GetDirectionFromTarget(GhostBehaviour behaviour) =>
            (behaviour.transform.position - behaviour.Target.position).normalized;
    }
}
