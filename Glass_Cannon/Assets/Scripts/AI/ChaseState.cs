using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : State
{
    public AttackState attackState;

    public bool isInAttackRange;

    private GameObject Player;

    public NavMeshAgent agent;
    public float AttackRange = 1f;

    public override State RunCurrentState()
    {
        RunState();
        if(isInAttackRange)
        {
            return attackState;
        }
        else 
        {
            return this;
        }
    }
    public void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public void RunState()
    {
        agent.SetDestination(Player.transform.position);

        if(Vector3.Distance(transform.position, Player.transform.position) <= AttackRange)
        {
            isInAttackRange = true;
        }
        else
        {
            isInAttackRange = false;
        }
    }
}
