using System;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    Inventory playerInventory;

    public Color selectedSlotColor;
    public Color notSelectedSlotColor;
    public GameObject inventorySlotPrefab;
    public Transform playerInventoryPanel;
    public Transform containerInventoryPanel;
    public GameObject containerUI;

    public InventorySlotUI[] playerInventorySlotsUI;

    public List<InventorySlotUI> containerInventorySlotsUI = new List<InventorySlotUI>();
    PlayerInteract playerInteract;
    GameObject player;

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInventory = player.GetComponent<Inventory>();
        playerInteract = player.GetComponent<PlayerInteract>();
        // containerInventoryPanel = GameObject.FindWithTag("ContainerInventoryUI").transform;

        InitializePlayerInventory();
        playerInteract.OnContainerPlayerInteract += InitializeContainerUI;
        playerInteract.OnContainerPlayerClose += CloseContainerUI;
    }

    void InitializeContainerUI(GameObject targetContainer)
    {
        // Inventory targetInventory = targetContainer.GetComponent<Inventory>();
        containerUI.SetActive(true);
        RefreshContainerInventory(targetContainer);
    }

    void InitializePlayerInventory()
    {

        playerInventorySlotsUI = new InventorySlotUI[playerInventory.size];

        for (int i = 0; i < playerInventory.size; i++)
        {
            GameObject newSlotGO = Instantiate(inventorySlotPrefab, playerInventoryPanel);
            InventorySlotUI newSlotUI = newSlotGO.GetComponent<InventorySlotUI>();
            newSlotUI.inventorySlot = playerInventory.slots[i];
            playerInventorySlotsUI[i] = newSlotUI;
            newSlotUI.slotImage.color = notSelectedSlotColor;
        }

        playerInventory.OnSelectedSlotChanged += RefreshPlayerSlotSelectionUI;
        playerInventory.OnInventoryChange += RefreshPlayerItems;
        RefreshPlayerItems(playerInventory);
    }

    void CloseContainerUI()
    {
        player.GetComponent<Player>().SwitchUIFocus();
        ContainerUIReset();
        containerUI.SetActive(false);
    }

    void ContainerUIReset()
    {
        containerInventorySlotsUI.Clear();
        foreach (Transform containerSlot in containerInventoryPanel)
        {
            Destroy(containerSlot.gameObject);
        }
    }

    public void RefreshContainerInventory(GameObject container)
    {
        ContainerUIReset();

        Inventory containerInventory = container.GetComponent<Inventory>();

        for (int i = 0; i < containerInventory.size; i++)
        {
            GameObject newSlotGO = Instantiate(inventorySlotPrefab, containerInventoryPanel);
            InventorySlotUI newSlotUI = newSlotGO.GetComponent<InventorySlotUI>();
            newSlotUI.inventorySlot = containerInventory.slots[i];
            containerInventorySlotsUI.Add(newSlotUI);
        }
        // containerInventory.OnInventoryChange -= RefreshContainerItems;
        containerInventory.OnInventoryChange += RefreshContainerItems;
        RefreshContainerItems(containerInventory);
    }

    public void RefreshPlayerSlotSelectionUI(int oldIndex, int newIndex)
    {
        if (newIndex < playerInventory.size && newIndex >= 0)
        {
            playerInventorySlotsUI[newIndex].slotImage.color = selectedSlotColor;
        }
        if (oldIndex < playerInventory.size && oldIndex >= 0)
        {
            playerInventorySlotsUI[oldIndex].slotImage.color = notSelectedSlotColor;
        }

    }

    public void RefreshPlayerItems(Inventory targetInventory)
    {
        //если UI активен?
        for (int i = 0; i < targetInventory.size; i++)
        {
            playerInventorySlotsUI[i].Refresh();
        }

    }

    public void RefreshContainerItems(Inventory targetInventory)
    {
        //если UI активен?
        for (int i = 0; i < targetInventory.size; i++)
        {
            containerInventorySlotsUI[i].Refresh();
        }

    }

}
