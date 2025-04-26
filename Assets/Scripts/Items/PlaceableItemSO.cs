using UnityEngine;

[CreateAssetMenu(fileName = "PlaceableItemSO", menuName = "Scriptable Objects/PlaceableItemSO")]
public class PlaceableItemSO : UsableItemSO
{
    public Vector2Int gridSize;
    public GameObject previewPrefab;
    public GameObject scenePrefab;
}