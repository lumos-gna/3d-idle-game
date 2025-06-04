using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
   private GameManager _gameManager;

   public void Initialize(GameManager gameManager)
   {
      _gameManager = gameManager;
   }
   
   public void CreateStage(StageData stageData)
   {
      List<Vector2Int> roomTree = CreateRoomTree(stageData);

      CreateRooms(roomTree, stageData);
      CreatePath(roomTree, stageData);
   }


   private List<Vector2Int> CreateRoomTree(StageData stageData)
   {
      List<Vector2Int> newTree = new()
      {
         Vector2Int.zero
      };
      
      Vector2Int[] directions4 = new[]
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
            int randCount = UnityEngine.Random.Range(0, directions4.Length);
            
            Vector2Int targetNode =  newTree[^1] + directions4[randCount];

            if (!newTree.Contains(targetNode))
            {
               newTree.Add(targetNode);

               break;
            }
         }
      }

      return newTree;
   }

   void CreateRooms(List<Vector2Int> roomTree, StageData stageData)
   {
      for (int i = 0; i < roomTree.Count; i++)
      {
         var createRoom = Instantiate(stageData.RoomPrefab);

         Vector3 placePos = 
            new Vector3(roomTree[i].x, 0, roomTree[i].y) * (stageData.RoomSize.x + stageData.PathSize.x);

         createRoom.transform.position = placePos;
      }
   }

   void CreatePath(List<Vector2Int> roomTree, StageData stageData)
   {
      for (int i = 0; i < roomTree.Count - 1; i++)
      {
         var createPath = Instantiate(stageData.PathPrefab);
         
         Vector2 centerPos = (Vector2)(roomTree[i + 1] + roomTree[i])  * 0.5f;

         createPath.transform.position = new Vector3(centerPos.x, 0, centerPos.y) * (stageData.RoomSize.x + stageData.PathSize.x);
         
         
         Vector2 delta = roomTree[i + 1] - roomTree[i];
         
         if (delta.y != 0) 
         {
            createPath.transform.rotation = Quaternion.Euler(0, 90f, 0);
         }
      }
   }
}
