using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreObjectInfo : MonsterObjectInfo {
    public int HealAmount;
}

public class CoreObject : MonsterObject {
    public LineRenderer LineObject;
    public GameObject HealParticle;
    
    private int healAmount;
    public int HealAmount => healAmount;
    
    private CoreAttackBoss targetBoss = null;
    
    public override void Init(ObjectInfo objectInfo) {
        base.Init(objectInfo);
        
        if (objectInfo is CoreObjectInfo coreObjectInfo) {
            healAmount = coreObjectInfo.HealAmount;
        }
    }

    protected override void Start() {
        base.Start();

        if (GameWorld.Instance.WorldBossObject != null) {
            if (GameWorld.Instance.WorldBossObject is CoreAttackBoss coreAttackBoss) {
                targetBoss = coreAttackBoss;
            }
        }
        StartCoroutine(Heal());
    }

    protected override void Update() {
        base.Update();

        if (targetBoss != null) {
            LineObject.SetPosition(0, new Vector3(transform.position.x, transform.position.y, 1f));
            LineObject.SetPosition(1, new Vector3(targetBoss.transform.position.x, targetBoss.transform.position.y, 1f));
        }
    }

    private IEnumerator Heal() {
        while (true) {
            if (targetBoss != null) {
                GameObject particleObject = Instantiate(HealParticle, targetBoss.transform.position + new Vector3(0f, 0f, -0.1f),
                    Quaternion.identity);
                Destroy(particleObject, 1.0f);
                targetBoss.HealTo(healAmount);
            }
            
            yield return new WaitForSeconds(1.0f);
        }
    }

    protected override void OnDead() {
        base.OnDead();
        Destroy(gameObject);
    }
}
