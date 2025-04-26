using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : Inventory
{
    public int selectedSlotIndex = -1;

    public event Action<int, int> OnSelectedSlotChanged;

    //move to new script playerInventory
    public int SelectSlot(int newSlotIndex)
    {
        if (newSlotIndex == selectedSlotIndex)
        {
            return selectedSlotIndex;
        }

        if (newSlotIndex >= size)
        {
            return selectedSlotIndex;
        }

        int oldSlotIndex = selectedSlotIndex;
        selectedSlotIndex = newSlotIndex;
        // slots[newSlotIndex].Preview();
        OnSelectedSlotChanged(oldSlotIndex, selectedSlotIndex);
        return selectedSlotIndex;
    }

    // public void Update()
    // {

    // }

    public InventoryItem GetSelectedItem()
    {
        return (selectedSlotIndex == -1) ? null : slots[selectedSlotIndex].item;
    }

}