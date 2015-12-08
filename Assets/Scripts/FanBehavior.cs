﻿using UnityEngine;
using System.Collections;

public class FanBehavior : MonoBehaviour {
    private Animator animator;
    public string input = "f";
    public int estadoFan;
    public bool teclaF;
    public Manager manager;
    private Rigidbody rb;

    void Awake()
    {
       animator = GetComponent<Animator>();
       animator.SetInteger("fan_state", 0);
       estadoFan = 0;
       rb = this.GetComponent<Rigidbody>();
    }
    // Use this for initialization



    // Update is called once per frame
    void Update () {
        teclaF = Input.GetKey(KeyCode.F) ;
        if (teclaF)
        {
            if (estadoFan ==0)
            {
                estadoFan=1;
                animator.SetInteger("fan_state", 1);
            }
            else if (estadoFan == 1)
            {
                estadoFan = 2;
                animator.SetInteger("fan_state", 2);
            }
            else if (estadoFan == 2)
            {
                estadoFan = 3;
                animator.SetInteger("fan_state", 3);
            }
            else if (estadoFan == 3)
            {
                estadoFan = 4;
                animator.SetInteger("fan_state", 4);
            }
            else
            {
                estadoFan = 0;
                animator.SetInteger("fan_state", 0);
            }
        }
	}

    void FixedUpdate() {
        rb.position += new Vector3(manager.velocidadEdif, 0.0f, 0.0f);
    }
}
