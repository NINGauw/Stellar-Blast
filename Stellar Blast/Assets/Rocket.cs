using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Rigidbody rigidBody;
    [SerializeField]private AudioSource thrustSound;
    [SerializeField]private float rotationspeed;
    [SerializeField]private float thrustSpeed;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Thrust();
        Rotate();
    }
    void OnCollisionEnter(Collision collision){
        switch(collision.gameObject.tag){
            case "Friendly":
            print("friendly");
            break;
            default:
            print("dead");
            break;
        }
    }
    private void Rotate()
    {
        rigidBody.freezeRotation = true;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationspeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationspeed * Time.deltaTime);
        }
        rigidBody.freezeRotation = false;
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);
            if (!thrustSound.isPlaying)
            {
                thrustSound.Play();
            }
        }
        else
        {
            thrustSound.Stop();
        }
    }
}
