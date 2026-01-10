using UnityEngine;

public class MetalGenerator : FactoryBuilding
{
    public GameObject MetalItem;
    public int metalPerTick = 1;
    public float tickRate = 1f;
    public Vector3Int gridPos;

    public ConveyorDirection direction;
    protected override void OnBuilt()
    {
        gridPos = Vector3Int.RoundToInt(this.gameObject.transform.position);
        Debug.Log(Vector3Int.RoundToInt(this.gameObject.transform.position));
        InvokeRepeating(nameof(Produce), 0, tickRate);

    }

    void Produce()
    {
        if (FactoryGrid.Instance.HasValidPath(gridPos, out Vector3Int[] path))
        {
            Instantiate(MetalItem, gridPos, Quaternion.identity).GetComponent<MetalItem>().Init(path);
            foreach (var p in path)
                Debug.Log(p);
        }
    }
}