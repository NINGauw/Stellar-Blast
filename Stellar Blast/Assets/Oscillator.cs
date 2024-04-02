using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    [SerializeField]private Vector3 movement;
    [Range(0,1)] [SerializeField]float movementFactor;
    private Vector3 startingPos;
    void Start()
    {
        startingPos = transform.position;
    }
    void Update()
    {
        Vector3 offset = movement * movementFactor;
        transform.position = startingPos + offset;
    }
}
