using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMov : MonoBehaviour
{
    private Vector3 forwardDirection = Vector3.zero;
    public CharacterController controller;
    public Transform cam;
    public Animator animator;
    public Rigidbody rb;
    public Collider Casco;

    public float turnAngle = 800f;
    public float forwardforce = 2000f;
    public float sidewaysforce = 600f;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    float turnSmoothVelocity;
    Vector3 velocity;
    public bool isGrounded;
    public bool isMoving;
    public bool isRolling;
    public bool hasRun = true;

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;


        if (direction.magnitude >= 0.1)
        {
            
            animator.SetBool("isMoving", true);
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
   
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);

            if (moveDir != Vector3.zero)
            {
                transform.forward = moveDir;
            }
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetTrigger("Jump");
        }
        if (Input.GetButtonDown("Jump") && isGrounded == false)
        {
            isRolling = true;
        }
        if (isRolling == true)
        {
            EntrarCasco();
        }
        if (Input.GetKey("c") && isRolling == true)
        {
            animator.SetBool("isRolling", false);
            isRolling = false;
            controller.enabled = true;
            Casco.enabled = false;
            forwardDirection = Vector3.zero;
        }

    }
    void EntrarCasco()
    {

        animator.SetBool("isRolling", true);
        if (forwardDirection == Vector3.zero)
        {
            forwardDirection = transform.forward;
        }
        controller.enabled = false;
        Casco.enabled = true;
        rb.AddForce(forwardDirection * forwardforce * Time.deltaTime);
        rb.AddForce(0, gravity * 15, 0);
        if (Input.GetKey("a"))
    {
            forwardDirection = Quaternion.Euler(0, -turnAngle, 0) * forwardDirection;
        }
        if (Input.GetKey("d"))
        {
            forwardDirection = Quaternion.Euler(0, turnAngle, 0) * forwardDirection; // Rotaciona o forwardDirection para a direita
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "parede":
                animator.SetBool("isRolling", false);
                isRolling = false;
                controller.enabled = true;
                Casco.enabled = false;
                break;
        }
    }
}

