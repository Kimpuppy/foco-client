using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWorld : MonoSingleton<GameWorld> {
    private PlayerObject worldPlayerObject;
    public PlayerObject WorldPlayerObject => worldPlayerObject;
    
    private BossObject worldBossObject;
    public BossObject WorldBossObject => worldBossObject;
    
    private List<MonsterObject> worldMonsterObjectList = new List<MonsterObject>();
    public List<MonsterObject> WorldMonsterObjectList => worldMonsterObjectList;
    
    public PlayerObject SpawnPlayer(GameObject prefab, Vector3 position, float angle = 0f) {
        PlayerObject playerObject = Instantiate(prefab, position, Quaternion.Euler(new Vector3(0, 0, angle))).GetComponent<PlayerObject>();
        worldPlayerObject = playerObject;
        return playerObject;
    }
    
    public BossObject SpawnBoss(GameObject prefab, Vector3 position, float angle = 0f) {
        BossObject bossObject = Instantiate(prefab, position, Quaternion.Euler(new Vector3(0, 0, angle))).GetComponent<BossObject>();
        worldBossObject = bossObject;
        return bossObject;
    }
    
    public MonsterObject SpawnMonster(GameObject prefab, Vector3 position, float angle = 0f) {
        MonsterObject monsterObject = Instantiate(prefab, position, Quaternion.Euler(new Vector3(0, 0, angle))).GetComponent<MonsterObject>();
        worldMonsterObjectList.Add(monsterObject);
        return monsterObject;
    }
}