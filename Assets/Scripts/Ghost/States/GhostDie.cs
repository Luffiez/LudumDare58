using System;

namespace Assets.Scripts.Ghost
{
    public class GhostDie : IGhostState
    {
        void IGhostState.OnStateEnter(GhostBehaviour behaviour)
        {
            behaviour.PlayAnimation("Die");
            SoundManager.Instance.PlaySfx(SoundManager.Instance.GhostHitClip, true);
        }

        void IGhostState.OnStateExit(GhostBehaviour behaviour)
        {
        }

        IGhostState IGhostState.Run(GhostBehaviour behaviour)
        {
            behaviour.gameObject.SetActive(false);
            return this;
        }
    }
}
