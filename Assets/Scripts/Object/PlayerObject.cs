using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectInfo : MovingObjectInfo {
    public int AttackDamage;
}

public class PlayerObject : MovingObject {
    private Vector2 clickPos = Vector2.zero;
    private Vector2 dragPos = Vector2.zero;
    protected float dragPower = 0;
    
    private float velocity = 0.0f;
    public float Velocity => velocity;
    
    private bool isAttacking = false;
    public bool IsAttacking => isAttacking;

    public GameObject DamageParticle;
    
    private int attackDamage = 1;
    public int AttackDamage => attackDamage;

    private CameraController cameraController;

    private SpriteRenderer spriteRenderer;

    private Timer superArmorTimer;

    public AudioClip MoveSound;
    public AudioClip AttackSound;
    public AudioClip DamageSound;
    
    public GameObject PowerMark;
    
    public override void Init(ObjectInfo objectInfo) {
        base.Init(objectInfo);
        
        if (objectInfo is PlayerObjectInfo playerObjectInfo) {
            attackDamage = playerObjectInfo.AttackDamage;
        }
    }
    
    protected override void Start() {
        base.Start();
        cameraController = Camera.main.GetComponent<CameraController>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        superArmorTimer = new Timer(1.0f, false); {
            superArmorTimer.IsEnable = true;
        };
        UIManager.Instance.SetHpGauge(hp);
    }
    
    protected override void Update() {
        base.Update();
        
        Move();
        
        velocity = rigidbody.velocity.magnitude;
        isAttacking = (velocity > GameConstants.PLAYER_ATTACK_VELOCITY) ? true : false;
        spriteRenderer.flipX = (rigidbody.velocity.x < 0f) ? true : false;
        spriteRenderer.color = Color.Lerp(spriteRenderer.color, Color.white, Time.deltaTime);
    }
    
    private void Move() {
        if (Input.GetMouseButtonDown(0)) {
            clickPos = Input.mousePosition;
            PowerMark.SetActive(true);
        }
        if (Input.GetMouseButton(0)) {
            dragPos = Input.mousePosition;

            dragPower = Vector2.Distance(clickPos, dragPos) * 0.05f;
            
            Vector2 direction = (clickPos - dragPos);
            direction.Normalize();
            PowerMark.transform.position = gameObject.transform.position + (Vector3)(-direction * Mathf.Clamp(dragPower / 10.0f, 0f, 2f))
                + new Vector3(0f, 0f, -1f);
        }
        if (Input.GetMouseButtonUp(0)) {
            Vector2 dragDirection = (clickPos - dragPos).normalized;
            rigidbody.AddForce(dragDirection * dragPower, ForceMode2D.Impulse);
            
            dragPower = 0;
            clickPos = Vector2.zero;
            dragPos = Vector2.zero;
            
            SoundManager.Instance.PlaySFX(MoveSound);
            PowerMark.SetActive(false);
        }
    }
    
    public override void DamageTo(int damage) {
        if (superArmorTimer.IsDone) {
            base.DamageTo(damage);
        }
    }
    
    protected override void OnDamaged() {
        if (superArmorTimer.IsDone) {
            base.OnDamaged();
            spriteRenderer.color = Color.red;
            GameObject particleObject = Instantiate(DamageParticle, transform.position + new Vector3(0f, 0f, -0.1f),
                Quaternion.identity);
            Destroy(particleObject, 1.0f);
            superArmorTimer.IsEnable = true;
            SoundManager.Instance.PlaySFX(DamageSound);
            UIManager.Instance.SetHpGauge(hp);
        }
    }
    
    protected override void OnDead() {
        base.OnDead();
        UIManager.Instance.SeeFailScreen();
        Destroy(gameObject);
    }
    
    protected virtual void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Monster")) {
            MonsterObject monsterObject = collision.gameObject.GetComponent<MonsterObject>();
            if (monsterObject != null) {
                if (isAttacking) {
                    cameraController.Shake(Mathf.Clamp(velocity / 100f, 0f, 0.75f), 7f);
                    monsterObject.DamageTo(attackDamage);
                    
                    GameObject particleObject = Instantiate(DamageParticle, transform.position + new Vector3(0f, 0f, -0.1f),
                        Quaternion.identity);
                    Destroy(particleObject, 1.0f);
                    
                    SoundManager.Instance.PlaySFX(AttackSound);
                }
            }
        }
    }
}
