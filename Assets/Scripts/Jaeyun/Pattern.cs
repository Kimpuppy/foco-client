using UnityEngine;

namespace Jaeyun
{
    public abstract class Pattern : ScriptableObject
    {
        private BossObject _boss;
        private AnimationPlayer _animationPlayer;

        public void Init(BossObject boss, AnimationPlayer animationPlayer)
        {
            _boss = boss;
            _animationPlayer = animationPlayer;
        }
        
        public abstract void EnterPattern();
        public abstract void ExitPattern();
    }
}