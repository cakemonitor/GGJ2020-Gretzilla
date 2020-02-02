using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ICBM : MonoBehaviour
{
    public Transform PlayerTransform;

    public float Speed;
    public float TargettingHeight;

    void Awake()
    {
        if (PlayerTransform == null)
        {
            PlayerController TempPlayerController = (PlayerController)FindObjectOfType(typeof(PlayerController));
            GameObject TempPlayer = TempPlayerController.gameObject;
            PlayerTransform = TempPlayer.transform;
        }
    }

    void Update()
    {
        if (TargettingHeight != 0)
        {
            Move();
            TargettingHeight -= 1;
        }
        else
        {
            TargetPlayer();
            Move();
        }
    }

    void Move()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }

    void TargetPlayer()
    {
        // Determine which direction to rotate towards
        Vector3 TargetDirection = (PlayerTransform.position - transform.position);
        // The step size is equal to speed times frame time.
        float SingleStep = ((Speed * 0.1f) * Time.deltaTime);
        // Rotate the forward vector towards the target direction by one step
        Vector3 Target = Vector3.RotateTowards(transform.forward, TargetDirection, SingleStep, 0.0f);
        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(Target);
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
