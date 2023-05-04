using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 1000f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem MainBooster;
    [SerializeField] ParticleSystem LeftBooster;
    [SerializeField] ParticleSystem RightBooster;

    AudioSource audioSource;
    Rigidbody rb;
    bool isAlive;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey("space")) {
            StartThrusting();
        }
        else {
            StopThrusting();
        }
       
    }
    
    void ProcessRotation()
    {
        if (Input.GetKey("a")) {
            RotateLeft();
        }
        else if (Input.GetKey("d")) {
            RotateRight();
        }
        else {
            StopRotating();
        }
        
    }

    void StartThrusting() 
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying) {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!MainBooster.isPlaying) {
            MainBooster.Play();
        }
    }
    
    void StopThrusting() 
    {
        audioSource.Stop();
        MainBooster.Stop();
    }



    void RotateLeft()
    {
        ApplyRotation(rotationThrust);
        if (!LeftBooster.isPlaying) {
            LeftBooster.Play();
        }
    }

    void RotateRight()
    {
        ApplyRotation(-rotationThrust);
        if (!RightBooster.isPlaying) {
            RightBooster.Play();
        }
    }
    
    void StopRotating()
    {
        RightBooster.Stop();
        LeftBooster.Stop();
    }

    void ApplyRotation(float rotateThisFrame)
    {
        rb.freezeRotation = true; //freeze rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotateThisFrame * Time.deltaTime); 
        rb.freezeRotation = false; //unfreeze rotation so the physics system can take over

        //freeze rotation so we can rotate the object if it hit obstacles
    }
}
