using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Jaeyun
{
    [CreateAssetMenu(menuName = "Pattern/Shoot Beam Pattern")]
    public class ShootBeamPattern : Pattern
    {
        
        private WeakPointBoss _weakPointBoss;
        private WeakPointBossBeam _beam;

        [SerializeField] private int shootCount;
        [SerializeField] private float shootDelay;

        [SerializeField] private MakeWeakPointPattern makeWeakPointPattern;

        public override void EnterPattern()
        {
            _weakPointBoss = (WeakPointBoss)_boss;
            _beam = _weakPointBoss.Beam;
            _beam.ChasePlayer();
            _thisPatternRoutine = _boss.StartCoroutine(ShootPattern());
        }

        IEnumerator ShootPattern()
        {
            yield return new WaitForSeconds(shootDelay);

            for(int i = 0; i < shootCount; i++)
            {
                yield return _beam.Shoot().ToCoroutine();
                yield return new WaitForSeconds(shootDelay);
            }
            ExitPattern();
        }

        public override void ExitPattern()
        {
            _beam.StopChasePlayer();
            _weakPointBoss.StartPattern(makeWeakPointPattern);
        }

        protected override void OnStopPattern()
        {
            _beam.StopChasePlayer();
        }
    }
}