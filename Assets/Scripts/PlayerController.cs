using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject planet;
    public float moveSpeed = 8.0f;
    public float turnSpeed = 180.0f;
    public float attackDuration = 0.25f;

    Rigidbody rBody;
    Vector2 motion = Vector2.zero;
    Transform centerPivot;
    GameObject movementTarget;
    float angularMovementSpeed;
    GameObject damageArea;
    float planetRadius;

    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        centerPivot = new GameObject().transform;
        centerPivot.name = "Player Center Pivot";
        centerPivot.position = planet.transform.position;
        movementTarget = new GameObject();
        movementTarget.name = "Player Movement Target";
        movementTarget.transform.position = transform.position;
        movementTarget.transform.rotation = transform.rotation;
        movementTarget.transform.SetParent(centerPivot.transform);

        planetRadius = (planet.transform.position - transform.position).magnitude;
        float circumference = Mathf.PI * 2.0f * planetRadius;
        angularMovementSpeed = moveSpeed / circumference * 360.0f;

        damageArea = transform.Find("Damage Area").gameObject;
        DeactiveDamageArea();
    }

    void Update()
    {
        centerPivot.transform.LookAt(transform.position, -transform.forward);
        movementTarget.transform.position = (transform.position - centerPivot.transform.position).normalized * planetRadius + centerPivot.transform.position;

        motion.x = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
        motion.y = Input.GetAxis("Vertical") * angularMovementSpeed * Time.deltaTime;

        centerPivot.Rotate(motion.y, 0, motion.x);

        if (Input.GetButtonDown("Attack") && !damageArea.activeSelf)
        {
            damageArea.SetActive(true);
            Invoke("DeactiveDamageArea", attackDuration);
        }
    }

    void FixedUpdate()
    {
        rBody.velocity = Vector3.zero;
        rBody.MovePosition(movementTarget.transform.position);
        rBody.MoveRotation(centerPivot.transform.rotation * Quaternion.Euler(90.0f, 0.0f, 0.0f));
    }

    void DeactiveDamageArea()
    {
        damageArea.SetActive(false);
    }
}
