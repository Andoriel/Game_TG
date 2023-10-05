using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movConstant : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = Vector3.forward * speed;

    }
    void Jump()
    {

    }
}
