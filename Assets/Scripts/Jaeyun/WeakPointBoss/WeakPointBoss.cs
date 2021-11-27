using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaeyun
{
    [RequireComponent(typeof(AnimationPlayer))]
    public class WeakPointBoss : BossObject
    {
        [SerializeField] private float weakPointDamage;
        [SerializeField] private float normalDamage;

        [SerializeField] private AnimationPlayer animationPlayer;


        public List<Pattern> patterns = new List<Pattern>();

        protected override IEnumerator PlayPattern()
        {
            yield return null;
        }
    }
}