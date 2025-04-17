using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItemSO", menuName = "Scriptable Objects/InventoryItemSO")]
public class InventoryItemSO : ScriptableObject
{
    public string id;
    public GameObject scenePrefab;
    public Sprite sprite;
    public int stacksize = 1;
}
