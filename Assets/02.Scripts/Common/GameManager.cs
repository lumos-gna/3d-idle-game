using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [field: SerializeField] public GameData GameData { get; private set; }

    public PlayerController Player { get; private set; }
    public StageManager StageManager { get; private set; }
    public UIManager UIManager { get; private set; }

    public int StageLevel { get; private set; }

    public BigInteger Gold { get; private set; }



    protected override void Awake()
    {
        base.Awake();

        Player = FindAnyObjectByType<PlayerController>();
        
        Player.Init(this);
        
        StageManager = GetComponentInChildren<StageManager>();

        UIManager = GetComponentInChildren<UIManager>();
        
        UIManager.Init();
    }

    private void Start()
    {
        StageLevel = 0;
        
        StartStage(0);
        
        UIManager.UpdateGoldText(Gold);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ClearedStage();
        }
    }


    public void AddGold(int amount)
    {
        Gold += amount;
        
        UIManager.UpdateGoldText(Gold);
    }
    
    
    public void FailedStage()
    {
        StartStage(StageLevel);
    }

    public void ClearedStage()
    {
        var clearedStageData = GameData.StageDatas.FirstOrDefault((data)=> data.Level == StageLevel);

        List<Item> items = GetRewardItems(clearedStageData);

        for (int i = 0; i < items.Count; i++)
        {
            Player.Inventory.AddItemToUnlimit(items[i].itemData, 1);
        }
        
        UIManager.ShowStageResult(items);
    }


    public void StartNextStage()
    {
        var targetStageData = GameData.StageDatas.FirstOrDefault((data)=> data.Level == StageLevel + 1);

        StageLevel = targetStageData == null ? StageLevel : StageLevel + 1;
        
        StartStage(StageLevel);
    }



    
    private void StartStage(int level)
    {
        var targetStageData = GameData.StageDatas.FirstOrDefault((data) => data.Level == level);

        if (targetStageData != null)
        {
            StageManager.CreateStage(targetStageData);
            
            UIManager.UpdateStageText(targetStageData.Level);
            
            Player.InitState();
        }
    }

    List<Item> GetRewardItems(StageData clearedStageData)
    {
        List<Item> items = new();

        for (int i = 0; i < clearedStageData.RewardInfos.Count; i++)
        {
            if (Random.value < clearedStageData.RewardInfos[i].dropChance)
            {
                items.Add(new(clearedStageData.RewardInfos[i].itemData));
            }
        }

        return items;
    }
}
