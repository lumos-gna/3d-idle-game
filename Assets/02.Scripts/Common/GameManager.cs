using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameState GameState { get; private set; }
    [field: SerializeField] public GameData GameData { get; private set; }

    public PlayerController Player { get; private set; }
    public StageManager StageManager { get; private set; }


    
    protected override void Awake()
    {
        base.Awake();

        Player = FindAnyObjectByType<PlayerController>();
        
        StageManager = GetComponentInChildren<StageManager>();

        GameState = new();
    }

    private void Start()
    {
        GameState.stageLevel = 0;
        
        StartStage(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            RestartStage();
        }
    }


    public void RestartStage()
    {
        StartStage(GameState.stageLevel);
    }

    public void StartNextStage()
    {
        var targetStage = GameData.GetStage((data) => data.Level == GameState.stageLevel + 1);

        if (targetStage == null)
        {
            RestartStage();
        }
        else
        {
            StartStage(GameState.stageLevel + 1);
        }
    }
    
    private void StartStage(int level)
    {
        var targetStageData = GameData.GetStage((data) => data.Level == level);

        if (targetStageData != null)
        {
            var targetEnemyDataList = GameData.GetEnemies((data) => data.Level == level);
            
            StageManager.CreateStage(targetStageData, targetEnemyDataList);
            
            Player.Init();
        }
    }

}
