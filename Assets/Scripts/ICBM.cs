using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ICBM : MonoBehaviour
{
    public Transform PlayerTransform;

    public GameObject ClosestBuildiung;
    public GameObject Player;

    public float Speed;
    public float TargettingHeight;

    public bool AttackPlayer = false;

    void Awake()
    {
        if (PlayerTransform == null)
        {
            PlayerController TempPlayerController = (PlayerController)FindObjectOfType(typeof(PlayerController));
            Player = TempPlayerController.gameObject;
            PlayerTransform = Player.transform;
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
            if (AttackPlayer == true)
            {
                TargetPlayer();
            }
            else
            {
                TargetBuilding();
            }
                Move();
            
        }
    }

    void Move()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }

    void TargetBuilding()
    {
        if (Pollution.CleanBuildings.Count > 0)
        {
            float Distance = 0;
            float ClosestDistance = 999999;

            for (int i = 0; i < Pollution.CleanBuildings.Count; i++)
            {
                if (Pollution.CleanBuildings[i] != null)
                {
                    Distance = Vector3.Distance(transform.position, Pollution.CleanBuildings[i].transform.position);

                    if (ClosestDistance >= 999999)
                    {
                        ClosestBuildiung = Pollution.CleanBuildings[i];
                        ClosestDistance = Distance;
                    }
                    else
                    {
                        if (Distance < ClosestDistance)
                        {
                            ClosestBuildiung = Pollution.CleanBuildings[i];
                            ClosestDistance = Distance;
                        }
                    }
                }
            }

            // Determine which direction to rotate towards
            Vector3 TargetDirection = (ClosestBuildiung.transform.position - transform.position);
            // The step size is equal to speed times frame time.
            float SingleStep = ((Speed * 0.1f) * Time.deltaTime);
            // Rotate the forward vector towards the target direction by one step
            Vector3 Target = Vector3.RotateTowards(transform.forward, TargetDirection, SingleStep, 0.0f);
            // Calculate a rotation a step closer to the target and applies rotation to this object
            transform.rotation = Quaternion.LookRotation(Target);
        }
        else
        {
            TargetPlayer();
        }
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
        if (collision.gameObject.tag == "Destroyable")
        {
            if (collision.gameObject.GetComponent<Factory>().enabled == true)
            {
                collision.gameObject.GetComponent<Factory>().Smash();
            }
        }

        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            AttackPlayer = true;
        }
    }
}
