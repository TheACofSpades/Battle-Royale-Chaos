﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl1 : MonoBehaviour
{
    private float speed = 5.0f;
    private float jump = 15.0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.up * jump * Time.deltaTime;
        }
    }
}
