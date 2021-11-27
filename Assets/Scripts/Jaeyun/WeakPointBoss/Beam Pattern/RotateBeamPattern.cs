using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Jaeyun
{
    [CreateAssetMenu(menuName = "Pattern/Rotate Beam Pattern")]
    public class RotateBeamPattern : Pattern
    {
        private WeakPointBoss _weakPointBoss;
        private WeakPointBossBeam _beam;

        [SerializeField]
        private int rotationCount = 2;
        [SerializeField]
        private float rotationSpeed = 5;
        [SerializeField]
        private  float attackDelay;

        [SerializeField] private MakeWeakPointPattern weakPointPattern;
        
        public override void EnterPattern()
        {
            _weakPointBoss = (WeakPointBoss)_boss;
            _beam = _weakPointBoss.Beam;
            
             _thisPatternRoutine = _weakPointBoss.StartCoroutine(BeamAttack().ToCoroutine());
        }

        private async UniTask BeamAttack()
        {
            for (int i = 0; i < rotationCount; i++)
            {
                await _beam.ActiveBeam();
                await _beam.RotateBeam(rotationSpeed).ToUniTask();
                await _beam.DeActivateBeam();
                await UniTask.Delay(TimeSpan.FromSeconds(attackDelay));
            }
            
            ExitPattern();
        }

        public override void ExitPattern()
        {
            _weakPointBoss.StartPattern(weakPointPattern);
        }

        protected override void OnStopPattern()
        {
            _beam.DeActivateBeam().Forget();
        }
    }
}