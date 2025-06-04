using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class StageManager : MonoBehaviour
{
   [SerializeField] private GameObject roomPrefab;
   [SerializeField] private GameObject pathPrefab;

   [SerializeField] private int maxRoomCount;
   
   [SerializeField] private int roomLength;
   [SerializeField] private int pathLength;
   
   private void Start()
   {
      List<Vector2Int> roomTree = CreateRoomTree();

      CreateRooms(roomTree);
      CreatePath(roomTree);
   }

   private List<Vector2Int> CreateRoomTree()
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
      
      for (int count = 0; count < maxRoomCount; count++)
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

   void CreateRooms(List<Vector2Int> roomTree)
   {
      for (int i = 0; i < roomTree.Count; i++)
      {
         var createRoom = Instantiate(roomPrefab);

         Vector3 placePos = new Vector3(roomTree[i].x, 0, roomTree[i].y) * (roomLength + pathLength);

         createRoom.transform.position = placePos;
      }
   }

   void CreatePath(List<Vector2Int> roomTree)
   {
      for (int i = 0; i < roomTree.Count - 1; i++)
      {
         var createPath = Instantiate(pathPrefab);
         
         Vector2 centerPos = (Vector2)(roomTree[i + 1] + roomTree[i])  * 0.5f;

         createPath.transform.position = new Vector3(centerPos.x, 0, centerPos.y) * (roomLength + pathLength);
         
         
         Vector2 delta = roomTree[i + 1] - roomTree[i];
         
         if (delta.y != 0) 
         {
            createPath.transform.rotation = Quaternion.Euler(0, 90f, 0);
         }
      }
   }
}
