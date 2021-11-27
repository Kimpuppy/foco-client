using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreAttackBoss : BossObject {
    public List<Vector2> CoreObjectSpawnPosition;
    public float SpawnCoreObjectDelay = 10.0f;
    public GameObject CoreObjectPrefab;
    
    private List<GameObject> coreObjectList = new List<GameObject>();

    protected override void Start() {
        base.Start();
        SpawnCoreObject();
        StartCoroutine(CheckCoreObject());
    }

    private void SpawnCoreObject() {
        for (int i = 0; i < CoreObjectSpawnPosition.Count; i++) {
            MonsterObject monsterObject = GameWorld.Instance.SpawnMonster(CoreObjectPrefab, CoreObjectSpawnPosition[i]);
            if (monsterObject is CoreObject coreObject) {
                coreObject.Init(new CoreObjectInfo {
                    Damage = 1,
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
            yield return null;
        }
    }
    
    private IEnumerator CheckCoreObject() {
        while (true) {
            coreObjectList.RemoveAll(x => x == null);
            if (coreObjectList.Count <= 0) {
                coreObjectList.Clear();
                yield return new WaitForSeconds(SpawnCoreObjectDelay);
                SpawnCoreObject();
            }
            yield return new WaitForSeconds(1.0f);
        }
    }
}
