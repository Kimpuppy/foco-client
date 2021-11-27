using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = System.Random;

namespace Jaeyun
{
    [CreateAssetMenu(menuName = "Pattern/Make Weak Point Pattern")]
    public class MakeWeakPointPattern : Pattern
    {

        [SerializeField] private float patternDuration;
        [SerializeField] private int activateWeakPointCount;
        
        private WeakPointBoss _weakPointBoss;
        private List<WeakPoint> _weakPointCandidates;

        private List<WeakPoint> _activeWeakPoints;

        [SerializeField] private RotateBeamPattern rotateBeamPattern;
        [SerializeField] private ShootBeamPattern shootBeamPattern;

        private void GetWeakPointCandidates()
        {
            _weakPointCandidates = _weakPointBoss.WeakPointCandidates;
        }
        
        public override void EnterPattern()
        {
            PlayAnimation(enterPatternAnimation);
            
            _weakPointBoss = (WeakPointBoss)_boss;

            MakeWeakPoints();

            _thisPatternRoutine = _weakPointBoss.StartCoroutine(CheckPatternDone());
        }

        private void MakeWeakPoints()
        {
            _activeWeakPoints = new List<WeakPoint>();

            GetWeakPointCandidates();
            
            var weakPointIndex = GetRandomNumber(0, _weakPointCandidates.Count, activateWeakPointCount);
            
            foreach (var index in weakPointIndex)
            {
                var weakPointToActive = _weakPointCandidates[index];
                weakPointToActive.Activate(UnRegisterWeakPoint);
                RegisterWeakPoint(weakPointToActive);
            }
        }

        IEnumerator CheckPatternDone()
        {
            float timeCount = 0;
            
            while (true)
            {
                if (IsPatternTimeOver())
                {
                    break;
                }

                if (PatternCleared())
                {
                    break;
                }
                
                timeCount += Time.deltaTime;

                yield return null;
            }
            
            ExitPattern();

            bool IsPatternTimeOver()
            {
                return timeCount >= patternDuration;
            }

            bool PatternCleared()
            {
                return _activeWeakPoints.Count < 1;
            }
        } 


        public override void ExitPattern()
        {
            DeActivateAllWeakPoint();
            PlayAnimation(exitPatternAnimation);
            var random = new Random();
            var value = random.Next(0, 2);
            if (value == 0)
            {
                _weakPointBoss.StartPattern(rotateBeamPattern);
            }
            else if (value == 1)
            {
                _weakPointBoss.StartPattern(shootBeamPattern);
            }
        }

        protected override void OnStopPattern()
        {
            DeActivateAllWeakPoint();
        }

        private void DeActivateAllWeakPoint()
        {
            foreach (var weakPoint in _activeWeakPoints)
            {
                weakPoint.DeActivate();
            }
        }


        #region Helper Function

        private List<int> GetRandomNumber(int from,int to,int numberOfElement)
        {
            var random = new Random();
            var numbers = new HashSet<int>();
            while (numbers.Count < numberOfElement)
            {
                numbers.Add(random.Next(from, to));
            }
            return numbers.ToList();
        }

        private void RegisterWeakPoint(WeakPoint weakPoint)
        {
            _activeWeakPoints.Add(weakPoint);
        }
        
        private void UnRegisterWeakPoint(WeakPoint weakPoint)
        {
            _activeWeakPoints.Remove(weakPoint);
        }

        #endregion
    }
}