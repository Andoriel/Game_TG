using UnityEngine;

public class Aceleracao : MonoBehaviour
{
    public float velocidadeInicial = 5.0f;  // Velocidade inicial do objeto
    public float aceleracao = 2.0f;         // Taxa de aceleração
    public float velocidadeMaxima = 10.0f;  // Velocidade máxima do objeto

    private float velocidadeAtual;          // Velocidade atual do objeto

    void Start()
    {
        velocidadeAtual = velocidadeInicial;
    }

    void Update()
    {
        // Verificar se a tecla W (ou outra tecla de sua escolha) está sendo pressionada
        if (Input.GetKey(KeyCode.W))
        {
            // Acelerar o objeto
            velocidadeAtual += aceleracao * Time.deltaTime;

            // Limitar a velocidade máxima
            velocidadeAtual = Mathf.Min(velocidadeAtual, velocidadeMaxima);
        }
        else
        {
            // Quando a tecla não está pressionada, o objeto pára de acelerar
            velocidadeAtual = velocidadeInicial;
        }

        // Mover o objeto na direção da frente com a velocidade atual
        transform.Translate(Vector3.forward * velocidadeAtual * Time.deltaTime);
    }
}

