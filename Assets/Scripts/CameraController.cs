using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraLocator;
    public Transform cameraTarget;
    public float smoothTime = 0.2f;
    public float maxCameraShake = 0.3f;
    public float traumaDecayRate = 0.5f;
    public float traumaPowerFactor = 3.0f;

    Vector3 velocity = Vector3.zero;
    Vector3 shakeOffset = Vector3.zero;

    static float trauma = 0.0f;

    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, cameraLocator.position, ref velocity, smoothTime);
        transform.LookAt(cameraTarget, cameraLocator.up);
    }

    void FixedUpdate()
    {
        if (trauma > 0.0f)
        {
            shakeOffset = Random.insideUnitSphere * maxCameraShake * Mathf.Pow(trauma, traumaPowerFactor);
            transform.position += shakeOffset;
            trauma -= traumaDecayRate * Time.deltaTime;
        }
    }

    public static void AddTrauma(float traumaAmount)
    {
        trauma = Mathf.Clamp01(trauma + traumaAmount);
    }

}
