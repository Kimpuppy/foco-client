using UnityEngine;

namespace Jaeyun
{
    public abstract class Pattern : ScriptableObject
    {
        protected BossObject _boss;
        protected AnimationPlayer _animationPlayer;
        
        [SerializeField]
        protected AnimationClip enterPatternAnimation;
        [SerializeField]
        protected AnimationClip exitPatternAnimation;

        public void Init(BossObject boss, AnimationPlayer animationPlayer)
        {
            _boss = boss;
            _animationPlayer = animationPlayer;
        }

        public abstract void EnterPattern();
        public abstract void ExitPattern();
        
        protected void PlayAnimation(AnimationClip clip)
        {
            if (clip == null) return;
            _animationPlayer.PlayAnim(clip);
        }
        
    }
}