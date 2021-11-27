using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossObject : MonsterObject {
    protected override void Start() {
        base.Start();
        StartCoroutine(PlayPattern());
    }
    
    protected virtual IEnumerator PlayPattern() {
        yield return null;
    }
}
