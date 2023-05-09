using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public AudioManager audioManager;
    public EventHandler eventHandler;
    public UserInterfaceHandler userInterfaceHandler;
    public Player player;

    #region Singleton

    public static Inventory instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 10;  // Amount of item spaces

    // Our current list of items in the inventory
    public List<Item> items = new List<Item>();

    // Add a new item if enough room
    public void Add(Item item)
    {
        if (item.showInInventory)
        {
            if (items.Count >= space)
            {
                StartCoroutine(eventHandler.ShowEvent("Inventory full...", 1));
                Debug.Log("Not enough room.");
                return;
            }

            items.Add(item);

            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }
    }

    public void PurchaseItem(Item item)
    {
        if (items.Count >= space)
        {
            StartCoroutine(eventHandler.ShowEvent("Inventory full...", 1));
            Debug.Log("Not enough room");
            return;
        }

        if (player.money >= item.price)
        {
            audioManager.BoughtItemSFX();
            player.money -= item.price;
            items.Add(item);
            StartCoroutine(eventHandler.ShowEvent($"Purchased: \n {item.itemName}", 1));
            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }
            userInterfaceHandler.UpdatePlayerUI();
        } else
        {
            StartCoroutine(eventHandler.ShowEvent("Not enough money...", 1));
            return;
        }
    }

    // Remove an item
    public void Remove(Item item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }


    public bool CheckForConsumables()
    {
        for (int i = 0; i < items.Count; i++)
        {
            Item item = items[i];
            if (item != null)
            {
                if (item.type == Item.Type.Consumable)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        return false;
    }

    public bool IsInventoryFull()
    {
        foreach (Item item in items)
        {
            if (item == null)
            {
                return false;
            }
        }
        return true;
    }

}