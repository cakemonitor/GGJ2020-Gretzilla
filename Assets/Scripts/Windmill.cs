using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windmill : MonoBehaviour
{
    public float turnSpeed = 300.0f;

    void Update()
    {
        transform.Rotate(0.0f, turnSpeed * Time.deltaTime, 0.0f);
    }
}
