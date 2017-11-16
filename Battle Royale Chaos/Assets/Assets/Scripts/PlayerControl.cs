using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed = 5.0f;
    public float jump = 15.0f;

    public float explosionForce = 10f;
    public float explosionRadius = 10f;

    Vector3 move;
    bool doJump;

    Rigidbody rb;

    public Rigidbody otherPlayer;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        move = Vector3.zero;

        if (Input.GetKey(KeyCode.D))
        {
            //transform.position += Vector3.right * speed * Time.deltaTime;
            move += speed * Vector3.right;
        }
        if (Input.GetKey(KeyCode.A))
        {
            //transform.position += Vector3.left * speed * Time.deltaTime;
            move -= speed * Vector3.right;
        }
        if (Input.GetKeyDown(KeyCode.W) && IsGrounded())
        {
            //transform.position += Vector3.up * jump * Time.deltaTime;
            doJump = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            otherPlayer.AddExplosionForce(explosionForce, transform.position, explosionRadius);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = move + rb.velocity.y * Vector3.up;

        if (doJump)
        {
            rb.AddForce(jump * Vector3.up, ForceMode.Impulse);
            doJump = false;
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.5f * transform.localScale.y + 0.0001f);
    }
}
