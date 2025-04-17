using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class InventoryItem
{

    public InventoryItemSO itemTemplate;
    public int amount;

    public InventoryItem(InventoryItemSO itemTemplate, int amount)
    {
        this.itemTemplate = itemTemplate;
        this.amount = amount;
    }

}