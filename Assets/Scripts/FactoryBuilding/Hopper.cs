using UnityEngine;

public class Hopper : MonoBehaviour
{

    public Vector3Int gridPos;
    void Start()
    {
        gridPos = Vector3Int.RoundToInt(this.gameObject.transform.position);
        FactoryGrid.Instance.RegisterHopper(gridPos, this);
    }
}
