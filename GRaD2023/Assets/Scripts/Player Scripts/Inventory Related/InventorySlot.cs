using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

/* Sits on all InventorySlots. */

public class InventorySlot : MonoBehaviour
{

    public Inventory inventory;
    public Player player;

    public Image icon;
    //public Button removeButton;
    //public Button useButton;

    Item item;  // Current item in the slot

    // Add item to the slot
    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        //useButton.interactable = true;
        //removeButton.interactable = true;
    }

    // Clear the slot
    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        //useButton.interactable = false;
        //removeButton.interactable = false;
    }

    // If the remove button is pressed, this function will be called.
    public void RemoveItemFromInventory()
    {
        Inventory.instance.Remove(item);
    }

    // Use the item
    public void UseItem()
    {
        if (item != null)
        {
            if (item.type == Item.Type.Consumable)
            {
                item.Use();
                if (inventory.CheckForConsumables())
                {
                    inventory.Remove(item);
                    player.hungerAndThirst += 5;
                    player.userInterface.UpdatePlayerUI();
                    player.inventoryUI.UpdateUI();
                }
            }
        }
    }

}