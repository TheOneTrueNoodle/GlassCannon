using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int MaxHealth;
    public int Health;
    public int MinDamage;
    public int MaxDamage;
    public float[] position;

    public PlayerData (Player Player)
    {
        MaxHealth = Player.MaxHealth;
        Health = Player.CurrentHP;
        MinDamage = Player.MinDamage;
        MaxDamage = Player.MaxDamage;

        position = new float[3];
        position[0] = Player.transform.position.x;
        position[1] = Player.transform.position.y;
        position[2] = Player.transform.position.z;
    }
}
