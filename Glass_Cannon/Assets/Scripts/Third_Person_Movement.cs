using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Third_Person_Movement : MonoBehaviour
{
    //Variables for Controls
    private Player_Controls pInput;
    public CharacterController controller;
    public Transform cam;

    //Variables for movement values
    public float walkspeed = 6f;
    public float runspeed = 12f;
    [HideInInspector] public float currentspeed;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    //Variables for ground registration
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    //Variables for applying movement
    private Vector2 MoveInput;
    public Vector3 moveDir;
    [HideInInspector] public Vector3 velocity;
    [HideInInspector] public bool isGrounded;

    //Variables for turning character body
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    //Variables for Animation
    public Animator anim;
    public float allowPlayerRotation = 0.1f;
    [Header("Animation Smoothing")]
    [Range(0, 1f)]
    public float HorizontalAnimSmoothTime = 0.2f;
    [Range(0, 1f)]
    public float VerticalAnimTime = 0.2f;
    [Range(0, 1f)]
    public float StartAnimTime = 0.3f;
    [Range(0, 1f)]
    public float StopAnimTime = 0.15f;

    public bool blockRotationPlayer;
    public float desiredRotationSpeed = 0.1f;
    [HideInInspector]
    public float moveSpeed;

    private void Awake()
    {
        currentspeed = walkspeed;
        pInput = new Player_Controls();
        pInput.Player.Jump.performed += _ => Jump();
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
        Movement(); 
        if (MoveInput.x == 0 && MoveInput.y == 0)
        {
            currentspeed = walkspeed;
        }

        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Falling To Landing 0"))
        {
            OnDisable();
        }
        else
        {
            OnEnable();
        }
    }

    private void Movement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        anim.SetBool("isGrounded", isGrounded);

        if(isGrounded == true && velocity.y < 0)
        {
            anim.ResetTrigger("TriggerJump");
        }

        MoveInput = pInput.Player.Move.ReadValue<Vector2>();
        Vector3 Direction = new Vector3(MoveInput.x, 0f, MoveInput.y).normalized;

        if (Direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(Direction.x, Direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            //transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * (currentspeed * Time.deltaTime));
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if(currentspeed != runspeed)
        {
            moveSpeed = Direction.magnitude * 0.5f;
        }
        else
        {
            moveSpeed = Direction.magnitude;
        }

        if (currentspeed > allowPlayerRotation)
        {
            anim.SetFloat("Blend", moveSpeed, StartAnimTime, Time.deltaTime);
            PlayerMoveAndRotation();
        }
        else if (currentspeed < allowPlayerRotation)
        {
            anim.SetFloat("Blend", moveSpeed, StopAnimTime, Time.deltaTime);
        }
    }

    public void PlayerMoveAndRotation()
    {
        if (blockRotationPlayer == false)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir), desiredRotationSpeed);
        }
    }

    private void Jump()
    {
        if (isGrounded == true)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            anim.SetTrigger("TriggerJump");
        }
    }

    private void speedManager()
    {
        if(moveDir.magnitude == 0)
        {
            currentspeed = walkspeed;
        }
    }
}
