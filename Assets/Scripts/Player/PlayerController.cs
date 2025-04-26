using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerInventory playerInventory;
    InventoryItem currentSelectedItem;

    PlayerLook playerLook;

    bool mouseActive = false;

    float rayDistance = 100f;
    // bool showDebugRay = true;
    int interactiveLayer;

    public event Action<GameObject> OnContainerPlayerInteract;
    public event Action OnContainerPlayerClose;

    // bool UiIsInFocus;

    public void Awake()
    {
        playerInventory = gameObject.GetComponent<PlayerInventory>();
        playerInventory.OnSelectedSlotChanged += ChangeCurrentSelectedItem;

        playerLook = gameObject.GetComponentInChildren<PlayerLook>();
        Cursor.lockState = CursorLockMode.Locked;

        interactiveLayer = LayerMask.GetMask("Interactive");

    }

    public void Update()
    {
        SelectInventorySlot();
        CheckContainerInteraction();
        ToggleInventory();
        PreviewItem();
        CheckItemActionInputs();

    }

    private void ChangeCurrentSelectedItem(int oldSlotIndex, int newSlotIndex)
    {
        currentSelectedItem = playerInventory.GetSelectedItem();
    }

    private void CheckItemActionInputs()
    {
        if (currentSelectedItem is null)
        {
            return;
        }

        List<ItemActionSO> itemActions = currentSelectedItem.itemTemplate.usableItem.GetAvailableActions(gameObject);
        foreach (ItemActionSO action in itemActions)
        {
            if (action.inputAction.action.WasPerformedThisFrame())
            {
                currentSelectedItem.itemTemplate.usableItem.Use(action.actionName, gameObject);
                break;
            }
        }
    }

    private void PreviewItem()
    {
        currentSelectedItem?.itemTemplate?.usableItem?.Preview(gameObject);
    }

    void ToggleInventory()
    {
        if (Keyboard.current.iKey.wasPressedThisFrame)
        {
            SwitchUIFocus();
        }
    }

    public void SwitchUIFocus()
    {
        mouseActive = !mouseActive;

        playerLook.enabled = !mouseActive;
        Cursor.lockState = mouseActive ? CursorLockMode.None : CursorLockMode.Locked;

    }

    void SelectInventorySlot()
    {
        if (Keyboard.current.numpad0Key.wasPressedThisFrame)
        {
            playerInventory.SelectSlot(0);
        }
        else if (Keyboard.current.numpad1Key.wasPressedThisFrame)
        {
            playerInventory.SelectSlot(1);
        }
        else if (Keyboard.current.numpad2Key.wasPressedThisFrame)
        {
            playerInventory.SelectSlot(2);
        }
        else if (Keyboard.current.numpad3Key.wasPressedThisFrame)
        {
            playerInventory.SelectSlot(3);
        }
        else if (Keyboard.current.numpad4Key.wasPressedThisFrame)
        {
            playerInventory.SelectSlot(4);
        }
        else if (Keyboard.current.numpad5Key.wasPressedThisFrame)
        {
            playerInventory.SelectSlot(5);
        }
        else if (Keyboard.current.numpad0Key.wasPressedThisFrame)
        {
            playerInventory.SelectSlot(6);
        }
    }
    void CheckContainerInteraction()
    {
        // if (showDebugRay)
        // {
        //     Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.green);
        // }

        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            TryOpenContainer();
        }
    }

    void TryOpenContainer()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, interactiveLayer))
        {
            Debug.Log("Hit Interactive Object: " + hit.collider.gameObject.name);
            SwitchUIFocus();
            OnContainerPlayerInteract(hit.collider.gameObject);
        }
    }

    public void CloseContainer()
    {
        OnContainerPlayerClose();
    }
}