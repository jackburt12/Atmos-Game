using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 10;  // Amount of item spaces

    // Our current list of items in the inventory
    public List<Item> items = new List<Item>();


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    public void Add(Item item)
    {
        if(item.showInInventory)
        {
            if(items.Count >= space)
            {
                Debug.Log("Not enough room");
                return;
            }

            items.Add(item);

            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }
    }

    // Update is called once per frame
    public void Remove(Item item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
