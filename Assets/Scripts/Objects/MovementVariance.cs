using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MovementVariance : MonoBehaviour
{
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;

    private float speed;

    private Transform parentTransform;

    private void Awake()
    {
        parentTransform = GetComponent<Transform>();
    }

    private void OnEnable()
    {
        speed = Random.Range(minSpeed, maxSpeed);
    }

    private void FixedUpdate()
    {
        parentTransform.Rotate(0, 0, speed * Time.fixedDeltaTime);
    }
}