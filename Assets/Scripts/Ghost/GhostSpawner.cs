using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace Assets.Scripts.Ghost
{

    public class GhostSpawner : MonoBehaviour
    {
        [SerializeField] private int maxGhosts = 5;
        [SerializeField] private float increasePerWave;
        [SerializeField] private float waveInterval;
        [SerializeField] private Collider2D spawnArea;
        [SerializeField] private float spawnInterval = 0.5f;
        [SerializeField] private float spawnIntervalDeviation = 0.15f;

        [Space]
        [SerializeField] private GhostSpawn[] prefabs;

        List<GameObject> ghosts = new();
        int rounds = 1;

        float spawnTimer;

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
                if(spawnTimer >= 0)
                {
                    spawnTimer -= Time.deltaTime;
                    return;
                }

                SpawnGhost();
                spawnTimer = spawnInterval + Random.Range(-spawnIntervalDeviation, spawnIntervalDeviation);
            }

            UpdateSpawnTimerProgress();
        }

        private void UpdateSpawnTimerProgress()
        {
            if (GameManager.Instance.Timer > rounds * waveInterval)
            {
                rounds++;
                maxGhosts = Mathf.RoundToInt(maxGhosts * increasePerWave);
            }
        }

        internal void SpawnGhost()
        {
            Vector2 position = new(
                Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
                Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y)
            );

            GameObject ghostPrefab = GetWeightedRandomPrefab();
            ghosts.Add(Instantiate(ghostPrefab, position, Quaternion.identity));
        }

        private GameObject GetWeightedRandomPrefab()
        {
            // Weighted random selection based on Chance
            float totalChance = 0f;
            foreach (var spawn in prefabs)
                totalChance += spawn.Chance;

            float randomValue = Random.Range(0f, totalChance);
            float cumulative = 0f;
            GameObject ghostPrefab = prefabs[0].GhostPrefab;
            foreach (var spawn in prefabs)
            {
                cumulative += spawn.Chance;
                if (randomValue <= cumulative)
                {
                    ghostPrefab = spawn.GhostPrefab;
                    break;
                }
            }

            return ghostPrefab;
        }
    }
}
