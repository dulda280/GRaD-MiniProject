using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName = "New Item";
    public string itemDescription = "New Description";
    public Sprite icon;
    public int price = 0;
    public bool showInInventory = true;
    public enum Type { Default, Consumable}
    public Type type = Type.Default;

    // Called when the item is pressed in the inventory
    public virtual void Use()
    {
        // Use the item
        // Something may happen
        Debug.Log("Using: " + itemName);
    }

    // Call this method to remove the item from inventory
    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}
