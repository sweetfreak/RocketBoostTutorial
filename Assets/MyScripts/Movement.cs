using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody rb;
    private AudioSource myAudioSource;

    //[SerializeField] private Vector3 forceUp = new Vector3 (0f,1f,0f);
    [SerializeField] private float mainThrust = 10f;
    [SerializeField] private float rotateSpeed = 2f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        myAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            //rb.AddRelativeForce(forceUp);NO!!
            //vector 3 is upward, multiplied by the thrust force i want, multiplied by time so it's frame-independent.
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if (!myAudioSource.isPlaying)
            { 
                myAudioSource.Play();

            }

        }
        else
        {
            myAudioSource.Stop();
        }
    }

    void ProcessRotation()
    { 
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            //Debug.Log("Left!");
            ApplyRotation(rotateSpeed);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            //Debug.Log("Right!");
            //transform.Rotate(-Vector3.forward * rotateSpeed * Time.deltaTime);
            ApplyRotation(-rotateSpeed);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        //freeze rotation so we can manually rotate
        rb.freezeRotation = true;
        
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);

        //unfreezes rotation so physics system can take over
        rb.freezeRotation = false;
    }
}
