using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJump : MonoBehaviour
{
    private CharacterController characterController;
    public float jumpForce = 5.0f;
    public float gravity = 9.81f;
    public float speed = 5.0f; // Adicione a variável de velocidade

    private float verticalVelocity = 0.0f;
    private bool isGrounded;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = characterController.isGrounded;

        if (isGrounded)
        {
            verticalVelocity = -0.1f; // Garante que o personagem está "grudado" no chão
        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            verticalVelocity = jumpForce;
        }

        // Aplicar a gravidade
        verticalVelocity -= gravity * Time.deltaTime;

        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;
        moveDirection.y = verticalVelocity;

        // Move o personagem usando o Character Controller
        characterController.Move(moveDirection * Time.deltaTime);
    }
}