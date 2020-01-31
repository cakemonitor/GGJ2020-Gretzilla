using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject planet;
    public float moveSpeed = 8.0f;

    Rigidbody rBody;
    Vector2 motion = Vector2.zero;

    Transform centerPivot;
    GameObject movementTarget;

    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        centerPivot = new GameObject().transform;
        centerPivot.name = "Player Center Pivot";
        centerPivot.position = planet.transform.position;
        movementTarget = new GameObject();
        movementTarget.name = "Player Movement Target";
        movementTarget.transform.position = transform.position;
        movementTarget.transform.SetParent(centerPivot.transform);
    }

    void Update()
    {
        motion.x = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        motion.y = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        centerPivot.Rotate(motion.y, motion.x, 0);
    }
}
