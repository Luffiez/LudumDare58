using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Ghost
{
    public class StateManager : MonoBehaviour
    {
        public static StateManager Instance { get; private set; }

        List<GhostBehaviour> ghosts = new List<GhostBehaviour>();


        private void Awake()
        {
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
                ghosts.Add(ghost);
        }

        public void UnregisterGhost(GhostBehaviour ghost)
        {
            if (ghosts.Contains(ghost))
                ghosts.Remove(ghost);
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
