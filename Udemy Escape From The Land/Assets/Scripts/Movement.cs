using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //PARAMETERS - Things to tune in the editor
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotateThrust = 100f;
    [SerializeField] AudioClip mainThrustSound;
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
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if (!rocketSound.isPlaying)
            {
                rocketSound.PlayOneShot(mainThrustSound, 1);
            }
        }
        else
        {
            rocketSound.Stop();            
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Rotate(rotateThrust);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Rotate(-rotateThrust);
        }
    }

    void Rotate(float RotationThisFrame)
    {
        rb.freezeRotation = true; //freezeing so we can manually rotate
        transform.Rotate(Vector3.forward * RotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; //so the physics system can do its thing
    }
}
