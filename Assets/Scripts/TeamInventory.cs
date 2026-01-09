using UnityEngine;

public class TeamInventory : MonoBehaviour
{
    public static TeamInventory Instance;

    public int metal;

    void Awake()
    {
        Instance = this;
    }

    public void AddMetal(int amount)
    {
        metal += amount;
        Debug.Log("Metal: " + metal);
    }

    public bool SpendMetal(int amount)
    {
        if (metal < amount) return false;
        metal -= amount;
        return true;
    }
}