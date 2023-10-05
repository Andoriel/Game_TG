using UnityEngine;

public class Aceleracao : MonoBehaviour
{
    public float velocidadeInicial = 5.0f;  // Velocidade inicial do objeto
    public float aceleracao = 2.0f;         // Taxa de acelera��o
    public float velocidadeMaxima = 10.0f;  // Velocidade m�xima do objeto

    private float velocidadeAtual;          // Velocidade atual do objeto

    void Start()
    {
        velocidadeAtual = velocidadeInicial;
    }

    void Update()
    {
        // Verificar se a tecla W (ou outra tecla de sua escolha) est� sendo pressionada
        if (Input.GetKey(KeyCode.W))
        {
            // Acelerar o objeto
            velocidadeAtual += aceleracao * Time.deltaTime;

            // Limitar a velocidade m�xima
            velocidadeAtual = Mathf.Min(velocidadeAtual, velocidadeMaxima);
        }
        else
        {
            // Quando a tecla n�o est� pressionada, o objeto p�ra de acelerar
            velocidadeAtual = velocidadeInicial;
        }

        // Mover o objeto na dire��o da frente com a velocidade atual
        transform.Translate(Vector3.forward * velocidadeAtual * Time.deltaTime);
    }
}

