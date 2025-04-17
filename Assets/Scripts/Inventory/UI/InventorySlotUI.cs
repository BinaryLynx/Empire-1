using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour, IDropHandler
{

    [HideInInspector] public Image slotImage; // Backgorund

    public InventorySlot inventorySlot;
    public GameObject inventoryItemUIPrefab;
    GameObject itemInSlotUI;
    Inventory playerInventory;

    public void Awake()
    {
        slotImage = gameObject.GetComponent<Image>();
        playerInventory = GameObject.FindWithTag("Player").GetComponent<Inventory>();
    }

    public void Refresh()
    {
        //TODO вместо того чтобы всегда пересоздавать, написать логику чтобы просто менять значения.

        if (itemInSlotUI is not null)
        {
            Destroy(itemInSlotUI);
        }

        InventoryItem item = inventorySlot.item;
        if (item is null)
        {
            return;
        }

        GameObject itemGO = Instantiate(inventoryItemUIPrefab, transform);
        InventoryItemUI inventoryItemUI = itemGO.GetComponent<InventoryItemUI>();
        itemInSlotUI = itemGO;
        inventoryItemUI.Refresh(item);
    }

    public void OnDrop(PointerEventData eventData)
    {
        InventoryItemUI inventoryItemUI = eventData.pointerDrag.GetComponent<InventoryItemUI>();
        InventorySlot slotFrom = inventoryItemUI.parentAfterDrag.GetComponent<InventorySlotUI>().inventorySlot;
        InventorySlot slotTo = transform.GetComponent<InventorySlotUI>().inventorySlot;

        if (slotFrom.inventory == slotTo.inventory)
        {
            slotFrom.inventory.MoveItem(slotFrom, slotTo);
        }
        else
        {
            InventoryManager.Instance.TradeItems(slotFrom, slotTo);
        }

        // if (is_success)
        // {
        //     inventoryItemUI.parentAfterDrag = transform;
        // }

    }

}
