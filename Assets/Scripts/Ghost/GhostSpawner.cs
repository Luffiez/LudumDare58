using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Ghost
{
    public class GhostSpawner : MonoBehaviour
    {
        [SerializeField] private int maxGhosts = 5;
        [SerializeField] private GameObject ghostPrefab;
        [SerializeField] private Collider2D spawnArea;

        [SerializeField] private int increasePerWave;
        [SerializeField] private float waveInterval;

        List<GameObject> ghosts = new();
        int rounds = 1;

        private void Start()
        {
            StateManager.Instance.OnGhostRemoved += Instance_OnGhostRemoved;
        }

        private void Instance_OnGhostRemoved(GhostBehaviour ghost)
        {
            // Could use pooling system instead.
            ghosts.Remove(ghost.gameObject);
            GameObject.Destroy(ghost.gameObject);
        }

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

            UpdateSpawnTimerProgress();
        }

        private void UpdateSpawnTimerProgress()
        {
            if (GameManager.Instance.Timer > rounds * waveInterval)
            {
                rounds++;
                maxGhosts += increasePerWave;
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
