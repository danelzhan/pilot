using System.Collections.Generic;
using UnityEngine;

public class FactoryGrid : MonoBehaviour
{
    public static FactoryGrid Instance;

    Dictionary<Vector3Int, ConveyorBelt> conveyors = new();
    Dictionary<Vector3Int, FactoryBuilding> buildings = new();

    void Awake()
    {
        Instance = this;
    }

    public void RegisterConveyor(Vector3Int pos, ConveyorBelt belt)
    {
        conveyors[pos] = belt;
    }

    public ConveyorBelt GetConveyor(Vector3Int pos)
    {
        conveyors.TryGetValue(pos, out var belt);
        return belt;
    }

    public FactoryBuilding GetBuilding(Vector3Int pos)
    {
        buildings.TryGetValue(pos, out var b);
        return b;
    }

    public bool HasValidPath(Vector3Int start, Vector3Int end)
    {
        HashSet<Vector3Int> visited = new();
        Queue<Vector3Int> queue = new();

        queue.Enqueue(start + Vector3Int.up);
        queue.Enqueue(start + Vector3Int.down);
        queue.Enqueue(start + Vector3Int.left);
        queue.Enqueue(start + Vector3Int.right);

        while (queue.Count > 0)
        {
            Vector3Int current = queue.Dequeue();
            if (current == end) return true;

            if (visited.Contains(current)) continue;
            visited.Add(current);

            ConveyorBelt belt = GetConveyor(current);
            if (belt == null) continue;

            Vector3Int next = current + DirToOffset(belt.direction);
            queue.Enqueue(next);
        }

        return false;
    }
    Vector3Int DirToOffset(ConveyorDirection dir)
    {
        Debug.Log(dir);
        return dir switch
        {
            ConveyorDirection.Up => Vector3Int.up,
            ConveyorDirection.Down => Vector3Int.down,
            ConveyorDirection.Left => Vector3Int.left,
            ConveyorDirection.Right => Vector3Int.right,
            _ => Vector3Int.zero
        };
    }
}