using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

/* The base item class. All items should derive from this. */
public class Item
{
    [XmlElement("id")]
    public int id { get; set; }
    [XmlAttribute("name")]
    public string name { get; set; }
    [XmlElement("value")]
    public float value { get; set; }
    [XmlElement("type")]
    public string type { get; set; }

    public int amount { get; set; }

    public Item() { }

    public Item(Item item)
    {
        this.id = item.id;
        this.name = item.name;
        this.value = item.value;
        this.type = item.type;
        this.amount = item.amount;

    }

    // Called when the item is pressed in the inventory
    public virtual void Use()
    {
        // Use the item
        // Something may happen
    }

    // Call this method to remove the item from inventory
    //public void RemoveFromInventory()
    //{
    //    Inventory.Instance.Remove(this);
    //}

    private void ParseItem()
    {

    }
}