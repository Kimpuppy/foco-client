using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoSingleton<GameManager> {
    [Header("Camera Controller")]
    public CameraController CameraObject;
    
    [Header("Player Property")]
    public GameObject PlayerPrefab;
    public int PlayerHp;
    public int PlayerAttackDamage;
    public Vector2 PlayerSpawnPosition = Vector2.zero;
    
    [Header("Boss Property")]
    public GameObject BossPrefab;
    public int BossHp;
    public int BossDamage;
    public Vector2 BossSpawnPosition = Vector2.zero;

    [Header("Boss Name")]
    public string KorBossName;
    public string EngBossName;
    
    private void Start() {
        GameWorld.Instance.SpawnPlayer(PlayerPrefab, PlayerSpawnPosition);
        GameWorld.Instance.WorldPlayerObject.Init(new PlayerObjectInfo {
            Hp = PlayerHp,
            AttackDamage = PlayerAttackDamage,
        });
            
        GameWorld.Instance.SpawnBoss(BossPrefab, BossSpawnPosition);
        GameWorld.Instance.WorldBossObject.Init(new MonsterObjectInfo {
            Hp = BossHp,
            Damage = BossDamage,
        });

        CameraObject.Target = GameWorld.Instance.WorldPlayerObject.gameObject.transform;
    }
}
