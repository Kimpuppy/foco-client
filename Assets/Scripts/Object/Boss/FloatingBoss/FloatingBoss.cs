using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingBoss : BossObject {
    public float MoveSpeed = 5.0f;
    public List<Vector2> MovingPosition;
    
    public GameObject FallingObjectPrefab;
    public GameObject BasementObjectPrefab;
    
    private int movingIndex = 0;
    
    protected override void Start() {
        base.Start();
        damage = 1;
        StartCoroutine(Move());
    }
    
    protected override IEnumerator PlayPattern() {
        base.PlayPattern();
        
        yield return new WaitForSeconds(3.0f);
        
        while (true) {
            for (int i = 0; i < 5; i++) {
                Instantiate(FallingObjectPrefab, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(5.0f);
            
            Instantiate(BasementObjectPrefab);
            yield return new WaitForSeconds(5.0f);
        }
    }
    
    private IEnumerator Move() {
        base.PlayPattern();
        
        while (true) {
            Vector2 targetPosition = MovingPosition[movingIndex];
            Vector2 nowPosition = transform.position;
            
            float targetDistance = Vector2.Distance(targetPosition, nowPosition);
            if (targetDistance < 0.1f) {
                if (movingIndex == 0) {
                    movingIndex = 1;
                }
                else {
                    movingIndex = 0;
                }
            }
            
            Vector2 direction = targetPosition - nowPosition;
            direction.Normalize();
            transform.Translate(direction * MoveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
