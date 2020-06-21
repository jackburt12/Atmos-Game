using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    //public int itemId;
    public Item item;

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.GetComponentInParent<ShopInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuySell()
    {
        if(gameObject.transform.parent.name == "Buy Window")
        {
            GetComponentInParent<ShopInventory>().BuyItem(this);
        } else
        {
            GetComponentInParent<ShopInventory>().SellItem(this);
        }
    }

    
}
