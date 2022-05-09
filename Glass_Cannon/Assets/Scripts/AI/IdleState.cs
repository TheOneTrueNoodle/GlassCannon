using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public ChaseState chaseState;
    public bool canSeeThePlayer;
    public float DetectionRange = 5f;

    private GameObject Player;

    public override State RunCurrentState()
    {
        RunState(); 
        if (canSeeThePlayer)
        {
            return chaseState;
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
        if (Vector3.Distance(transform.position, Player.transform.position) <= DetectionRange)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, Player.transform.position - transform.position, out hit, DetectionRange))
            {
                if (hit.transform.tag == "Player")
                {
                    canSeeThePlayer = true;
                }
            }
        }
    }
}
