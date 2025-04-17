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
        OnSelectedSlotChanged(oldSlotIndex, selectedSlotIndex);
        return selectedSlotIndex;
    }

}