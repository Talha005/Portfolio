using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassSway : MonoBehaviour
{
    public float windStrength = 1.0f; // Adjust the strength of the wind effect
    public Vector3 windDirection = Vector3.right; // Adjust the direction of the wind

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        ApplyWind();
    }

    private void ApplyWind()
    {
        // Calculate the force of the wind based on windStrength and windDirection
        Vector3 windForce = windStrength * windDirection;

        // Apply the wind force to the Rigidbody
        rb.AddForce(windForce, ForceMode.Force);
    }
}
