using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
   [SerializeField] private Stage stagePrefab;

   private Stage _currentStage;

   public void CreateStage(StageData stageData, List<EnemyData> enemyDataList)
   {
      if (_currentStage != null)
      {
         Destroy(_currentStage.gameObject);
      }
      
      List<Vector2Int> roomCells = CreateRoomCells(stageData);

      _currentStage = Instantiate(stagePrefab);
      
      _currentStage.Init(stageData, enemyDataList, roomCells);
   }

   private List<Vector2Int> CreateRoomCells(StageData stageData)
   {
      List<Vector2Int> newCells = new()
      {
         Vector2Int.zero
      };
      
      Vector2Int[] directions4 = 
      {
         Vector2Int.down,
         Vector2Int.left,
         Vector2Int.right,
         Vector2Int.up
      };
      
      for (int count = 0; count < stageData.MaxRoomCount; count++)
      {
         for (int dirIndex = 0; dirIndex < directions4.Length; dirIndex++)
         {
            int randCount = Random.Range(0, directions4.Length);
            
            Vector2Int targetNode =  newCells[^1] + directions4[randCount];

            if (!newCells.Contains(targetNode))
            {
               newCells.Add(targetNode);

               break;
            }
         }
      }

      return newCells;
   }

  

  
}
