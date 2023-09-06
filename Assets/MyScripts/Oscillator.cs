using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    private Vector3 startingPosition;
    [SerializeField] private Vector3 movementVector;
    [Range(0,1)] private float movementFactor;
    [SerializeField] private float period = 2f;
    
    void Start()
    {
        startingPosition = transform.position;
        Debug.Log("starting position is: " + startingPosition);
    }

    // Update is called once per frame
    void Update()
    {
        //Sin draws a wave. waves are like circles if you drew the  heights and spread it across a line.. it's confusing.
        //tau is a math term that means 2pi. A circle's circumference divided by tau is the radius (should be a little over 6)
        //A period is the length of a wave?

        //don't usually want to do 0 because it's not necessarily "absolute". mathf.episode is a better way to do it. Mathf.Episilon is just a very tiny number.
        if (period <= Mathf.Epsilon ) { return; }
        
        float cycles = Time.time / period ;
        
        const float tau = Mathf.PI * 2; //constant value
        float rawSignWave = Mathf.Sin(cycles * tau); //going from -1 to 1
        
        //Debug.Log(rawSinWave); // should be a range from -1 to 1, going back and forth, since time progresses.

        //below: makes it go from 0 to 2 instead of -1 to 1. then divide by 2 so it stays under 1
        movementFactor = (rawSignWave + 1f) / 2; // recalculated to be 0-1, so it's cleaner
        
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
