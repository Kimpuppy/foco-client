using System;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
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
        
        
        public void ActiveBeam()
        {
            gameObject.SetActive(true);
        }

        public void DeActivateBeam()
        {
            gameObject.SetActive(false);
        }

        [ContextMenu("Chase Player")]
        public void ChasePlayer()
        {
            StartCoroutine(ChasePlayerRoutine());
        }

        IEnumerator ChasePlayerRoutine()
        {
            
            var player = FindObjectOfType<PlayerObject>();
            var nowRotation = MathUtil.AngleTo360(transform.eulerAngles.z);

            
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

            if (hit)
            {
                var dist = hit.distance;
                beamCollider.localScale = new Vector3(dist, 1, 1);
            }
            else
            {
                beamCollider.localScale = new Vector3(100, 1, 1);
            }

            beamCollider.localPosition = new Vector3(beamCollider.localScale.x * .5f, 0, 0);
        }

        
    }
}