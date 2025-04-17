using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class InventoryItemUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public Transform parentAfterDrag;

    public InventoryItem inventoryItem;

    [HideInInspector] public TextMeshProUGUI itemAmountText;
    [HideInInspector] public Image itemImage;

    Transform playerInventoryUI;

    public void Awake()
    {
        itemImage = gameObject.GetComponent<Image>();
        itemAmountText = transform.GetComponentInChildren<TextMeshProUGUI>();
        playerInventoryUI = GameObject.FindWithTag("PlayerInventoryUI").transform;
    }

    public void Refresh(InventoryItem item)
    {
        inventoryItem = item;

        itemAmountText.text = (item is null) ? "" : inventoryItem.amount.ToString();
        itemImage.sprite = (item is null) ? null : inventoryItem.itemTemplate.sprite;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        itemImage.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(playerInventoryUI);
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        itemImage.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }

}
