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
    public float speed = 6f;
    public float gravity = -9.81f;

    //Variables for ground registration
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    //Variables for applying movement
    private Vector3 velocity;
    private Vector2 MoveInput;
    public Vector3 moveDir;
    private bool isGrounded;

    //Variables for turning character body
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    private void Awake()
    {
        pInput = new Player_Controls();
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
    }

    private void Movement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        MoveInput = pInput.Player.Move.ReadValue<Vector2>();
        Vector3 Direction = new Vector3(MoveInput.x, 0f, MoveInput.y).normalized;

        if (Direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(Direction.x, Direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * (speed * Time.deltaTime));
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
