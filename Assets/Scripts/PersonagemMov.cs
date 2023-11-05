using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonagemMov : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public float speed = 6f;
    public float turnsmoothtime = 0.1f;
    float turnsmoothvelocity;
    Vector3 Velocity;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float grounddistance = 0.4f;
    public LayerMask groundmask;
    public bool isGrounded;
    public float jumpheight = 3f;
    // Update is called once per frame
    private void Start()
    {
        controller.enabled = true;
    }

    private void Update()
    {

    isGrounded = Physics.CheckSphere(groundCheck.position, grounddistance, groundmask);

    if (isGrounded && Velocity.y< 0)
    {
        Velocity.y = -2f;
    }

    float horizontal = Input.GetAxisRaw("Horizontal");
    float vertical = Input.GetAxisRaw("Vertical");
    Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

    if (direction.magnitude >= 0.1f)
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnsmoothvelocity, turnsmoothtime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        // Normaliza a direção antes de criar o Quaternion.
        Vector3 normalizedDirection = Vector3.Normalize(direction);

        // Calcula o Quaternion olhando na direção desejada.
        Quaternion targetRotation = Quaternion.LookRotation(normalizedDirection);

        // Verifica se o jogo está pausado antes de calcular o movimento.
        float deltaTime = Time.timeScale > 0 ? Time.deltaTime : 0;

        // Aplica a rotação suave.
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnsmoothtime * deltaTime);

        // Move o personagem.
        controller.Move(transform.forward * speed * deltaTime);
    }

    if (Input.GetButtonDown("Jump") && isGrounded)
    {
        Velocity.y = Mathf.Sqrt(jumpheight * -2f * gravity);
        isGrounded = false;
    }

    Velocity.y += gravity * Time.deltaTime;
    controller.Move(Velocity * Time.deltaTime);

    }
    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }
}
