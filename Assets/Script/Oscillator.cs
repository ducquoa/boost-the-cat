using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)] float movementFactor;
    [SerializeField] float period = 2f;
    [SerializeField] float rotationSpeed = 1f;
    
    float rotateFactor;

    void Start()
    {
        startingPosition = transform.position;
        Debug.Log(startingPosition);
        
    }

    
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }
        float xAngle = (rotateFactor + rotationSpeed) * Time.deltaTime;
        const float tau = Mathf.PI * 2;               //constant value of 6.283
        float cycles = Time.time / period;            // continually growing over time
        float rawSinWave = Mathf.Sin(cycles * tau);   //going from -1 to 1

        movementFactor = (rawSinWave + 1f) / 2f;      //recalculated to go from 0 to 1 so its cleaner
        transform.Rotate(xAngle, 0f, 0f);
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;  
    }
}
