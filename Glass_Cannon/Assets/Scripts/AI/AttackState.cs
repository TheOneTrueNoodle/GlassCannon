using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : State
{
    public ChaseState chaseState;

    public EnemyStats Stats;
    public GameObject Hitbox;
    public GameObject DangerArea;

    public float attackWindup = 0.25f;
    public float attackTime = 0.15f;
    public float attackLag = 1f;
    private bool IsAttacking = false;
    private bool FinishedAttacking;
    public Transform ParentTransform;

    private GameObject Player;

    public NavMeshAgent agent;

    public override State RunCurrentState()
    {
        RunState();

        if(FinishedAttacking)
        {
            if (Vector3.Distance(transform.position, Player.transform.position) <= chaseState.AttackRange)
            {
                IsAttacking = false;
                FinishedAttacking = false;
                return this;
            }
            else
            {
                IsAttacking = false;
                FinishedAttacking = false;
                return chaseState;
            }
        }
        else
        {
            return this;
        }
    }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        DangerArea.SetActive(false);
        Hitbox.SetActive(false);
    }

    public void RunState()
    {
        agent.SetDestination(transform.position);

        if(IsAttacking != true)
        {
            StartCoroutine(TriggerAttack());
        }
    }

    IEnumerator TriggerAttack()
    {
        IsAttacking = true;
        Vector3 pointToLook = Player.transform.position;
        ParentTransform.LookAt(pointToLook);

        DangerArea.SetActive(true);
        float starttime = Time.time;
        while (Time.time < starttime + attackWindup)
        {
            yield return null;
        }
        DangerArea.SetActive(false);

        starttime = Time.time;
        while (Time.time < starttime + 0.1f)
        {
            yield return null;
        }

        Hitbox.SetActive(true);
        Hitbox.GetComponent<EnemyHitbox>().IsAttacking = true;
        starttime = Time.time;
        while (Time.time < starttime + attackTime)
        {
            yield return null;
        }
        Hitbox.GetComponent<EnemyHitbox>().IsAttacking = false;
        Hitbox.SetActive(false);
        Hitbox.GetComponent<EnemyHitbox>().DealtDamage = false;

        starttime = Time.time;
        while (Time.time < starttime + attackLag)
        {
            yield return null;
        }
        FinishedAttacking = true;
    }
}
