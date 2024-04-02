using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    [SerializeField]private Vector3 movement;
    [SerializeField]private float period = 2f;
    [Range(0,1)] [SerializeField]float movementFactor;
    private Vector3 startingPos;
    void Start()
    {
        startingPos = transform.position;
    }
    void Update()
    {
        Moving();
    }

    private void Moving()
    {
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);
        movementFactor = rawSinWave / 2f;
        Vector3 offset = movement * movementFactor;
        transform.position = startingPos + offset;
    }
}
