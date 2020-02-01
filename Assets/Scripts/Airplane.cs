using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.parent.transform.Rotate((2 * Time.deltaTime), 0, 0);   
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
