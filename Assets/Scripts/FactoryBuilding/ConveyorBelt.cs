using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public ConveyorDirection direction;
    public Vector3Int gridPos;
    void Start()
    {
        gridPos = Vector3Int.RoundToInt(this.gameObject.transform.position);
        FactoryGrid.Instance.RegisterConveyor(gridPos, this);
        Debug.Log(gridPos);
    }
}