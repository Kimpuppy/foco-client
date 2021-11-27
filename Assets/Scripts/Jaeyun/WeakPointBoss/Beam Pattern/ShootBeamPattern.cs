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

        [SerializeField] private ShootBeam shootBeam;

        private ShootBeam _shootBeam;

        public override void EnterPattern()
        {
            _weakPointBoss = (WeakPointBoss)_boss;
            _beam = _weakPointBoss.Beam;
            _beam.ChasePlayer();
            _thisPatternRoutine = _boss.StartCoroutine(ShootPattern());

            _shootBeam = Instantiate(shootBeam);
            _shootBeam.gameObject.SetActive(false);
        }

        IEnumerator ShootPattern()
        {
            yield return new WaitForSeconds(shootDelay);

            for(int i = 0; i < shootCount; i++)
            {
                _shootBeam.gameObject.SetActive(true);
                _shootBeam.transform.position = _boss.transform.position;
                _shootBeam.Shoot();
                yield return new WaitForSeconds(shootDelay);
            }
            ExitPattern();
        }

        public override void ExitPattern()
        {
            _beam.StopChasePlayer();
            Destroy(_shootBeam.gameObject);
            _weakPointBoss.StartPattern(makeWeakPointPattern);
        }

        protected override void OnStopPattern()
        {
            Destroy(shootBeam.gameObject);
            _beam.StopChasePlayer();
        }
    }
}