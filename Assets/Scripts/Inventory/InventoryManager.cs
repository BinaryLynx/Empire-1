using Unity.VisualScripting;
using UnityEngine;

//Между разными инвентарями
public class InventoryManager : Singleton<InventoryManager>
{

    protected override void Awake()
    {
        base.Awake();
    }

    public void TradeItems(InventorySlot slotFrom, InventorySlot slotTo)
    {

        if (slotFrom.item is null)
        {
            return;
        }

        if (slotFrom.item?.itemTemplate.id == slotTo.item?.itemTemplate.id)
        {
            //Добавить сколько добавится, остаток оставить
            int remainder = slotTo.inventory.AddItemToSlot(slotTo, slotFrom.item.itemTemplate, slotFrom.item.amount);
            int addedAmount = slotFrom.item.amount - remainder;
            slotFrom.inventory.RemoveItemFromSlot(slotFrom, slotFrom.item.itemTemplate, addedAmount);

        }
        else
        {
            // Поменять местами
            InventoryItem temp = slotTo.item;
            slotTo.inventory.InsertItemToSlot(slotFrom.item, slotTo);
            slotFrom.inventory.InsertItemToSlot(temp, slotFrom);
        }
    }

    // InventorySlotUI[] inventorySlots;
    // public GameObject inventoryItemPrefabUI;
    // int selectedSlot = -1;

    // public void ChangeSelectedSlot(int newValue)
    // {
    //     if (selectedSlot >= 0)
    //     {
    //         getSelectedInventorySlot().Deselect();
    //     }

    //     inventorySlots[newValue].Select();
    //     selectedSlot = newValue;
    // }

    // public bool GetItem(InventoryItemSO newItem)
    // {
    //     InventorySlotUI slot = FindSlotForItem(newItem);
    //     if (slot is not null)
    //     {
    //         AddItemToSlot(newItem, slot);
    //         return true;
    //     }
    //     else
    //     {
    //         return false;
    //     }

    // }

    // InventorySlotUI FindSlotForItem(InventoryItemSO newItem)
    // {
    //     InventorySlotUI slot = null;
    //     for (int i = 0; i > inventorySlots.Length; i++)
    //     {
    //         slot = inventorySlots[i];
    //         InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
    //         if (itemInSlot is null || (itemInSlot.itemSO == newItem && itemInSlot.itemSO.stacksize >= itemInSlot.count + 1)) //!newItem.count
    //         {
    //             return slot;
    //         }
    //     }
    //     return slot;

    // }

    // void AddItemToSlot(InventoryItemSO itemSO, InventorySlotUI slot)
    // {
    //     GameObject newItemGO = Instantiate(inventoryItemPrefabUI, slot.transform);
    //     InventoryItem inventoryItem = newItemGO.GetComponent<InventoryItem>();
    //     inventoryItem.GetItem(itemSO);

    // }

    // InventorySlotUI getSelectedInventorySlot()
    // {
    //     return inventorySlots[selectedSlot];
    // }

    // public InventoryItem getSelectedItem(bool is_removed)
    // {
    //     InventorySlotUI slot = getSelectedInventorySlot();
    //     InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

    //     if (is_removed)
    //     {
    //         itemInSlot.count -= 1;
    //         if (itemInSlot.count <= 0)
    //         {
    //             Destroy(itemInSlot.gameObject);
    //         }
    //         else
    //         {
    //             itemInSlot.RefreshItemUI();
    //         }
    //     }

    //     return itemInSlot;
    // }
}
