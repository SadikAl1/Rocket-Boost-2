 using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] float rotationStrength = 100f ; 
    [SerializeField] float thrustStrength = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] ParticleSystem thrustPartical;
    [SerializeField] ParticleSystem rightThrusterParticle;
    [SerializeField] ParticleSystem leftThrusterParticle;

    Rigidbody rb;
    AudioSource audioSource;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }
    private void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();

    }
    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }

    }
    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * Time.fixedDeltaTime * thrustStrength);
        thrustPartical.Play();
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
    }
    private void StopThrusting()
    {
        audioSource.Stop();
    }
    private void ProcessRotation() {
        float rotationInput = rotation.ReadValue<float>();
        if (rotationInput < 0)
        {
            RotateRight();
        }
        else if ( rotationInput > 0 )
        {
            RotateLeft();
        }
    }
    private void RotateRight()
    {
        ApplyRotation(-rotationStrength);
        rightThrusterParticle.Play();
    }
    private void RotateLeft()
    {
        ApplyRotation(rotationStrength);
        leftThrusterParticle.Play();
    }
    private void ApplyRotation(float rotateThisFrame
        )
    {
        rb.freezeRotation = true;
        
        transform.Rotate(Vector3.back * rotateThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false; 
    }
}
