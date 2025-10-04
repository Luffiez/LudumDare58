using UnityEngine;

public static class GhostExtensions
{
    public static bool IsPlayerInRange(this GhostBehaviour behaviour)
    {
        float distanceToPlayer = Vector2.Distance(behaviour.transform.position, behaviour.Target.position);
        return distanceToPlayer <= behaviour.detectionRange;
    }

    internal static void Move(GhostBehaviour behaviour, Vector3 position, float speed)
    {
        Vector2 direction = (position - behaviour.transform.position).normalized;
        behaviour.RigidBody.AddForce(direction * speed);
    }
}
