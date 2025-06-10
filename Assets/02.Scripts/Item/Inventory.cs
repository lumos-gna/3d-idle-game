using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    private Dictionary<ItemType, List<Item>> _totalItems = new();
    
    private Dictionary<ItemType, Item> _equippedItems = new();


    private GameManager _gameManager;
    private StatHandler _statHandler;
    
    
    private void Awake()
    {
        _gameManager = GameManager.Instance;

        _statHandler = GetComponent<StatHandler>();
    }


    public void AddItem(Item item)
    {
        ItemType itemType = item.itemData.Type;
        
        if (!_totalItems.ContainsKey(itemType))
        {
            _totalItems[itemType] = new();

            EquipItem(item);
        }
        
        _totalItems[itemType].Add(item);
    }
    
    
    public void RemoveItem(Item item)
    {
        ItemType itemType = item.itemData.Type;
        
        if (_totalItems.ContainsKey(itemType))
        {
            if (item.count > 1)
            {
                item.count--;

                return;
            }
            
            _totalItems[itemType].Remove(item);

            if (_equippedItems[itemType] == item)
            {
                _equippedItems[itemType] = null;
            }
        }
    }

    public void EquipItem(Item item)
    {
        ItemType itemType = item.itemData.Type;

        if (_equippedItems.ContainsKey(itemType))
        {
            StatData[] removeStatDatas = _equippedItems[itemType].itemData.StatDatas;
            
            for (int i = 0; i < removeStatDatas.Length; i++)
            {
                if (_statHandler.TryGetStat(removeStatDatas[i].type, out Stat stat))
                {
                    stat.ModifyStatValue(removeStatDatas[i].baseValue * -1);
                }
            }
        }
        
        _equippedItems[itemType] = item;
        
        StatData[] addStatDatas = _equippedItems[itemType].itemData.StatDatas;
            
        for (int i = 0; i < addStatDatas.Length; i++)
        {
            if (_statHandler.TryGetStat(addStatDatas[i].type, out Stat stat))
            {
                stat.ModifyStatValue(addStatDatas[i].baseValue);
            }
        }
    }
    

    public void SellItem(Item item)
    {
        _gameManager.AddGold(item.itemData.Price);
                

        RemoveItem(item);
    }


    public List<Item> GetItems(ItemType type)
    {
        List<Item> items = null;

        if (_totalItems.ContainsKey(type))
        {
            if (_totalItems[type].Count > 0)
            {
                items = _totalItems[type];
            }
        }
        
        return items;
    }

    public Item GetEquippedItem(ItemType type)
    {
        Item item = null;
        
        if (_equippedItems.ContainsKey(type))
        {
            item = _equippedItems[type];
        }
        
        return item;
    }
}