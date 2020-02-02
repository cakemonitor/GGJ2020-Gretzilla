using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject planet;
    public float moveSpeed = 8.0f;
    public float turnSpeed = 180.0f;
    public float smashCooldown = 1.0f;
    public GameObject smashVFXPrefab;
    public AudioSource jumpSound;

    public Image cooldownImage;

    Rigidbody rBody;
    Vector2 motion = Vector2.zero;
    Transform centerPivot;
    GameObject movementTarget;
    float angularMovementSpeed;
    GameObject damageArea;
    float planetRadius;
    float currentSmashCooldown;
    float attackDuration = 0.25f;
    Animator animator;
    bool isStomping = false;
    public AudioSource setpsAudio;

    void Start()
    {
        animator = GetComponent<Animator>();
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

        motion.x = Input.GetAxis("Horizontal");
        motion.y = Input.GetAxis("Vertical");

        centerPivot.Rotate(motion.y * angularMovementSpeed * Time.deltaTime, 0, motion.x * turnSpeed * Time.deltaTime);

        if (currentSmashCooldown > 0)
        {
            currentSmashCooldown -= Time.deltaTime;
            if (currentSmashCooldown <= 0.0f)
            {
                currentSmashCooldown = 0.0f;
                isStomping = false;
            }
            cooldownImage.fillAmount = 1.0f - (currentSmashCooldown / smashCooldown);
        }
        else if (Input.GetButtonDown("Attack") && !isStomping)
        {
            isStomping = true;
            animator.Play("Smash");
        }

        if (setpsAudio.isPlaying && motion.magnitude < 0.1f)
            setpsAudio.loop = false;
        else if (!setpsAudio.isPlaying && motion.magnitude > 0.1f)
        {
            setpsAudio.Play();
            setpsAudio.loop = true;
        }
    }

    void Smash()
    {
        currentSmashCooldown = smashCooldown;
        damageArea.SetActive(true);
        Invoke("DeactiveDamageArea", attackDuration);
        jumpSound.Play();
        CameraController.AddTrauma(1.0f);
        GameObject smashVFX = Instantiate(smashVFXPrefab, transform.position, transform.rotation);
        Destroy(smashVFX, 2f);
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
