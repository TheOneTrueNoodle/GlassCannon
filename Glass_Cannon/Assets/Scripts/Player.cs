using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int MaxHealth = 20;
    public int CurrentHP;
    public int MinDamage = 5;
    public int MaxDamage = 7;
    public Vector3 Position;
    public HealthBar healthBar;

    public Hitbox_Detection[] HitBoxes;

    public GameObject DeathUI;

    public bool Died;

    private void Start()
    {
        CurrentHP = MaxHealth; 
        foreach (var Hitbox in HitBoxes)
        {
            Hitbox.MaxDamage = MaxDamage;
            Hitbox.MinDamage = MinDamage;
        }
    }

    private void Update()
    {
        Position = gameObject.transform.position;

        healthBar.SetHealth((int)CurrentHP, (int)MaxHealth);

        if(CurrentHP <= 0 && Died != true)
        {
            Die();
            Died = true;
        }
    }

    public void TakeDamage(int Damage)
    {
        CurrentHP -= Damage;
    }

    public void Fall(int Damage)
    {
        Vector3 RespawnPoint = new Vector3(4.75f, -2,8.75f);
        transform.position = RespawnPoint;
        Position = RespawnPoint;
        CurrentHP -= Damage;
    }

    public void Die()
    {
        Time.timeScale = 0;
        DeathUI.SetActive(true);
    }
}
