using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Ghost
{
    public class StateManager : MonoBehaviour
    {
        public static StateManager Instance { get; private set; }

        List<GhostBehaviour> ghosts = new List<GhostBehaviour>();

        public delegate void GhostChanged(GhostBehaviour ghost);
        public event GhostChanged OnGhostAdded;
        public event GhostChanged OnGhostRemoved;


        private void Awake()
        {
            Application.targetFrameRate = 60;
            if (Instance != null && Instance != this)
             {
                 Destroy(this);
             }
             else
             {
                 Instance = this;
            }
        }
        public void RegisterGhost(GhostBehaviour ghost)
        {
            if (!ghosts.Contains(ghost))
            {
                ghosts.Add(ghost);
                OnGhostAdded?.Invoke(ghost);
            }
        }

        public void UnregisterGhost(GhostBehaviour ghost)
        {
            if (ghosts.Contains(ghost))
            {
                if (ghosts.Remove(ghost))
                    OnGhostRemoved?.Invoke(ghost);
            }
        }

        private void Update()
        {
            UpdateGhostStates();
        }

        private void UpdateGhostStates()
        {
            foreach (var ghost in ghosts)
                if (ghost != null && ghost.enabled)
                    GhostStateManager.Run(ghost);
        }
    }
}
