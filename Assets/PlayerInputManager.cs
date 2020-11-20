﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : PawnController
{
    public float speed = 1;
    public Transform parentTransform;

    public override void Start()
    {
        base.Start();
  //      parentTransform = transform.parent;
    }

    public void Update()
    {
        if (Input.GetButton("AttackButton"))
        {
            Attack();
        }

        if (Input.GetAxis("Horizontal") != 0 && InputEnabled) 
        {
            Vector3 newPosition = new Vector3(transform.position.x + Input.GetAxis("Horizontal") * speed * Time.deltaTime, transform.position.y, transform.position.z);
  //          transform.position = newPosition;
            UpdateSpeedVariable(151f);
        }
        else UpdateSpeedVariable(0f);

        if (Input.GetAxis("Vertical") != 0 && InputEnabled)
        {
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + Input.GetAxis("Vertical") * speed * Time.deltaTime);
 //           transform.position = newPosition;
            UpdateSpeedVariable(151f);
        }
    }
}