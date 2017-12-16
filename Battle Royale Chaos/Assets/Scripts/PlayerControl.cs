using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed = 5.0f;
    public float jump = 6.0f;
    public float maxSpeed = 5.0f;
    public float horizontalDamping = 0.1f;

    public float explosionForce = 10f;
    public float explosionRadius = 10f;

    public GameObject fists;
    public GameObject rotationPivot;

    Vector3 move;
    bool doJump;

    Rigidbody rb;
    Animator anim;

    public Rigidbody otherPlayer;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
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

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            anim.SetTrigger("DoubleJab");
        }
    }

    private void FixedUpdate()
    {
        Vector3 newVelocity = rb.velocity + move;
        float dampVelocity = 0f;
        if (IsGrounded())
        {
            newVelocity.x = Mathf.SmoothDamp(newVelocity.x, 0, ref dampVelocity, horizontalDamping);
        }
        newVelocity.x = Mathf.Clamp(newVelocity.x, -maxSpeed, maxSpeed);
        rb.velocity = newVelocity;

        if (doJump)
        {
            rb.AddForce(jump * Vector3.up, ForceMode.Impulse);
            doJump = false;
        }

        if (otherPlayer != null)
        {
            Vector3 opponentDirection = otherPlayer.position - rb.position;
            opponentDirection.Normalize();
            float angle = Mathf.Sign(opponentDirection.y) * Mathf.Acos(Vector3.Dot(Vector3.right, opponentDirection));
            print("*** angle = " + angle);
            rotationPivot.transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle);
        }
        //rb.rotation = Quaternion.LookRotation(opponentDirection, Vector3.up);
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.5f * transform.localScale.y + 0.0001f);
    }
}
