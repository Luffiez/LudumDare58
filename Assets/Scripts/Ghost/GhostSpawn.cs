using UnityEngine;

namespace Assets.Scripts.Ghost
{
    [System.Serializable]
    public class GhostSpawn
    {
        [SerializeField] private GameObject ghostPrefab;
        [SerializeField] private float chance;

        public GameObject GhostPrefab => ghostPrefab;
        public float Chance => chance;
    }
}
