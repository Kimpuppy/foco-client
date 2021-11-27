using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaeyun
{
    [RequireComponent(typeof(AnimationPlayer))]
    public class WeakPointBoss : BossObject
    {
        [SerializeField] private int weakPointDamage;
        [SerializeField] private int normalDamage;

        [SerializeField] private AnimationPlayer animationPlayer;

        [SerializeField]
        private Pattern startPattern;

        [SerializeField] private WeakPointBossBeam beam;

        [SerializeField] private List<WeakPoint> weakPointCandidates;
        public List<WeakPoint> WeakPointCandidates => weakPointCandidates;

        public WeakPointBossBeam Beam => beam;

        private Pattern _currentPattern;


        protected override IEnumerator PlayPattern()
        {
            StartPattern(startPattern);
            yield return null;
        }

        public void StartPattern(Pattern pattern)
        {
            pattern.Init(this, animationPlayer);
            pattern.EnterPattern();
            _currentPattern = pattern;
        }

        public void WeakPointAttack()
        {
            DamageTo(weakPointDamage);   
        }
        
        
        protected override void OnDamaged() 
        {
            
        }
    
        protected override void OnDead() 
        {
            _currentPattern.StopPattern();
            Destroy(gameObject);
        }

    }
}