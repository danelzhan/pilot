using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FactoryGrid : MonoBehaviour
{
    public static FactoryGrid Instance;
    public Tilemap factoryTilemap;

    Dictionary<Vector3Int, FactoryBuilding> buildings = new();
    Dictionary<Vector3Int, ConveyorBelt> conveyors = new();
    Dictionary<Vector3Int, Hopper> hoppers = new();

    void Awake()
    {
        Instance = this;
    }

    public void RegisterBuilding(Vector3Int pos, FactoryBuilding building)
    {
        buildings[pos] = building;
    }

    public void RegisterConveyor(Vector3Int pos, ConveyorBelt belt)
    {
        conveyors[pos] = belt;
    }
    public void RegisterHopper(Vector3Int pos, Hopper hopper)
    {
        hoppers[pos] = hopper;
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
    public bool HasValidPath(Vector3Int start, out Vector3Int[] path)
    {
        path = null;

        foreach (KeyValuePair<Vector3Int, Hopper> pair in hoppers)
        {
            HashSet<Vector3Int> visited = new();
            Queue<Vector3Int> queue = new();
            Dictionary<Vector3Int, Vector3Int> cameFrom = new();

            Vector3Int[] startNeighbors =
            {
            start + Vector3Int.up,
            start + Vector3Int.down,
            start + Vector3Int.left,
            start + Vector3Int.right
        };

            foreach (var neighbor in startNeighbors)
            {
                queue.Enqueue(neighbor);
                cameFrom[neighbor] = start;
            }

            while (queue.Count > 0)
            {
                Vector3Int current = queue.Dequeue();

                if (visited.Contains(current)) continue;
                visited.Add(current);

                if (current == pair.Key)
                {
                    path = ReconstructPath(start, current, cameFrom);
                    return true;
                }

                ConveyorBelt belt = GetConveyor(current);
                if (belt == null) continue;

                Vector3Int next = current + DirToOffset(belt.direction);

                if (visited.Contains(next)) continue;

                cameFrom[next] = current;
                queue.Enqueue(next);
            }
        }

        return false;
    }
    Vector3Int[] ReconstructPath(
    Vector3Int start,
    Vector3Int end,
    Dictionary<Vector3Int, Vector3Int> cameFrom)
    {
        List<Vector3Int> path = new();
        Vector3Int current = end;

        path.Add(current);

        while (current != start)
        {
            current = cameFrom[current];
            path.Add(current);
        }

        path.Reverse();
        return path.ToArray();
    }

    Vector3Int DirToOffset(ConveyorDirection dir)
    {
        return dir switch
        {
            ConveyorDirection.Up => Vector3Int.up,
            ConveyorDirection.Down => Vector3Int.down,
            ConveyorDirection.Left => Vector3Int.left,
            ConveyorDirection.Right => Vector3Int.right,
            _ => Vector3Int.zero
        };
    }

    void DebugPrintPath(Vector3[] path)
    {
        string result = "Conveyor Path:\n";
        for (int i = 0; i < path.Length; i++)
        {
            result += $"{i}: {path[i]}\n";
        }
        Debug.Log(result);
    }
}