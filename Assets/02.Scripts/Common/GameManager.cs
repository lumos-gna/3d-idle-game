using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [field: SerializeField] public GameData GameData { get; private set; }

    public PlayerController Player { get; private set; }
    public EnemyManager EnemyManager { get; private set; }
    public StageManager StageManager { get; private set; }

    
    
    protected override void Awake()
    {
        base.Awake();

        Player = FindAnyObjectByType<PlayerController>();
        
        EnemyManager = GetComponentInChildren<EnemyManager>();
        
        StageManager = GetComponentInChildren<StageManager>();
    }

    private void Start()
    {
        StartStage(0);
    }


    public void StartStage(int level)
    {
        var targetStage = GameData.GetStage((data) => data.Level == level);

        if (targetStage != null)
        {
            Stage stage = StageManager.CreateStage(targetStage);
            
            EnemyManager.CreateStageEnemies(stage);
            
            Player.StartStage(stage);
        }
    }

}
