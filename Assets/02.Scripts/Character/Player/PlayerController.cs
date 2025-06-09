using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{
    [SerializeField] private CharacterData playerData;

    private GameManager _gameManager;

    private Stat _curHealth;
    private Stat _maxHealth;
    
    
    protected override void Awake()
    {
        base.Awake();
        
        StatHandler.Init(playerData.Stats);
        
        if (StatHandler.TryGetStat(StatType.CurHealth, out Stat currentHealth))
        {
            _curHealth = currentHealth;
            
            if (StatHandler.TryGetStat(StatType.MaxHealth, out Stat maxHealth))
            {
                _maxHealth = maxHealth;
            }
        }
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
  
    public void Init(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void StartStage()
    {
        NavAgent.enabled = false;
        
        transform.position = Vector3.zero;

        _curHealth.ModifyStatValue(_maxHealth.value);
        
        NavAgent.enabled = true;
    }

}
