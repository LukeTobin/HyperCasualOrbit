using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform visualBody;

    public PlayerInput input { get; private set; }
    public PlayerDeath death { get; private set; }
    public PlayerMovement movement { get; private set; }

    public Transform VisualBody { get => visualBody; }
    public bool IsDead { get => death.isDead; }
    public bool IsMoving { get => movement.isMoving; }

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        input.player = this;

        death = GetComponent<PlayerDeath>();
        death.player = this;

        movement = GetComponent<PlayerMovement>();
        movement.player = this;
    }
}
