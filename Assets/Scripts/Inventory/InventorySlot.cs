using System;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class InventorySlot
{
    [SerializeReference] public InventoryItem item;
    public Inventory inventory;

    public InventorySlot(Inventory inventory)
    {
        this.inventory = inventory;

    }
}