using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    // Our current list of items in the inventory
    public List<Item> items;

    private ItemDatabase db;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            LoadInventory();
            db = ItemDatabase.Load("XML/items");
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetInventory();
        }
    }

    // Start is called before the first frame update
    public void Add(int itemId)
    {
        int index = items.FindIndex(i => i.id == itemId);

        if (index >= 0)
        {
            //items[index].amount += 1;
        }
        else
        {
            Item item = db.items.Find(obj => obj.id == itemId);
            item.amount = 1;
            items.Add(item);
        }

    }

    // Update is called once per frame
    public void Remove(int itemId)
    {

    }

    public void SaveInventory()
    {
        SaveSystem.SaveInventory(this);
        //LoadInventory();
    }

    public void LoadInventory()
    {
        InventoryData data = SaveSystem.LoadInventory();

        ItemDatabase db = ItemDatabase.Load("XML/items");

        items = new List<Item>();

        for (int i = 0; i < data.itemId.Length; i++)
        {
            Item item = db.items.Find(obj => obj.id == data.itemId[i]);
            item.amount = data.itemAmount[i];

            if(item.amount > 0)
            {
                items.Add(item);
            }
        }
    }

    public void ResetInventory()
    {
        

        items = new List<Item>();
        //items.Add(db.items.Find(obj => obj.id == 1));
        //items[0].amount = 3;
        //items.Add(db.items.Find(obj => obj.id == 3));
        //items[1].amount = 1;
        SaveInventory();
    }
}
