using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //CACHE (anything that will be called each game)
    private Rigidbody rb;
    private AudioSource myAudioSource;

    //PARAMETERS (variables)
    [Header("Variables")]
    //[SerializeField] private Vector3 forceUp = new Vector3 (0f,1f,0f);
    [SerializeField] private float mainThrust = 10f;
    [SerializeField] private float rotateSpeed = 2f;
    [SerializeField] private AudioClip thrustSfx;

    [SerializeField] private ParticleSystem upThrustParticleSystem;
    [SerializeField] private ParticleSystem leftThrustParticleSystem;
    [SerializeField] private ParticleSystem rightThrustParticleSystem;
    
    //STATES (bools)
    
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
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }
    
    void ProcessRotation()
    { 
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) )
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    void StartThrusting()
    {
        //rb.AddRelativeForce(forceUp);NO!!
        //vector 3 is upward, multiplied by the thrust force i want, multiplied by time so it's frame-independent.
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!myAudioSource.isPlaying)
        { 
            Debug.Log("playing thrust");
            myAudioSource.PlayOneShot(thrustSfx);
        }

        if (!upThrustParticleSystem.isPlaying)
        {
            upThrustParticleSystem.Play();
        }
    }

    void StopThrusting()
    {
        upThrustParticleSystem.Stop();
        myAudioSource.Stop();
    }

    void RotateLeft()
    {
        ApplyRotation(rotateSpeed);
        if (!leftThrustParticleSystem.isPlaying)
        {
            leftThrustParticleSystem.Play();
        }
    }

    void RotateRight()
    {
        //transform.Rotate(-Vector3.forward * rotateSpeed * Time.deltaTime);
        ApplyRotation(-rotateSpeed);
        if (!rightThrustParticleSystem.isPlaying)
        {
            rightThrustParticleSystem.Play();
        }
    }

    void StopRotating()
    {
        leftThrustParticleSystem.Stop();
        rightThrustParticleSystem.Stop();
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
