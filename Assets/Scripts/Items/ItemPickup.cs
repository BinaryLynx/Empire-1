using UnityEngine;
using UnityEngine.InputSystem;

public class ItemPickup : MonoBehaviour
{

    public InventoryItemSO itemTemplate;
    public int amount = 1;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {
            return;
        }
        Inventory inventory = other.gameObject.GetComponent<Inventory>();
        if (inventory is null)
        {
            return;
        }

        int remainder = inventory.AddItem(itemTemplate, amount);
        if (remainder == 0)
        {
            Destroy(gameObject);

        }
        else
        {
            amount = remainder;
        }

    }

}