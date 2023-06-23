using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Player player { private get; set; }

    private void Update()
    {
        // Mobile Input
        if (Input.touchCount > 0 && !player.IsMoving && !player.IsDead)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                StartCoroutine(player.movement.MoveVisualBody());
            }
        }

        // Mouse Input
        if (Input.GetMouseButtonDown(0) && !player.IsMoving && !player.IsDead)
        {
            StartCoroutine(player.movement.MoveVisualBody());
        }
    }
}