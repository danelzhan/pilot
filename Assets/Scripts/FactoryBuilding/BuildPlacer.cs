using UnityEngine;

public class BuildPlacer : MonoBehaviour
{
    public GameObject buildingPrefab;
    public GameObject beltUpPrefab;
    public GameObject beltDownPrefab;
    public GameObject beltLeftPrefab;
    public GameObject beltRightPrefab;
    public GameObject hopperPrefab;

    public float gridSize = 1f;

    public int mode = 0;

    void Update()
    {
        SwitchMode();

        if (!Input.GetMouseButtonDown(0)) return;

        Vector3 mouseWorld =
            Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 snapped = SnapToGrid(mouseWorld);

        if (Input.GetMouseButtonDown(0))
        {
            switch (mode)
            {
                case 0:
                    Instantiate(buildingPrefab, snapped, Quaternion.identity).GetComponent<FactoryBuilding>()
                        .Build();
                    break;
                case 1:
                    Instantiate(beltUpPrefab, snapped, Quaternion.identity);
                    break;
                case 2:
                    Instantiate(beltDownPrefab, snapped, Quaternion.identity);
                    break;
                case 3:
                    Instantiate(beltLeftPrefab, snapped, Quaternion.identity);
                    break;
                case 4:
                    Instantiate(beltRightPrefab, snapped, Quaternion.identity);
                    break;
                case 5:
                    Instantiate(hopperPrefab, snapped, Quaternion.identity);
                    break;
                default:
                    Instantiate(beltUpPrefab, snapped, Quaternion.identity);
                    break;
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

    // Just for rapid prototyping purposes
    private void SwitchMode()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            mode = 0;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            mode = 1;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            mode = 2;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            mode = 3;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            mode = 4;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            mode = 5;
        }
    }
}