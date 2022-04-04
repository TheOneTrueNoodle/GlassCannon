using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int Health;
    public int Damage;
    public float[] position;

    public PlayerData (Player Player)
    {
        Health = Player.Health;
        Damage = Player.Damage;

        position = new float[3];
        position[0] = Player.transform.position.x;
        position[1] = Player.transform.position.y;
        position[2] = Player.transform.position.z;
    }
}
