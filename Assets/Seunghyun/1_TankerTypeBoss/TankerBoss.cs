using System.Collections;
using System.Collections.Generic;
using DG.DemiLib;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class TankerBoss : BossObject
{
    // Range Ratio 0 ~ 1
    private float DEFENSE_RATIO = 0.5f;
    // 피격 판정 오브젝트가 소환될 높이
    private float RAIN_OFFSET_Y = 0.5f;
    private float FALL_SPEED = 5.0f;
    
    [SerializeField] private GameObject ironPrefab;
    [SerializeField] private GameObject confineLine;
    [SerializeField] private GameObject lineAttackPrefab;
    [SerializeField] private GameObject invincibilityIcon;
    
    [SerializeField] private float pushOutRange;
    private ConfineLineHandler confineLineHandler;
    
    protected PlayerObject player;

    [SerializeField] private GameObject pushEffect;

    private enum BuffType { None, Invincibility };

    private BuffType curBuffType;
    private bool invincibility = false;
    private float defenseRatio = 0.0f;
    

    [SerializeField] private float spawnHeight;

    private float moveSpeed = 2.3f;
    private float idleTime = 0;
    private float curTimes = 0;

    private SpriteRenderer spriteRenderer;
    
    protected void Start()
    {
        
        base.Start();
        Init(new MovingObjectInfo( ) {Hp = 30});
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<PlayerObject>();

        confineLineHandler = confineLine.GetComponent<ConfineLineHandler>();
        
        

        curTime = Time.realtimeSinceStartup;
        
        
        //StartCoroutine(LineCreateCycle(1, 13, 5));


        idleTime = Random.Range(2.0f, 5.0f);
    
        curTimes = Time.realtimeSinceStartup;
        // 라이프 사이클 시작
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }
    
    protected override void OnDead() 
    {
        Destroy(gameObject);
    }


    private float curTime = 0;

    private enum AttackPattern
    {
        Idle,
        Box,
        Push,
        Drop,
        Buff
    };

    [SerializeField] private AttackPattern curAttackPattern = AttackPattern.Drop;
    
    public bool patternEnd = true;
    public bool idleState = false;
    public bool actionPattern = false;
    private bool attackPattern = false;
    

    protected override void Update()
    {
        base.Update();
        
        Vector2 moveDirection = SpriteFlip() ? Vector2.right : Vector2.left;

        if (attackPattern == false)
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
        
        if (Time.realtimeSinceStartup - curTimes >= idleTime)
        {
            attackPattern = true;

            switch (curAttackPattern)
            {
                case AttackPattern.Box:
                    //confineLineHandler.LineObjectCreate(Vector2.zero, 1.1f);
                    StartCoroutine(LineCreateCycle(3, 10, 6));
                    break;
                case AttackPattern.Push:
                    StartCoroutine(PlayerPushOut(5.0f, 10));
                    break;
                case AttackPattern.Drop:
                    StopCoroutine(IronCreateCycle(0, 0, 0));
                    StartCoroutine(IronCreateCycle(5f, 20, 3));
                    break;
                case AttackPattern.Buff:
                    HealTo(10);
                    //StopCoroutine(ActiveBuff(BuffType.None, 0));
                    //StartCoroutine(ActiveBuff(BuffType.Invincibility, Random.Range(34, 8)));
                    idleTime = Random.Range(2.0f, 5.0f);
                    attackPattern = false;
                    break;
            }

            curAttackPattern = (AttackPattern) Random.Range(2, 5);
            curTimes = Time.realtimeSinceStartup;
        }
    }
    
    private bool SpriteFlip()
    {
        bool flip = player.transform.position.x >= transform.position.x;
        spriteRenderer.flipX = flip;

        return flip;
    }

    /// <summary>
    /// 버프를 활성화합니다.
    /// </summary>
    /// <param name="buffType">버프 타입</param>
    private IEnumerator ActiveBuff(BuffType buffType, float sustain)
    {
        curBuffType = buffType;

        switch (curBuffType)
        {
            case BuffType.None:
                invincibility = false;
                invincibilityIcon.SetActive(true);
                break;
            case BuffType.Invincibility:
                invincibility = true;
                invincibilityIcon.SetActive(false);
                break;
        }

        yield return new WaitForSeconds(sustain);
        
        curBuffType = BuffType.None;
        
        switch (curBuffType)
        {
            case BuffType.None:
                invincibility = false;
                invincibilityIcon.SetActive(true);
                break;
            case BuffType.Invincibility:
                invincibility = true;
                invincibilityIcon.SetActive(false);
                break;
        }
    }
    
    /// <summary>
    /// 일정 시간동안 일정한 간격으로 플레이어를 밀어냅니다.
    /// </summary>
    /// <param name="sustainT">지속 시간</param>
    /// <returns></returns>
    private IEnumerator PlayerPushOut(float sustain, int pushTick)
    {
        float sustainTime = sustain;
        int curPushCount = 0;
        
        pushEffect.SetActive(true);
        
        while (sustainTime >= 0)
        {
            if (curPushCount % pushTick == 0)
            {
                if (Vector2.Distance(new Vector2(player.transform.position.x, 0),
                    new Vector2(transform.position.x, 0)) <= pushOutRange)
                {
                    Vector2 dir = transform.position.x <= player.transform.position.x ? Vector2.right : Vector2.left;
                    player.Rigidbody.AddForce(dir * 1000, ForceMode2D.Force);
                }
  
                //ExplosionForce2D.AddExplosionForce(player.Rigidbody, 1000, Vector3.left, pushOutRange);
            }

            curPushCount++; 
            sustainTime -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        
        yield return new WaitForSeconds(Random.Range(4.0f, 8.0f));
        idleTime = Random.Range(2.0f, 5.0f);
        pushEffect.SetActive(false);
        patternEnd = true;
    }
    
    private IEnumerator IronCreateCycle(float fallspeed, int createDelay, int ironNum)
    {
        int curCreateCount = 0;

        while (ironNum > 0)
        {
            if (curCreateCount % createDelay == 0)
            {
                CreateIronRain(fallspeed);
                ironNum--;
                curCreateCount = 0;
            }

            curCreateCount++;
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(Random.Range(4.0f, 8.0f));
        idleTime = Random.Range(2.0f, 5.0f);
        attackPattern = false;
    }
    
    private IEnumerator LineCreateCycle(int damage, int createDelay, int lineNum)
    {
        int curCreateCount = 0;
        yield return new WaitForSeconds(2f);
        
        while (lineNum > 0)
        {
            if (curCreateCount % createDelay == 0)
            {
                CreateDamageLine(damage);
                lineNum--;
                curCreateCount = 0;
            }

            curCreateCount++;
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(Random.Range(4.0f, 8.0f));
        idleTime = Random.Range(2.0f, 5.0f);
        patternEnd = true;
    }
    
    /// <summary>
    /// 플레이어 주변에 4개의 라인 오브젝트가 있는 사각형으로 가둡니다.
    /// </summary>
    private void CreatePlayerLine()
    {
        var createPos = player.transform.position;

        confineLineHandler.ActiveLine(createPos);
    }

    /// <summary>
    /// 플레이어 머리 위에 데미지 피격 판정이 있는 오브젝트를 떨어뜨린다.
    /// </summary>
    private void CreateIronRain(float fallspeed)
    {
        var createPos = player.transform.position;

        GameObject ironObj = Instantiate(ironPrefab, new Vector2(createPos.x, createPos.y + spawnHeight),
            Quaternion.identity);
        //ironObj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 180));
        //if (ironObj.TryGetComponent(out Iron iron))
        //{
        //    iron.Init(new IronInfo 
        //    {
        //        Damage = 2,
        //        FallSpeed = fallspeed,
        //    });
        //}
    }

    /// <summary>
    /// 플레이어 위치에 데미지 피격이 있는 레이저를 소환합니다.
    /// </summary>
    /// <param name="damage">라인 데미지</param>
    private void CreateDamageLine(int damage)
    {
        int angle = Random.Range(0, 1) == 0 ? 0 : 90;
        
        GameObject damageLineObj = Instantiate(lineAttackPrefab, player.transform.position, 
            quaternion.Euler(0, 0, angle));

        var damageLine = damageLineObj.GetComponent<DamageLine>();
        damageLine.Init(damage);
    }
}
