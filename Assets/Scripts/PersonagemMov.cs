using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonagemMov : MonoBehaviour
{
    public CharacterController controller;

    public Transform cam;
    public Transform groundCheck;

    public Object npcCoelho;

    public LayerMask groundmask;

    public float speed;
    public float turnsmoothtime = 0.1f;
    public float gravity = -9.81f;
    public float grounddistance = 0.4f;
    public float jumpheight = 3f;

    private Animator anim;

    private float turnsmoothvelocity;

    private bool isGrounded;
    private bool isTriggerNPC;
    private bool isActiveBoxNPC;

    Vector3 Velocity;

    [SerializeField] private GameObject caixaDeConversaNPC;

    // Update is called once per frame
    private void Start()
    {
        controller.enabled = true;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            Andar();

        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
            Parado();

        if (Input.GetKeyDown(KeyCode.LeftShift))
            Correr();

        if (Input.GetKeyUp(KeyCode.LeftShift))
            anim.SetBool("correndo", false);

        if (Input.GetButtonDown("Jump") && isGrounded)
            Pular();

        if (Input.GetButtonDown("Fire1") && isTriggerNPC)
            if(!isActiveBoxNPC)
                AtivarConversa();
                else if (isActiveBoxNPC)
                    DesativarConversa();


        isGrounded = Physics.CheckSphere(groundCheck.position, grounddistance, groundmask);

        if (isGrounded && Velocity.y < 0)
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

        Velocity.y += gravity * Time.deltaTime;
        controller.Move(Velocity * Time.deltaTime);

    }
    private void Parado()
    {
        anim.SetBool("parado", true);
        anim.SetBool("andando", false);
        anim.SetBool("correndo", false);
        anim.SetBool("entrarNoCasco", false);
    }

    private void Andar()
    {
        speed = 5f;
        anim.SetBool("parado", false);
        anim.SetBool("andando", true);
        anim.SetBool("correndo", false);
        anim.SetBool("entrarNoCasco", false);
    }

    private void Correr()
    {
        speed = 12f;
        anim.SetBool("parado", false);
        anim.SetBool("andando", true);
        anim.SetBool("correndo", true);
        anim.SetBool("entrarNoCasco", false);
    }

    private void Pular()
    {
        anim.SetBool("parado", false);
        anim.SetBool("andando", true);
        anim.SetBool("correndo", true);
        anim.SetBool("entrarNoCasco", true);
        Velocity.y = Mathf.Sqrt(jumpheight * -2f * gravity);
        isGrounded = false;
    }

    private void AtivarConversa()
    {
        caixaDeConversaNPC.SetActive(true);
        isActiveBoxNPC = true;
    }

    private void DesativarConversa()
    {
        caixaDeConversaNPC.SetActive(false);
        isActiveBoxNPC = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    private void OnTriggerEnter(Collider npcCoelho)
    {
        isTriggerNPC = true;
    }

    private void OnTriggerExit(Collider npcCoelho)
    {
        Debug.Log("Saiu da area de Ativação");
        isTriggerNPC = false;
        caixaDeConversaNPC.SetActive(false);
        isActiveBoxNPC = false;
    }

}