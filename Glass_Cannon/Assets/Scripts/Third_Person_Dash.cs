using System.Collections;
using UnityEngine;

public class Third_Person_Dash : MonoBehaviour
{
    public Player_Controls pInput;

    Third_Person_Movement movescript;

    public float dashspeed;
    public float dashtime;
    private bool airDash;
    public float dashCount = 1f;
    private float currentDashCount;
    public float dashcooldown = 60f;
    private float dashdefaultcooldown;

    private void Awake()
    {
        dashdefaultcooldown = dashcooldown;
        pInput = new Player_Controls();
        pInput.Player.Dash.performed += _ => StartCoroutine(Dash());
        pInput.Player.Sprint.performed += _ => Sprint();
    }

    private void Start()
    {
        movescript = GetComponent<Third_Person_Movement>();
        currentDashCount = dashCount;
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
        /*if(movescript.isGrounded == true && airDash != true)
        {
            airDash = true;
        }*/
    }

    private void FixedUpdate()
    {
        if(currentDashCount <= 0)
        {
            dashcooldown--;
        }
        if (dashcooldown <= 0 && currentDashCount <= 0)
        {
            dashcooldown = dashdefaultcooldown;
            currentDashCount = dashCount;
        }
    }

    IEnumerator Dash()
    {
        if (currentDashCount > 0)
        {
            //if (movescript.isGrounded == true || airDash == true)
            //{
                float starttime = Time.time;

                while (Time.time < starttime + dashtime)
                {
                    movescript.velocity.y = 0;
                    movescript.controller.Move(movescript.moveDir * dashspeed * Time.deltaTime);

                    
                }
            currentDashCount -= 1;
            if (currentDashCount == 0)
            {
                dashcooldown = dashdefaultcooldown;
            }
            yield return null;
            //}
            // else { yield return null; }
        }
    }

    private void Sprint()
    {
        movescript.currentspeed = movescript.runspeed;
    }
}
