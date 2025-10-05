using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Ghost
{
    public class GhostSpawner : MonoBehaviour
    {
        [SerializeField] private int maxGhosts = 5;
        [SerializeField] private GameObject ghostPrefab;
        [SerializeField] private Collider2D spawnArea;

        List<GameObject> ghosts = new();

        private void Update()
        {
            if (ghosts.Count > maxGhosts)
            {
                Destroy(ghosts[0]);
                ghosts.RemoveAt(0);
            }
            else if (ghosts.Count < maxGhosts)
            {
                SpawnGhost();
            }
        }

        internal void SpawnGhost()
        {
            Vector2 position = new(
                Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
                Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y)
            );
            ghosts.Add(Instantiate(ghostPrefab, position, Quaternion.identity));
        }
    }
}
