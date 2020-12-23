using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotionControl : MonoBehaviour
{
    public GameObject RootMotion;

    void Update()
    {
        transform.position = RootMotion.transform.position;
    }
}
