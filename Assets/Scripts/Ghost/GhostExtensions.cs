using UnityEngine;

public static class GhostExtensions
{
    private static LayerMask playerLayer = LayerMask.NameToLayer("Player");
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

    internal static void Move(GhostBehaviour behaviour, Vector3 position, float speed)
    {
        Vector2 direction = (position - behaviour.transform.position).normalized;
        Move(behaviour, direction, speed);
    }

    internal static void Move(GhostBehaviour behaviour, Vector2 direction, float speed)
    {
        behaviour.RigidBody.AddForce(direction * speed * Time.deltaTime);
    }
}
