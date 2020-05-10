using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int health;
    public float money;

    public PlayerData (Player player)
    {
        health = player.health;
        money = player.money;
    }
}
