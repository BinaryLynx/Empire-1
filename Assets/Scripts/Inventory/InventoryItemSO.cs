using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItemSO", menuName = "Scriptable Objects/InventoryItemSO")]
public class InventoryItemSO : ScriptableObject
{
    //inventory
    public string id;
    public Sprite sprite;
    public int stacksize = 1;

    //additional info
    public string itemName;
    public string description;
    public StringVariable itemType;

    //usable system
    public UsableItemSO usableItem;
}
