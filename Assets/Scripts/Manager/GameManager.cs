using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager> {
    private int stage = 1;
    public int Stage => stage;
    
    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }
    
    private void Start() {
        
    }
}
