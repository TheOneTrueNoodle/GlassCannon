using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Third_Person_Attack : MonoBehaviour
{
    public Player_Controls pInput;

    Third_Person_Movement movescript;
    Third_Person_Dash dashscript;

    [HideInInspector] public int AttackString = 0;
    private Queue<IEnumerator> AttackQueue = new Queue<IEnumerator>();
    public int MaxAttackString = 3;
    public float attackTime = 0.15f;
    public GameObject[] HitBoxes;
    public GameObject MidAirHitbox;
    public float damage;
    public float attackLag = 0.15f;

    private bool AirAttack;

    public void Awake()
    {
        pInput = new Player_Controls();
        pInput.Player.PrimaryAttack.performed += _ => TriggerAttack();
    }

    public void Start()
    {
        StartCoroutine(CoroutineCoordinator());
        movescript = GetComponent<Third_Person_Movement>();
        dashscript = GetComponent<Third_Person_Dash>();
}
    private void OnEnable()
    {
        pInput.Player.Enable();
    }

    private void OnDisable()
    {
        pInput.Player.Disable();
    }

    private void FixedUpdate()
    {
        if(movescript.isGrounded == true)
        {
            AirAttack = true;
        }
    }

    private void TriggerAttack()
    {
        if (movescript.isGrounded != true && AirAttack == true)
        {
            AirAttack = false;
            AttackQueue.Enqueue(MidAirAttack());
        }
        else if(movescript.isGrounded == true)
        {
            if (AttackString < MaxAttackString)
            {
                if (dashscript.isDashing == true && AttackString == 0)
                {
                    AttackQueue.Enqueue(DashAttack(AttackString));
                }
                else
                {
                    AttackString += 1;
                    AttackQueue.Enqueue(Attack(AttackString));
                }
            }
        }
    }

    public void AttackCancel()
    {
        if(dashscript.isDashing == true)
        {
            StopAllCoroutines();
            movescript.enabled = true;
            AttackQueue.Clear();
            for(int i = 0; i <= MaxAttackString; i++)
            {
                HitBoxes[i].SetActive(false);
            }
            StartCoroutine(CoroutineCoordinator());
            AttackCooldown();
        }
    }

    public void AttackCooldown()
    {
        if (AttackQueue.Count == 0)
        {
            movescript.currentspeed = movescript.walkspeed;
            AttackString = MaxAttackString;
            AttackQueue.Enqueue(AtkCooldown());
            AttackString = 0;
        }
    }

    IEnumerator CoroutineCoordinator()
    {
        while (true)
        {
            while (AttackQueue.Count > 0)
                yield return StartCoroutine(AttackQueue.Dequeue());
            yield return null;
        }
    }


    IEnumerator DashAttack(int ThisAttackString)
    {
        movescript.enabled = false;
        movescript.velocity.x = 0;
        movescript.velocity.y = 0;
        movescript.velocity.z = 0;

        HitBoxes[ThisAttackString].SetActive(true);

        float starttime = Time.time;

        while (Time.time < starttime + attackTime)
        {
            yield return null;
        }
        HitBoxes[ThisAttackString].SetActive(false);

        starttime = Time.time;
        while (Time.time < starttime + attackLag)
        {
            yield return null;
        }
        movescript.enabled = true;
        AttackCooldown();
    }

    IEnumerator Attack(int ThisAttackString)
    {
        movescript.enabled = false;
        movescript.velocity.x = 0;
        movescript.velocity.y = 0;
        movescript.velocity.z = 0;

        HitBoxes[ThisAttackString].SetActive(true);

        float starttime = Time.time;
        while (Time.time < starttime + attackTime)
        {
            yield return null;
        }
        HitBoxes[ThisAttackString].SetActive(false);

        starttime = Time.time;
        while (Time.time < starttime + attackLag)
        {
            yield return null;
        }
        movescript.enabled = true;
        AttackCooldown();
    }

    IEnumerator MidAirAttack()
    {
        MidAirHitbox.SetActive(true);

        float starttime = Time.time;
        while (Time.time < starttime + attackTime)
        {
            yield return null;
        }
        MidAirHitbox.SetActive(false);

        starttime = Time.time;
        while (Time.time < starttime + attackLag)
        {
            yield return null;
        }
        AttackCooldown();
    }

    IEnumerator AtkCooldown()
    {
        float starttime = Time.time;
        while (Time.time < starttime + attackLag * 2)
        {
            yield return null;
        }
    }
}
