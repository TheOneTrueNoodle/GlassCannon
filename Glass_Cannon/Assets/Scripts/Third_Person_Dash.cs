using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Third_Person_Dash : MonoBehaviour
{
    public Player_Controls pInput;

    Third_Person_Movement movescript;
    Third_Person_Attack attackscript;

    [HideInInspector] public bool isDashing;
    public float dashspeed;
    public float dashtime;
    [HideInInspector] public bool airDash;
    public float dashcooldown = 60f;
    public float dashdefaultcooldown;

    private void Awake()
    {
        dashdefaultcooldown = dashcooldown;
        pInput = new Player_Controls();
        pInput.Player.Dash.performed += _ => StartCoroutine(Dash());
        pInput.Player.Sprint.performed += _ => Sprint();
        pInput.Player.ToggleSprint.performed += _ => Sprint();
    }

    private void Start()
    {
        movescript = GetComponent<Third_Person_Movement>();
        attackscript = GetComponent<Third_Person_Attack>();
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
                isDashing = true;
                attackscript.AttackCancel();
                float starttime = Time.time;

                while (Time.time < starttime + dashtime)
                {
                    movescript.velocity.y = 0;
                    movescript.controller.Move(movescript.moveDir.normalized * dashspeed * Time.deltaTime);

                    yield return null;
                }
                airDash = false;
                dashcooldown = dashdefaultcooldown;
                isDashing = false;
            }
            else { yield return null; }
        }
    }

    private void Sprint()
    {
        movescript.currentspeed = movescript.runspeed;
    }
}
