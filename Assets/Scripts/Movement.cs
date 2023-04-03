using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;

    [SerializeField] float mainPerformance = 1000f;
    [SerializeField] float rotationPerformance = 100f;
    [SerializeField] ParticleSystem engineAnimation;
    [SerializeField] ParticleSystem leftNozzleAnimation;
    [SerializeField] ParticleSystem rightNozzleAnimation;
    [SerializeField] AudioClip engine;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Thrust();
        Rotation();
    }

void Thrust()
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

    void Rotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotation();
        }
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainPerformance * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(engine);
        }
        if (!engineAnimation.isPlaying)
        {
            engineAnimation.Play();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        engineAnimation.Stop();
    }

    private void RotateRight()
    {
        ApplyRotation(-rotationPerformance);
        if (!leftNozzleAnimation.isPlaying)
        {
            leftNozzleAnimation.Play();
        }
    }

    private void RotateLeft()
    {
        ApplyRotation(rotationPerformance);
        if (!rightNozzleAnimation.isPlaying)
        {
            rightNozzleAnimation.Play();
        }
    }
    
    private void StopRotation()
    {
        rightNozzleAnimation.Stop();
        leftNozzleAnimation.Stop();
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;  
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; 
    }
}
