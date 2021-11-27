using System;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Jaeyun
{
    public class WeakPointBossBeam : MonoBehaviour
    {
        [SerializeField]
        private int damage;

        [SerializeField]
        private Transform beamCollider;

        [SerializeField] private LayerMask beamCollisionMask;
        
        public float rotationSpeed;

        public LineRenderer aimLine;

        public async UniTask ActiveBeam()
        {
            beamCollider.gameObject.SetActive(true);
            //활성화 이펙트 추가
            await UniTask.Delay(TimeSpan.FromSeconds(.3f));
        }

        public async UniTask DeActivateBeam()
        {
            //비활성화 이펙트 추가
            await UniTask.Delay(TimeSpan.FromSeconds(.3f));
            beamCollider.gameObject.SetActive(false);
        }

        public void ChasePlayer()
        {
            ActivateAimLine();
            StartCoroutine(nameof(ChasePlayerRoutine));
        }

        public void StopChasePlayer()
        {
            StopCoroutine(nameof(ChasePlayerRoutine));
            DeActivateAimLine();
        }

        private void ActivateAimLine()
        {
            aimLine.enabled = true;
        }
        
        private void DeActivateAimLine()
        {
            aimLine.enabled = false;
        }

        public async UniTask Shoot()
        {
            DeActivateAimLine();
            await ActiveBeam();
            await DeActivateBeam();
            ActivateAimLine();
        }

        IEnumerator ChasePlayerRoutine()
        {
            
            var player = FindObjectOfType<PlayerObject>();
            var nowRotation = MathUtil.AngleTo360(MathUtil.LookAt2D(transform.position, player.transform.position));

            
            while (true)
            {
                float targetRotation = MathUtil.AngleTo360(MathUtil.LookAt2D(transform.position, player.transform.position) - nowRotation);
                RotateSlerp(targetRotation, rotationSpeed);

                yield return null;
            }
            
            void RotateSlerp(float targetRotation, float rotSpeed)
            {
                if (targetRotation <= 180 && targetRotation > 0)
                {
                    float rotValue = rotSpeed * 10 * Time.deltaTime;
                    if (rotValue >= targetRotation)
                    {
                        nowRotation += targetRotation;
                    }
                    else
                    {
                        nowRotation += rotValue;
                    }
                }
                else if (targetRotation > 180 && targetRotation < 360)
                {
                    float rotValue = rotSpeed * 10 * Time.deltaTime;
                    if (rotValue >= (360 - targetRotation))
                    {
                        nowRotation -= (360 - targetRotation);
                    }
                    else
                    {
                        nowRotation -= rotValue;
                    }
                }

                transform.eulerAngles = Vector3.forward * nowRotation;
            }
        }
        

        public IEnumerator RotateBeam(float rotateTime)
        {
            float timeCount = 0;
            var currentAngle = transform.eulerAngles.z;
            var targetAngle = transform.eulerAngles.z + 360;

            while (true)
            {
                var t = timeCount / rotateTime;
                var lerpAngle = Mathf.Lerp(currentAngle, targetAngle, t);

                transform.rotation = Quaternion.Euler(0,0, lerpAngle);
                
                if (t >= 1) break;

                timeCount += Time.deltaTime;
                yield return null;
            }
        }
        
        private void Update()
        {
            UpdateBeamLength();
        }

        private void UpdateBeamLength()
        {
            var rayCastDir = transform.right;
            var hit = Physics2D.Raycast(transform.position, rayCastDir, 100, beamCollisionMask);
            aimLine.SetPosition(0, transform.position);
            if (hit)
            {
                var dist = hit.distance;
                beamCollider.localScale = new Vector3(dist, 1, 1);
                aimLine.SetPosition(1, hit.point);
            }
            else
            {
                beamCollider.localScale = new Vector3(100, 1, 1);
                aimLine.SetPosition(1, transform.position + rayCastDir * 100);
            }

            beamCollider.localPosition = new Vector3(beamCollider.localScale.x * .5f, 0, 0);
            
        }

        
    }
}