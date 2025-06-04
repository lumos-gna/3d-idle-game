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
        
        EnemyManager = GetComponentInChildren<EnemyManager>();
        
        StageManager = GetComponentInChildren<StageManager>();
        
        StageManager.Initialize(this);
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
            StageManager.CreateStage(targetStage);
        }
    }

}
