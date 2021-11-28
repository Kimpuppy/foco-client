using UnityEngine;

namespace Jaeyun.SpawnBoss
{
    public class SpawnMonster : MonsterObject
    {
        
        private bool _isAcitve = true;

        [SerializeField]
        private Collider2D collider;
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        public bool IsActive => _isAcitve;
        
        public GhostSprite GhostSpriteObject;

        public void Activate()
        {
            _isAcitve = true;
            collider.enabled = true;
            spriteRenderer.enabled = true;
        }
        
        public void DeActivate()
        {
            _isAcitve = false;
            collider.enabled = false;
            spriteRenderer.enabled = false;
        }
        
        protected override void OnPlayerAttackedToMonster(PlayerObject playerObject) 
        {
            DamageTo(1);
        }
    
        protected override void OnPlayerDamagedByMonster(PlayerObject playerObject) 
        {
            playerObject.DamageTo(1);
        }

        protected override void OnDead()
        {
            base.OnDead();
            DeActivate();
        }
        
    }
}