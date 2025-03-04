using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Rocket : MonoBehaviour
{
    private Rigidbody rigidBody;
    private AudioSource mainEngine;
    public AudioClip thrustSound;
    public AudioClip deathSound;
    public AudioClip winSound;
    
    [SerializeField]private ParticleSystem thrustParticle;
    [SerializeField]private ParticleSystem deathParticle;
    [SerializeField]private ParticleSystem winParticle;
    [SerializeField]private FixedJoystick joyStick;
    [SerializeField]private ThrustButton thrustButton;

    [SerializeField]private float rotationspeed;
    [SerializeField]private float thrustSpeed;
    enum State{Alive, Dying, Transcending}
    State state = State.Alive;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        mainEngine = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(state == State.Alive){
            RespondToThrustInput();
            Rotate();
        }
        
    }
    void OnCollisionEnter(Collision collision){
        if(state != State.Alive){
            return;
        }
        switch(collision.gameObject.tag){
            case "Friendly":
            break;
            case "Finish":
                state = State.Transcending;
                mainEngine.Stop();
                mainEngine.PlayOneShot(winSound);
                winParticle.Play();
                Invoke("LoadNextScene", 1f);
                break;
            default:
                state = State.Dying;
                mainEngine.Stop();
                mainEngine.PlayOneShot(deathSound);
                deathParticle.Play();
                Invoke("Death", 1f);
                break;
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Rotate()
    {
        rigidBody.angularVelocity = Vector3.zero;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationspeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationspeed * Time.deltaTime);
        }
        else if (joyStick.Horizontal < 0)
        {
            transform.Rotate(Vector3.forward * rotationspeed * Time.deltaTime);
        }
        else if (joyStick.Horizontal > 0)
        {
            transform.Rotate(-Vector3.forward * rotationspeed * Time.deltaTime);
        }
    }

    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        if (thrustButton.pressed == true){
            ApplyThrust();
        }
        else
        {
            mainEngine.Stop();
            thrustParticle.Stop();
        }
    }

    private void ApplyThrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);
        if (!mainEngine.isPlaying)
        {
            mainEngine.PlayOneShot(thrustSound);
        }
        thrustParticle.Play();
    }
}
