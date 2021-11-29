using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Third_Person_Dash : MonoBehaviour
{
    public Player_Controls pInput;

    Third_Person_Movement movescript;

    public float dashspeed;
    public float dashtime;

    private void Awake()
    {
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

    IEnumerator Dash()
    {
        float starttime = Time.time;

        while(Time.time < starttime + dashtime)
        {
            movescript.controller.Move(movescript.moveDir * dashspeed * Time.deltaTime);

            yield return null;
        }
    }
}
