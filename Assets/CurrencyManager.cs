using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour
{

    public static CurrencyManager Instance { get; private set; }

    public float PlayerBalance;
    private Text balance;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        balance = GameObject.Find("Monetary Balance").GetComponent<Text>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            UpdateBalance();
        }
    }

    public void BuyItem(double cost)
    {
        PlayerPrefs.SetFloat("playerBalance", (float)(PlayerBalance - cost));
        UpdateBalance();
    }

    public void SellItem(double value)
    {
        PlayerPrefs.SetFloat("playerBalance", (float)(PlayerBalance + value));
        UpdateBalance();
    }

    private void UpdateBalance()
    {
        PlayerBalance = PlayerPrefs.GetFloat("playerBalance");
        balance.text = PlayerBalance.ToString("c2");
    }

}
