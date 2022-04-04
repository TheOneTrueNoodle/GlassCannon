using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Health = 2;
    public int Damage = 5;
    public Vector3 Position;

    private void Update()
    {
        Position = gameObject.transform.position;
    }
}
