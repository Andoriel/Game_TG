using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // O transform do personagem que a c�mera seguir�
    public float smoothSpeed = 5f; // A velocidade de suaviza��o do movimento da c�mera

    private Vector3 offset; // A dist�ncia inicial entre a c�mera e o personagem

    void Start()
    {
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset; // Calcula a posi��o desejada da c�mera

        // Usando Lerp para suavizar o movimento da c�mera
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}
