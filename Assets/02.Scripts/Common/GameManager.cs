using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public PlayerController Player { get; private set; }
    public EnemyManager EnemyManager { get; private set; }
    public StageManager StageManager { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        
        EnemyManager = GetComponentInChildren<EnemyManager>();
        StageManager = GetComponentInChildren<StageManager>();
    }

}
