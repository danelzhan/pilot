using UnityEngine;

public class MetalGenerator : FactoryBuilding
{
    public int metalPerTick = 1;
    public float tickRate = 1f;

    public ConveyorDirection direction;
    protected override void OnBuilt()
    {
        Debug.Log(Vector3Int.RoundToInt(this.gameObject.transform.position));
        InvokeRepeating(nameof(Produce), 0, tickRate);

    }

    void Produce()
    {
        if (FactoryGrid.Instance.HasValidPath(Vector3Int.RoundToInt(this.gameObject.transform.position), new Vector3Int(0,0,0)))
        {
            TeamInventory.Instance.AddMetal(metalPerTick);
        }
    }
}