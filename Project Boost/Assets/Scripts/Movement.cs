using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngineSFX;
    [SerializeField] ParticleSystem engineFX;

    Rigidbody rb;
    AudioSource audioSource;

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

    void ProcessThrust() {
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
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);
        }
    }

    void StartThrusting()
    {
        // Vector3.up is same as (0,1,0). Time.deltaTime for frame independent speed
        // Relative force add force relative to object's direction
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        // SFX
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngineSFX);
        }
        // FX
        if (!engineFX.isPlaying)
        {
            engineFX.Play();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        engineFX.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        // physics system conflicts with update. so freeze physics system when controlling
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }

}
