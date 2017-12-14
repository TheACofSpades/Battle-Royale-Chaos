using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl1 : MonoBehaviour
{
    public float speed = 5.0f;
    public float jump = 6.0f;
    public float maxSpeed = 5.0f;
    public float horizontalDamping = 0.1f;

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

        if (Input.GetKey(KeyCode.RightArrow))
        {
            //transform.position += Vector3.right * speed * Time.deltaTime;
            move += speed * Vector3.right;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //transform.position += Vector3.left * speed * Time.deltaTime;
            move -= speed * Vector3.right;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded())
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
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.5f * transform.localScale.y + 0.0001f);
    }
}
