using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Third_Person_Dash : MonoBehaviour
{
    public Player_Controls pInput;

    Third_Person_Movement movescript;

    public float dashspeed;
    public float dashtime;
    private bool airDash;
    public float dashcooldown = 60f;
    private float dashdefaultcooldown;

    private void Awake()
    {
        dashdefaultcooldown = dashcooldown;
        pInput = new Player_Controls();
        pInput.Player.Dash.started += _ => StartCoroutine(Dash());
    }

    private void Start()
    {
        movescript = GetComponent<Third_Person_Movement>();
    }
    private void OnEnable()
    {
        pInput.Player.Enable();
    }

    private void OnDisable()
    {
        pInput.Player.Disable();
    }

    private void Update()
    {
        if(movescript.isGrounded == true && airDash != true)
        {
            airDash = true;
        }
    }

    private void FixedUpdate()
    {
        dashcooldown--;
    }

    IEnumerator Dash()
    {
        if (dashcooldown <= 0)
        {
            if (movescript.isGrounded == true || airDash == true)
            {
                float starttime = Time.time;

                while (Time.time < starttime + dashtime)
                {
                    movescript.velocity.y = 0;
                    movescript.controller.Move(movescript.moveDir * dashspeed * Time.deltaTime);

                    airDash = false;
                    dashcooldown = dashdefaultcooldown;
                    movescript.currentspeed = movescript.runspeed;
                    yield return null;
                }
            }
            else { yield return null; }
        }
    }
}
