using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float minValue;
    [SerializeField] private float maxValue;

    private float rotationSpeed;

    private void Start()
    {
        rotationSpeed = Random.Range(minValue,maxValue);
    }
    private void FixedUpdate()
    {
        transform.Rotate(0,0, rotationSpeed);
    }
}
