using UnityEngine;

public static class GhostExtensions
{
    public static bool IsPlayerInRange(this GhostBehaviour behaviour)
    {
        float distanceToPlayer = Vector2.Distance(behaviour.transform.position, behaviour.Target.position);
        return distanceToPlayer <= behaviour.detectionRange;
    }

    public static bool IsPlayerInView(this GhostBehaviour behaviour)
    {
        Vector2 directionToPlayer = (behaviour.Target.position - behaviour.transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(behaviour.transform.position, directionToPlayer, behaviour.detectionRange);
        return hit.collider != null && hit.collider.CompareTag("Player");
    }

    internal static void Move(GhostBehaviour behaviour, Vector3 position, float speed)
    {
        Vector2 direction = (position - behaviour.transform.position).normalized;
        behaviour.RigidBody.AddForce(direction * speed);
    }
}
