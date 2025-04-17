using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public InventorySlot[] slots;
    public int size;

    public event Action<Inventory> OnInventoryChange;

    public void Awake()
    {
        InitializeInventory();
    }

    public void InitializeInventory()
    {
        slots = new InventorySlot[size];
        for (int i = 0; i < size; i++)
        {
            InventorySlot newSlot = new InventorySlot(this);
            slots[i] = newSlot;
        }
    }

    //Add new item to inventory
    public int AddItem(InventoryItemSO itemToAdd, int amount)
    {
        int remainder = amount;
        //fill stacks
        for (int i = 0; i < size; i++)
        {

            if (slots[i].item?.itemTemplate.id == itemToAdd.id)
            {
                if (remainder == 0)
                {
                    break;
                }
                int availableSlotAmount = itemToAdd.stacksize - slots[i].item.amount;
                int addedAmount = (int)MathF.Min(remainder, availableSlotAmount);
                slots[i].item.amount += addedAmount;
                remainder -= addedAmount;

            }
        }

        //Add to empty slots
        for (int i = 0; i < size; i++)
        {
            if (remainder == 0)
            {
                break;
            }

            if (slots[i].item is null)
            {

                int availableSlotAmount = itemToAdd.stacksize - 0;
                int addedAmount = (int)MathF.Min(remainder, availableSlotAmount);

                slots[i].item = new InventoryItem(itemToAdd, addedAmount);
                remainder -= addedAmount;

            }
        }
        OnInventoryChange(this);
        return remainder;
    }

    public bool CheckHasItem(InventoryItemSO itemToRemove, int amount)
    {
        int remainder = amount;
        for (int i = 0; i < size; i++)
        {

            if (slots[i].item?.itemTemplate?.id == itemToRemove.id)
            {
                remainder -= slots[i].item.amount;
            }
            if (remainder <= 0)
            {
                return true;
            }
        }

        return false;
    }

    public int RemoveItem(InventoryItemSO itemToRemove, int amount)
    {
        int remainder = amount;
        for (int i = 0; i < size; i++)
        {
            if (remainder == 0)
            {
                break;
            }

            if (slots[i].item?.itemTemplate.id == itemToRemove.id)
            {
                int availableSlotAmount = slots[i].item.amount;
                int removedAmount = (int)MathF.Min(remainder, availableSlotAmount);
                slots[i].item.amount -= removedAmount;
                if (slots[i].item.amount == 0)
                {
                    slots[i].item = null;
                }
                remainder -= removedAmount;
            }
        }

        OnInventoryChange(this);
        return remainder;

    }

    public bool MoveItem(InventorySlot slotFrom, InventorySlot slotTo)
    {

        if (slotFrom.item is null)
        {
            return false;
        }
        else if (slotTo.item is null)
        {
            slotTo.item = slotFrom.item;
            slotFrom.item = null;
        }
        else if (slotFrom.item.itemTemplate.id == slotTo.item.itemTemplate.id)
        {
            int availableAmountToAdd = slotTo.item.itemTemplate.stacksize - slotTo.item.amount;
            if (availableAmountToAdd == 0)
            {
                return false;
            }
            int addedAmount = Mathf.Min(slotFrom.item.amount, availableAmountToAdd);
            slotTo.item.amount += addedAmount;
            slotFrom.item.amount -= addedAmount;
            if (slotFrom.item.amount == 0)
            {
                slotFrom.item = null;
            }

        }

        OnInventoryChange(this);
        return true;
    }

    public int AddItemToSlot(InventorySlot targetSlot, InventoryItemSO itemToAdd, int amount)
    {
        if (targetSlot.item?.itemTemplate.id != itemToAdd.id)
        {
            return amount;
        }
        int remainder = amount;

        int availableSlotAmount = (targetSlot.item is null) ? itemToAdd.stacksize : itemToAdd.stacksize - targetSlot.item.amount;
        int addedAmount = (int)MathF.Min(remainder, availableSlotAmount);
        targetSlot.item.amount += addedAmount;
        remainder -= addedAmount;

        OnInventoryChange(this);
        return remainder;
    }

    public int RemoveItemFromSlot(InventorySlot targetSlot, InventoryItemSO itemToRemove, int amount)
    {
        if (targetSlot.item?.itemTemplate.id != itemToRemove.id)
        {
            return amount;
        }
        int remainder = amount;

        int availableSlotAmount = targetSlot.item.amount;
        int removedAmount = (int)MathF.Min(remainder, availableSlotAmount);
        targetSlot.item.amount -= removedAmount;
        if (targetSlot.item.amount == 0)
        {
            targetSlot.item = null;
        }
        remainder -= removedAmount;

        OnInventoryChange(this);
        return remainder;

    }

    public void InsertItemToSlot(InventoryItem item, InventorySlot slot)
    {
        slot.item = item;
        OnInventoryChange(this);
    }

    // //get item from selected slot
    // // public InventoryItem GetSelectedItem(bool is_removed)
    // // {
    // //     InventorySlot slot = GetSelectedInventorySlot();
    // //     InventoryItem itemInSlot = slot.inventoryItem;

    // //     itemInSlot.Use()
    // //     // if (is_removed)
    // //     // {
    // //     //     itemInSlot.count -= 1;
    // //     //     if (itemInSlot.count <= 0)
    // //     //     {
    // //     //         Destroy(itemInSlot.gameObject);
    // //     //     }
    // //     //     else
    // //     //     {
    // //     //         itemInSlot.RefreshItemUI();
    // //     //     }
    // //     // }

    // //     // return itemInSlot;
    // // }

    // public bool TryUseSelectedSlot()
    // {
    //     InventorySlot slot = GetSelectedInventorySlot();
    //     slot?.inventoryItem?.Use();

    // }

    // public void TryDropSelectedSlot()
    // {

    // }

    // }

    // InventorySlot GetSelectedInventorySlot()
    // {
    //     return selectedSlot >= 0 ? inventorySlots[selectedSlot] : null;
    // }

}