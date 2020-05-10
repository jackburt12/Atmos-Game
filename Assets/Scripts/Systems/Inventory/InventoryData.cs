using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData
{

    public int[] itemId;
    public int[] itemAmount;

    public InventoryData(Inventory inventory)
    {
        itemId = new int[inventory.items.Count];
        itemAmount = new int[inventory.items.Count];

        for (int i = 0; i < inventory.items.Count; i++) {
            itemId[i] = inventory.items[i].id;
            itemAmount[i] = inventory.items[i].amount;
        }
    }

}
