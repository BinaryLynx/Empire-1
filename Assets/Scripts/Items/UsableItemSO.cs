using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "UsableItemSO", menuName = "Scriptable Objects/UsableItemSO")]
public class UsableItemSO : ScriptableObject
{
    public string id;
    public List<ItemActionSO> itemActions;
    public ItemActionSO previewAction;

    // public void OnEnable(){
    //     PlayerInventory playerInventory = GameObject.FindWithTag("Player").GetComponent<PlayerInventory>();
    //     playerInventory.OnSelectedSlotChanged += DisplayPreview;
    // }

    public void Preview(GameObject owner)
    {
        if (previewAction is null)
        {
            return;
        }

        previewAction.Execute(owner, this);
    }

    public bool Use(string actionName, GameObject owner)
    {
        ItemActionSO itemAction = itemActions.FirstOrDefault(action => action.actionName == actionName);

        if (itemAction is null)
        {
            return false;
        }

        if (itemAction.CanExecute(owner, this))
        {
            itemAction.Execute(owner, this);
            return true;
        }
        return false;
    }

    public List<ItemActionSO> GetAvailableActions(GameObject owner)
    {
        // return itemActions.Where(action => action.CanExecute(owner, this)).ToList();
        return itemActions;
    }

}
