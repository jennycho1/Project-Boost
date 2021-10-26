using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    // Obstacle animation
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    // have a slider in the Unity inspector
    [SerializeField] [Range(0,1)] float movementFactor;
    [SerializeField] float period = 2f;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // prevent error caused by dividing zero. chance of hitting exact 0f is very small. So this will 
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period; 
        const float tau = Mathf.PI * 2;
        // Mathf.Sin(input in radians)
        float rawSinWave = Mathf.Sin(cycles + tau);

        movementFactor = (rawSinWave + 1f) / 2f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
