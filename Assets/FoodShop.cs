using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodShop : Shop
{
    //// Start is called before the first frame update
    //public override void Interact()
    //{
    //    base.Interact();
    //    gameObject.GetComponent<DialogueTrigger>().TriggerDialogue(offset);
    //}

    public override void Interact()
    {
        base.Interact();

        ItemDatabase db = ItemDatabase.Load("XML/items");

        shopInventory = new List<Item>();
        shopInventory.Add(db.items.Find(obj => obj.id == 101));
        shopInventory.Add(db.items.Find(obj => obj.id == 102));
        shopInventory.Add(db.items.Find(obj => obj.id == 103));
        shopInventory.Add(db.items.Find(obj => obj.id == 104));
        shopInventory.Add(db.items.Find(obj => obj.id == 105));
        shopInventory.Add(db.items.Find(obj => obj.id == 106));

        shopInventory.Add(db.items.Find(obj => obj.id == 151));
        shopInventory.Add(db.items.Find(obj => obj.id == 152));
        shopInventory.Add(db.items.Find(obj => obj.id == 153));
        shopInventory.Add(db.items.Find(obj => obj.id == 154));

        foreach (Item item in shopInventory)
        {
            item.amount = 5;
        }

    }
}
