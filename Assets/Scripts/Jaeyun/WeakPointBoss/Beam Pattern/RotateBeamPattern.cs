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

        public int rotationCount = 2;
        public float rotationSpeed = 5;

        public float attackDelay;
        
        public override void EnterPattern()
        {
            _weakPointBoss = (WeakPointBoss)_boss;
            _beam = _weakPointBoss.Beam;
            
            BeamAttack().Forget();
        }

        private async UniTaskVoid BeamAttack()
        {
            for (int i = 0; i < rotationCount; i++)
            {
                _beam.ActiveBeam();
                await _beam.RotateBeam(rotationSpeed).ToUniTask();
                _beam.DeActivateBeam();
                await UniTask.Delay(TimeSpan.FromSeconds(attackDelay));
            }
        }

        public override void ExitPattern()
        {
            
        }
    }
}