using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Third_Person_Attack : MonoBehaviour
{
    public Player_Controls pInput;

    Third_Person_Movement movescript;
    Third_Person_Dash dashscript;

    [HideInInspector] public float AttackString = 0;
    public float MaxAttackString = 3f;
    public float attackTime = 0.15f;
    public GameObject HitBox;
    public float damage;
    public float attackLag = 0.15f;
    [HideInInspector] public bool IsAttacking = false;

    public void Awake()
    {
        pInput = new Player_Controls();
        pInput.Player.PrimaryAttack.performed += _ => TriggerAttack();
    }

    public void Start()
    {
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

    public void resetHitbox()
    {
        HitBox.transform.localPosition = new Vector3(0, 0, (float)1.4);
        HitBox.transform.localScale = new Vector3((float)1.5, (float)1.5, (float)1.5);
        HitBox.SetActive(false);
    }

    private void TriggerAttack()
    {
        if(IsAttacking != true)
        {
            IsAttacking = true;
            if (dashscript.isDashing == true)
            {
                StartCoroutine(DashAttack());
            }
            else
            {
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator DashAttack()
    {
        movescript.enabled = false;
        movescript.velocity.x = 0;
        movescript.velocity.y = 0;
        movescript.velocity.z = 0;

        HitBox.SetActive(true);
        HitBox.transform.localPosition = new Vector3(0, 0, 0);
        HitBox.transform.localScale = new Vector3((float)4, (float)1.5, (float)4);

        float starttime = Time.time;

        while (Time.time < starttime + dashscript.dashtime)
        {
            movescript.velocity.y = 0;
            movescript.controller.Move(movescript.moveDir * dashscript.dashspeed * Time.deltaTime);

            yield return null;
        }
        dashscript.airDash = false;
        dashscript.dashcooldown = dashscript.dashdefaultcooldown;
        dashscript.isDashing = false;
        IsAttacking = false;
        resetHitbox();

        starttime = Time.time;
        while (Time.time < starttime + attackLag)
        {
            yield return null;
        }
        movescript.enabled = true;
    }

    IEnumerator Attack()
    {
        movescript.enabled = false;
        movescript.velocity.x = 0;
        movescript.velocity.y = 0;
        movescript.velocity.z = 0;

        HitBox.SetActive(true);
        HitBox.transform.localPosition = new Vector3(0, 0, (float)1.4);
        HitBox.transform.localScale = new Vector3((float)1.5, (float)1.5, (float)1.5);

        float starttime = Time.time;
        while (Time.time < starttime + attackTime)
        {
            yield return null;
        }
        IsAttacking = false;
        resetHitbox();

        starttime = Time.time;
        while (Time.time < starttime + attackLag)
        {
            yield return null;
        }
        movescript.enabled = true;
    }
}
