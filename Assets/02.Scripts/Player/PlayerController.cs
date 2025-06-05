using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent _agent;

    private Stage _targetStage;
    
    
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
    

    private void Update()
    {
        if (_targetStage != null)
        {
            var currentEnemies = _targetStage.CurrentRoom.Enemies;
            
            if (currentEnemies.Count > 0)
            {
                Enemy targetEnemy = GetClosestEnemy(_targetStage.CurrentRoom);
                
                _agent.SetDestination(targetEnemy.transform.position);

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    targetEnemy.Die();
                }
            }   
        }
    }


    public void StartStage(Stage stage)
    {
        _targetStage = stage;

        transform.position = _targetStage.CurrentRoom.transform.position;
    }


    public Enemy GetClosestEnemy(Room currentRoom)
    {
        Enemy closestEnemy = null;

        float closestDist = float.MaxValue;
        
        for (int i = 0; i < currentRoom.Enemies.Count; i++)
        {
            float currentDist = (currentRoom.Enemies[i].transform.position - transform.position).sqrMagnitude;
            
            if (currentDist < closestDist)
            {
                closestDist = currentDist;

                closestEnemy = currentRoom.Enemies[i];
            }
        }
        
        return closestEnemy;
    }
}
