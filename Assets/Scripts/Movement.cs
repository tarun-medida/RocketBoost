using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Parameters - for tuning, typically set in the editor

    // Cache - e.g. reference for readability or speed

    // State - private instance (member) variables


    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotateThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem boost;
    [SerializeField] ParticleSystem wingBoostLeft;
    [SerializeField] ParticleSystem wingBoostRight;

    Rigidbody rb;
    AudioSource audioSource;
   

    // Start is called before the first frame update
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
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
            //audioSource.Play();
        }
        if (!boost.isPlaying)
        {
            boost.Play();
        }
    }
    void StopThrusting()
    {
        audioSource.Stop();
        boost.Stop();
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotate();
        }
    }
    void RotateLeft()
    {
        ApplyRotation(rotateThrust);
        if (!wingBoostLeft.isPlaying)
            wingBoostLeft.Play();
    }

    void RotateRight()
    {
        ApplyRotation(-rotateThrust);
        if (!wingBoostRight.isPlaying)
            wingBoostRight.Play();
    }

    void StopRotate()
    {
        wingBoostLeft.Stop();
        wingBoostRight.Stop();
    }

    

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
