using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    public GameState GameState { get; private set; }
    [field: SerializeField] public GameData GameData { get; private set; }

    public PlayerController Player { get; private set; }
    public StageManager StageManager { get; private set; }
    public UIManager UIManager { get; private set; }


    public event UnityAction<int> OnStageCleared;



    protected override void Awake()
    {
        base.Awake();

        Player = FindAnyObjectByType<PlayerController>();
        
        Player.Init(this);
        
        StageManager = GetComponentInChildren<StageManager>();

        UIManager = GetComponentInChildren<UIManager>();

        GameState = new();
    }

    private void Start()
    {
        GameState.stageLevel = 0;
        
        StartStage(0);
        
        UIManager.UpdateGoldText(GameState.gold);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartStage(GameState.stageLevel);
        }
    }


    public void AddGold(int amount)
    {
        GameState.gold += amount;
        
        UIManager.UpdateGoldText(GameState.gold);
    }
    
    
    public void FailedStage()
    {
        StartStage(GameState.stageLevel);
    }

    public void ClearedStage()
    {
        OnStageCleared?.Invoke(GameState.stageLevel);
    }


    public void StartNextStage()
    {
        var targetStageData = GameData.StageDatas.FirstOrDefault((data)=> data.Level == GameState.stageLevel + 1);

        GameState.stageLevel = targetStageData == null ? GameState.stageLevel : GameState.stageLevel + 1;
        
        StartStage(GameState.stageLevel);
    }



    
    private void StartStage(int level)
    {
        var targetStageData = GameData.StageDatas.FirstOrDefault((data) => data.Level == level);

        if (targetStageData != null)
        {
            StageManager.CreateStage(targetStageData);
            
            UIManager.UpdateStageText(targetStageData.Level);
        }
    }
}
