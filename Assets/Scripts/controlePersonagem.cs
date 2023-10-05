using UnityEngine;

public class controlePersonagem : MonoBehaviour
{
    public float velocidadeInicial = 5.0f; // Velocidade de movimento inicial do personagem.
    public float forcaPulo = 10.0f; // Força do pulo.
    public float aumentoVelocidadePorSegundo = 1.0f; // Aumento de velocidade por segundo ao correr.
    public float maximaVelocidade = 10.0f; // Velocidade máxima ao correr.

    private Rigidbody rb;
    private bool estaCorrendo = false;
    private float velocidadeAtual;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        velocidadeAtual = velocidadeInicial;
    }

    private void Update()
    {
        float movimentoHorizontal = Input.GetAxis("Horizontal");
        float movimentoVertical = Input.GetAxis("Vertical");

        Vector3 direcao = new Vector3(movimentoHorizontal, 0.0f, movimentoVertical).normalized;

        // Se estiver segurando Shift, aumentar gradualmente a velocidade.
        if (Input.GetKey(KeyCode.LeftShift))
        {
            estaCorrendo = true;
            velocidadeAtual += aumentoVelocidadePorSegundo * Time.deltaTime;
            velocidadeAtual = Mathf.Clamp(velocidadeAtual, velocidadeInicial, maximaVelocidade);
        }
        else
        {
            estaCorrendo = false;
            velocidadeAtual = velocidadeInicial;
        }

        // Aplica força para o movimento.
        Vector3 velocidadeMovimento = direcao * velocidadeAtual;
        rb.velocity = new Vector3(velocidadeMovimento.x, rb.velocity.y, velocidadeMovimento.z);

        // Rotaciona o personagem na direção do movimento.
        if (direcao != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direcao);
        }

        // Verifica o pulo.
        if (Input.GetKeyDown(KeyCode.Space))
        {

           
                rb.AddForce(Vector3.up * forcaPulo, ForceMode.Impulse);
        }
    }

    private bool EstaNoChao()
    {
        RaycastHit hit;
        float raio = 0.1f;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, raio))
        {
            return true;
        }

        return false;
    }
}