using System.Collections;
using System.Collections.Generic;
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

    protected void Start()
    {
        base.Start();

        spriteRenderer = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<PlayerObject>();

        confineLineHandler = confineLine.GetComponent<ConfineLineHandler>();
        
        
        confineLineHandler.LineObjectCreate(Vector2.zero, 1.1f);

        curTime = Time.realtimeSinceStartup;
        StartCoroutine(PlayerPushOut(20.0f, 10));
        StartCoroutine(IronCreateCycle(12f, 2, 5));
        //StartCoroutine(LineCreateCycle(1, 13, 5));
    }

    private float curTime = 0;
    

    protected override void Update()
    {
        base.Update();

        Vector2 moveDirection = SpriteFlip() ? Vector2.right : Vector2.left;
        
        //RaycastHit hit = Physics.BoxCast(transform.position, Vector3.one, Vector3.zero,
        //    quaternion.identity, out hit)
        //    
        //    Physics2D.BoxCastAll()
//
        //if (!)
        //{
        //    transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        //}
        
        //rigidbody.AddForce(moveDirection * 10 * Time.deltaTime, ForceMode2D.Impulse);
        //bool isHit = Physics2D.BoxCast (transform.position, transform.lossyScale / 2, transform.forward, out hit, transform.rotation, maxDistance);

    }

    private SpriteRenderer spriteRenderer;
    

    protected 
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
    private void ActiveBuff(BuffType buffType)
    {
        curBuffType = buffType;

        switch (buffType)
        {
            case BuffType.None :
                break;
            case BuffType.Invincibility :
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
        
        pushEffect.SetActive(false);
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
    }
    
    private IEnumerator LineCreateCycle(int damage, int createDelay, int lineNum)
    {
        int curCreateCount = 0;

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

        if (ironObj.TryGetComponent(out Iron iron))
        {
            iron.Init(new IronInfo 
            {
                Damage = 2,
                FallSpeed = fallspeed,
            });
        }
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
