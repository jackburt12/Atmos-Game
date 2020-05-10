using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class ShopInventory : MonoBehaviour
{

    public GameObject itemPrefab;
    public GameObject sellWindow, buyWindow;

    public Inventory instance;

    public GameObject totalMoney;
    public GameObject balanceUI;

    private float shopBalance = 0;

    public void PopulateShop(List<Item> items)
    {
        Inventory.Instance.LoadInventory();

        foreach (ShopItem item in sellWindow.GetComponentsInChildren<ShopItem>())
        {
            GameObject.Destroy(item.gameObject);
        }
        foreach (ShopItem item in buyWindow.GetComponentsInChildren<ShopItem>())
        {
            GameObject.Destroy(item.gameObject);
        }

        shopBalance = 0;
        UpdateUI();

        //Populate sell
        ItemDatabase db = ItemDatabase.Load("XML/items");

        foreach (Item item in Inventory.Instance.items)
        {
            InstantiateItem(item, sellWindow.transform);
        }

        
        //Populate buy
        foreach (Item item in items)
        {
            InstantiateItem(item, buyWindow.transform);
        }
        

        totalMoney.GetComponent<Text>().text = Player.Instance.money.ToString("C", CultureInfo.CurrentCulture);
    }

    public void BuyItem (ShopItem shopItem)
    {
        //decrease number in BuyWindow
        shopItem.item.amount -= 1;
        if (shopItem.item.amount == 0)
        {
            GameObject.Destroy(shopItem.gameObject);
        }
        else
        {
            shopItem.gameObject.GetComponentsInChildren<Text>()[2].text = shopItem.item.amount.ToString();
        }

        //increase number in SellWindow
        List<ShopItem> tempList = new List<ShopItem>(sellWindow.GetComponentsInChildren<ShopItem>());

        int index = tempList.FindIndex(i => i.item.id == shopItem.item.id);
        if (index >= 0)
        {
            tempList[index].item.amount += 1;
            Inventory.Instance.items.Find(obj => obj.id == tempList[index].item.id).amount = tempList[index].item.amount;
            tempList[index].gameObject.GetComponentsInChildren<Text>()[2].text = tempList[index].item.amount.ToString();
        }
        else
        {
            Item newItem = new Item(shopItem.item);
            Inventory.Instance.Add(newItem.id);
            newItem.amount = 1;
            GameObject newRow = InstantiateItem(newItem, sellWindow.transform);
        }


        //update balance UI
        shopBalance -= float.Parse(shopItem.GetComponentsInChildren<Text>()[1].text, NumberStyles.Currency);
        UpdateUI();
    }

    public void SellItem(ShopItem shopItem)
    {
        //decrease number in SellWindow
        shopItem.item.amount -= 1;
        if (shopItem.item.amount == 0)
        {
            GameObject.Destroy(shopItem.gameObject);
        }
        else
        {
            shopItem.gameObject.GetComponentsInChildren<Text>()[2].text = shopItem.item.amount.ToString();
        }

        //increase number in BuyWindow
        List<ShopItem> tempList = new List<ShopItem>(buyWindow.GetComponentsInChildren<ShopItem>());

        int index = tempList.FindIndex(i => i.item.id == shopItem.item.id);
        if (index >= 0)
        {
            tempList[index].item.amount += 1;
            tempList[index].gameObject.GetComponentsInChildren<Text>()[2].text = tempList[index].item.amount.ToString();
        }
        else
        {
            Item newItem = new Item(shopItem.item);
            newItem.amount = 1;
            GameObject newRow = InstantiateItem(newItem, buyWindow.transform);
        }

        //update balance UI
        shopBalance += float.Parse(shopItem.GetComponentsInChildren<Text>()[1].text, NumberStyles.Currency);
        UpdateUI();
    }

    private void UpdateUI()
    {

        Text balanceText = balanceUI.GetComponent<Text>();
        balanceText.text = shopBalance.ToString("C", CultureInfo.CurrentCulture);
        if(shopBalance < 0)
        {
            balanceText.color = new Color(1f, 0f, 0f, 1f);
        } else if(shopBalance > 0)
        {
            balanceText.color = new Color(0f, 1f, 0f, 1f);
        } else
        {
            balanceText.color = new Color(1f, 1f, 1f, 0f);
        }
    }

    private void UpdateLists()
    {

    }

    private GameObject InstantiateItem(Item item, Transform parent)
    {
        GameObject itemRow = Instantiate(itemPrefab, parent);
        itemRow.GetComponentsInChildren<Text>()[0].text = item.name;
        itemRow.GetComponentsInChildren<Text>()[1].text = item.value.ToString("C", CultureInfo.CurrentCulture);
        itemRow.GetComponentsInChildren<Text>()[2].text = item.amount.ToString();
        itemRow.GetComponent<ShopItem>().item = item;

        return itemRow;
    }

    public void Confirm()
    {
        try
        {
            Player.Instance.money += shopBalance;
            Player.Instance.SavePlayer();
            Inventory.Instance.SaveInventory();
        } catch (Exception ex)
        {
            throw ex;
        }


        CloseShop();

    }

    public void CloseShop()
    {
        gameObject.SetActive(false);
    }


}
