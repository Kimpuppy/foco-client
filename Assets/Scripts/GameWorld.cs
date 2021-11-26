using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWorld : MonoSingleton<GameWorld> {
    private PlayerObject worldPlayerObject;
    public PlayerObject WorldPlayerObject => worldPlayerObject;
    
    private List<MonsterObject> worldMonsterObjectList;
    public List<MonsterObject> WorldMonsterObjectList => worldMonsterObjectList;
    
    public PlayerObject SpawnPlayer(GameObject prefab, Vector3 position, Vector3 angle) {
        PlayerObject playerObject = Instantiate(prefab, position, Quaternion.Euler(angle)).GetComponent<PlayerObject>();
        worldPlayerObject = playerObject;
        return playerObject;
    }
    
    public MonsterObject SpawnMonster(GameObject prefab, Vector3 position, Vector3 angle) {
        MonsterObject monsterObject = Instantiate(prefab, position, Quaternion.Euler(angle)).GetComponent<MonsterObject>();
        worldMonsterObjectList.Add(monsterObject);
        return monsterObject;
    }
}
