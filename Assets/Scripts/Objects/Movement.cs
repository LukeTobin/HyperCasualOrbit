using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float defaultSpeed;
    
    private Transform parentTransform;

    private void Awake()
    {
        parentTransform = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        parentTransform.Rotate(0, 0, defaultSpeed * Time.fixedDeltaTime);
    }
}