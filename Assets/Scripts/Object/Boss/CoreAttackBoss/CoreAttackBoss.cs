using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreAttackBoss : BossObject {
    public Vector2 DefaultPosition;
    public float MoveSpeed = 5.0f;
    
    public List<Vector2> CoreObjectSpawnPosition;
    public float SpawnCoreObjectDelay = 10.0f;
    public GameObject CoreObjectPrefab;
    
    public GameObject SpriteObject;
    public Animator SpriteAnimator;
    
    private List<GameObject> coreObjectList = new List<GameObject>();
    private bool isCoreDestroyed = false;

    protected override void Start() {
        base.Start();
        damage = 1;
        SpawnCoreObject();
        StartCoroutine(CheckCoreObject());
    }
    
    private void SpawnCoreObject() {
        isCoreDestroyed = false;
        
        for (int i = 0; i < CoreObjectSpawnPosition.Count; i++) {
            MonsterObject monsterObject = GameWorld.Instance.SpawnMonster(CoreObjectPrefab, CoreObjectSpawnPosition[i]);
            if (monsterObject is CoreObject coreObject) {
                coreObject.Init(new CoreObjectInfo {
                    Damage = 0,
                    HealAmount = 1,
                    Hp = 5,
                });
                coreObjectList.Add(coreObject.gameObject);
            }
        }
    }
    
    protected override IEnumerator PlayPattern() {
        base.PlayPattern();
        
        while (true) {
            Vector2 targetPosition = Vector2.zero;
            Vector2 nowPosition = transform.position;
            if (isCoreDestroyed) { 
                targetPosition = GameWorld.Instance.WorldPlayerObject.transform.position;
            }
            else {
                targetPosition = DefaultPosition;
            }
            
            Vector2 direction = targetPosition - nowPosition;
            direction.Normalize();
            
            float targetDistance = Vector2.Distance(targetPosition, nowPosition);
            if (targetDistance > 0.1f) {
                if (SpriteAnimator.GetBool("IsMoving") == false) {
                    SpriteAnimator.SetBool("IsMoving", true);
                }
                
                transform.Translate(direction * MoveSpeed * Time.deltaTime);
            }
            else {
                if (SpriteAnimator.GetBool("IsMoving")) {
                    SpriteAnimator.SetBool("IsMoving", false);
                }
            }
            
            if (direction.x < 0f) {
                SpriteObject.transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else {
                SpriteObject.transform.localScale = new Vector3(-1f, 1f, 1f);
            }

            yield return null;
        }
    }
    
    private IEnumerator CheckCoreObject() {
        while (true) {
            coreObjectList.RemoveAll(x => x == null);
            if (coreObjectList.Count <= 0) {
                isCoreDestroyed = true;
                coreObjectList.Clear();
                
                yield return new WaitForSeconds(SpawnCoreObjectDelay);
                
                SpawnCoreObject();
            }
            else {
                isCoreDestroyed = false;
            }
            yield return new WaitForSeconds(1.0f);
        }
    }
}
