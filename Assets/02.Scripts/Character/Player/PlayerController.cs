using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{
    public Inventory Inventory { get; private set; }
    public EquipmentHandler EquipmentHandler => equipmentHandler;
    
    [SerializeField] private EquipmentHandler equipmentHandler;

    [SerializeField] private CharacterData playerData;
    
    private GameManager _gameManager;
    private Stat _curHealth;
    private Stat _maxHealth;
    

    public void Init(GameManager gameManager)
    {
        _gameManager = gameManager;
        
        Inventory = new();

        StatHandler.Init(playerData.Stats);
        
        if (StatHandler.TryGetStat(StatType.CurHealth, out Stat currentHealth))
        {
            _curHealth = currentHealth;
            
            if (StatHandler.TryGetStat(StatType.MaxHealth, out Stat maxHealth))
            {
                _maxHealth = maxHealth;
            }
        }
        
        
        Inventory.OnAddItem += (item) =>
        {
            if (equipmentHandler.GetItem(item.itemData.Type) == null)
            {
                equipmentHandler.Equip(item);
            }
        };

        equipmentHandler.OnEquip += (item) =>
        {
            foreach (var itemStat in item.itemData.StatDatas)
            {
                if (StatHandler.TryGetStat(itemStat.type, out Stat stat))
                {
                    stat.ModifyStatValue(itemStat.baseValue);
                }
            }
        };
        
        equipmentHandler.OnUnequip += (item) =>
        {
            foreach (var itemStat in item.itemData.StatDatas)
            {
                if (StatHandler.TryGetStat(itemStat.type, out Stat stat))
                {
                    stat.ModifyStatValue(itemStat.baseValue * -1);
                }
            }
        };
    }

    public override CharacterController GetTargetController()
    {
        Enemy closerEnemy = null;

        float closestDist = float.MaxValue;
        
        for (int i = 0; i < CurrentRoom.Enemies.Count; i++)
        {
            float currentDist = (CurrentRoom.Enemies[i].transform.position - transform.position).sqrMagnitude;
            
            if (currentDist < closestDist)
            {
                closestDist = currentDist;

                closerEnemy = CurrentRoom.Enemies[i];
            }
        }
        
        return closerEnemy;
    }
    
    protected override void Die()
    {
        _gameManager.FailedStage();
    }
  
  

    public void InitState()
    {
        NavAgent.enabled = false;
        
        transform.position = Vector3.zero;

        _curHealth.ModifyStatValue(_maxHealth.value);
        
        NavAgent.enabled = true;
    }

}
