using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //PARAMETERS - Things to tune in the editor
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotateThrust = 100f;
    [SerializeField] AudioClip mainThrustSound;
    [SerializeField] ParticleSystem mainThrustPart;
    [SerializeField] ParticleSystem leftThrustPart;
    [SerializeField] ParticleSystem rightThrustPart;

    //CACHED DATA
    Rigidbody rb;
    AudioSource rocketSound;
    
    void Start() 
    {
        rb = GetComponent<Rigidbody>();
        rocketSound = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrust();
        }
        else
        {
            StopThrusting();
        }
    }

    void StartThrust()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!rocketSound.isPlaying)
        {
            rocketSound.PlayOneShot(mainThrustSound, 1);
        }
        if (!mainThrustPart.isPlaying)
        {
            mainThrustPart.Play();
        }
    }

    private void StopThrusting()
    {
        rocketSound.Stop();
        mainThrustPart.Stop();
    }

        void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Rotate(rotateThrust);
            ThrustParticleProcessing(rightThrustPart);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Rotate(-rotateThrust);
            ThrustParticleProcessing(leftThrustPart);
        }
        else
        {
            StopDirectionalThrust();
        }
    }

    void Rotate(float RotationThisFrame)
    {
        rb.freezeRotation = true; //freezeing so we can manually rotate
        transform.Rotate(Vector3.forward * RotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; //so the physics system can do its thing
    }

        void ThrustParticleProcessing(ParticleSystem thrustPart)
    {
        if(!thrustPart.isPlaying)
            {
                thrustPart.Play();
            }
    }

    void StopDirectionalThrust()
    {
        leftThrustPart.Stop();
        rightThrustPart.Stop();
    }
}
