using UnityEngine;

public class PlayerBuild : MonoBehaviour
{
    public Grid grid;
    public LayerMask placementAreaLayer;
    public LayerMask placedItemsLayer;
    private float currentRotation = 0f;

    //Preview
    private Vector3Int lastCellPos;
    public GameObject previewObject;

    //magic vars
    public float rotationAngle = 90f;
    int rayLength = 10;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            RemoveItemFromGrid();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            //add logic to check for item in hand
            ChangePlacementRotation();
        }

    }

    public void ChangePlacementRotation()
    {
        currentRotation = (currentRotation + rotationAngle) % 360;
    }

    public bool PlaceItemOnGrid(PlaceableItemSO placeableItemSO)
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(ray, out RaycastHit hit, rayLength, placementAreaLayer))
        {
            // Get the grid cell position
            Vector3Int cellPosition = grid.WorldToCell(hit.point);
            Vector3 placementPosition = grid.CellToWorld(cellPosition);

            // Check if position is occupied
            if (ValidatePlacementWithRays(cellPosition, placeableItemSO.gridSize))
            {
                InstantiateWithRotation(placeableItemSO.scenePrefab, placementPosition);
            }
            return true;
        }
        return false;
    }

    GameObject InstantiateWithRotation(GameObject itemPrefab, Vector3 placementPosition)
    {
        GameObject placedItem = Instantiate(itemPrefab, placementPosition, Quaternion.identity);
        placedItem.transform.GetChild(0).Rotate(new Vector3(0, currentRotation, 0));
        return placedItem;
    }

    // void RotatePreview

    void RemoveItemFromGrid()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayLength, placedItemsLayer))
        {
            Destroy(hit.collider.gameObject);

        }
    }

    public bool ValidatePlacementWithRays(Vector3Int originCell, Vector2Int itemSize)
    {
        for (int x = 0; x < itemSize.x; x++)
        {
            for (int y = 0; y < itemSize.y; y++)
            {
                Vector3Int currentCell = originCell + new Vector3Int(x, 0, y);
                if (!SingleCellRayCheck(currentCell))
                {
                    return false;
                }
            }
        }
        return true;
    }

    private bool SingleCellRayCheck(Vector3Int cell)
    {
        Vector3 worldPos = grid.GetCellCenterWorld(cell);
        Ray ray = new Ray(worldPos + Vector3.up * 5f, Vector3.down);

        // First check for ground
        if (!Physics.Raycast(ray, out RaycastHit groundHit, rayLength, placementAreaLayer))
        {
            Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red, 2f);
            return false;
        }

        // Then check for obstacles between the ray origin and ground
        float obstacleCheckDistance = Vector3.Distance(ray.origin, groundHit.point);
        if (Physics.Raycast(ray, obstacleCheckDistance, placedItemsLayer))
        {
            Debug.DrawRay(ray.origin, ray.direction * obstacleCheckDistance, Color.yellow, 2f);
            return false;
        }

        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.green, 2f);
        return true;
    }

    public void UpdatePreview(GameObject previewObject)
    {

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayLength, placementAreaLayer))
        {
            Vector3Int cellPos = grid.WorldToCell(hit.point);
            if (cellPos != lastCellPos)
            {
                if (this.previewObject != previewObject)
                {
                    this.previewObject = InstantiateWithRotation(previewObject, grid.CellToWorld(cellPos));
                }
                lastCellPos = cellPos;
            }
        }
    }
}