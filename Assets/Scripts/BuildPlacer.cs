using UnityEngine;

public class BuildPlacer : MonoBehaviour
{
    public GameObject buildingPrefab;
    public GameObject beltPrefab;
    public float gridSize = 1f;

    public int mode = 0;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (mode == 0)
            {
                mode = 1;
            }
            else
            {
                mode = 0;
            }
        }
        if (!Input.GetMouseButtonDown(0)) return;

        Vector3 mouseWorld =
            Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 snapped = SnapToGrid(mouseWorld);

        if (Input.GetMouseButtonDown(0))
        {
            if (mode == 0)
            {
                Instantiate(buildingPrefab, snapped, Quaternion.identity).GetComponent<FactoryBuilding>()
                .Build(); ;
            }
            else
            {
                Instantiate(beltPrefab, snapped, Quaternion.identity);
            }
        } 
    }

    Vector3 SnapToGrid(Vector3 pos)
    {
        pos.x = Mathf.Round(pos.x / gridSize) * gridSize;
        pos.y = Mathf.Round(pos.y / gridSize) * gridSize;
        pos.z = 0;
        return pos;
    }
}