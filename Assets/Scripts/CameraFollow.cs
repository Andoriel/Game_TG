using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // O transform do personagem que a câmera seguirá
    public float smoothSpeed = 5f; // A velocidade de suavização do movimento da câmera

    private Vector3 offset; // A distância inicial entre a câmera e o personagem

    void Start()
    {
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset; // Calcula a posição desejada da câmera

        // Usando Lerp para suavizar o movimento da câmera
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}
