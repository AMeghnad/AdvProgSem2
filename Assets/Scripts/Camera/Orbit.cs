using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 1f, 0);
    public float distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float distanceMin = .5f;
    public float distanceMax = 15f;

    [Header("Collision")]
    public bool cameraCollision = false;
    public float rayDistance = 1000f;
    public LayerMask ignoreLayers;

    private Vector3 originalOffset;
    private float x = 0.0f;
    private float y = 0.0f;

    // Use this for initialisation
    void Start()
    {
        // Calculate offset of camera at start
        originalOffset = transform.position - target.position;
        // Ray distance is as long as the magnitude of offset
        rayDistance = originalOffset.magnitude;

        transform.SetParent(null);
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    public void Look(float mouseX, float mouseY)
    {
        x += mouseX * xSpeed * Time.deltaTime;
        y += mouseY * ySpeed * Time.deltaTime;

        y = ClampAngle(y, yMinLimit, yMaxLimit);

        Quaternion rotation = Quaternion.Euler(y, x, 0);

        transform.rotation = rotation;
    }

    void FixedUpdate()
    {
        if (target)
        {
            if (cameraCollision)
            {
                Ray camRay = new Ray(target.position, -transform.forward);
                RaycastHit hit;
                if (Physics.Raycast(camRay, out hit, rayDistance, ~ignoreLayers, QueryTriggerInteraction.Ignore))
                {
                    distance = hit.distance;
                    //return
                    return;
                }
            }

            // Reset distance if not hitting
            distance = originalOffset.magnitude;
        }

    }

    void LateUpdate()
    {
        if (target)
        {
            transform.position = target.position + -transform.forward * distance;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}