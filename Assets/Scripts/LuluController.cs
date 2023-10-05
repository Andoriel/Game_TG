using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuluController: MonoBehaviour
{
    public float moveSpeed = 5.0f; // Velocidade de movimento do personagem.



    void Update()
    {
        // Obt�m as entradas de movimento do jogador.
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");



        // Calcula o vetor de movimento com base nas entradas.
        Vector3 moveDirection = new Vector3(horizontalInput, 0.0f, verticalInput);



        // Normaliza o vetor de movimento para evitar movimento mais r�pido na diagonal.
        moveDirection.Normalize();



        // Move o personagem.
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
}
