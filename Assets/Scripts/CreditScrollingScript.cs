using UnityEngine;

public class CreditScrollingScript : MonoBehaviour
{
    public float scrollSpeed = 1.0f;

    void Start()
    {
        // Define a posição inicial da caixa de texto no eixo Y para -600.
        transform.position = new Vector3(transform.position.x, -600f, transform.position.z);
    }

    void Update()
    {
        // Move o painel de créditos para cima ao longo do eixo Y
        transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime*15);
    }

    // Restante do código...
}
