using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int RoomIndex { get; private set; }
    public Vector2Int Size { get; private set; }
    public List<Enemy> Enemies { get; private set; } = new();

    public void Initialize(int roomIndex, Vector2Int roomSize)
    {
        RoomIndex = roomIndex;

        Size = roomSize;
    }
}