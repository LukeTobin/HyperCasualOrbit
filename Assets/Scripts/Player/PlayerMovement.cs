using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int radiusFromCenter = 12; // distance the visual container of our object is from the center
    [SerializeField] private float rotationSpeed = 10;
    [SerializeField] private float jumpSpeed = 70;

    public Player player { private get; set; }

    public bool isMoving { get; private set; }

    private void FixedUpdate()
    {
        if (!isMoving && !player.IsDead)
        {
            float rotation = rotationSpeed * Time.fixedDeltaTime * radiusFromCenter;
            transform.Rotate(0, 0, rotation);
        }
    }

    /// <summary>
    /// Move the player's visual orb toward to inverse of it's current position, while disabling movement
    /// </summary>
    /// <returns></returns>
    public IEnumerator MoveVisualBody()
    {
        // Disable orbiting
        isMoving = true;

        // Inverse movement direction
        rotationSpeed *= -1;

        Vector3 startPosition = player.VisualBody.localPosition;
        Vector3 targetPosition = new Vector3(startPosition.x * -1, 0, 0);

        // Calculate how long the move should take
        float moveDuration = Vector3.Distance(startPosition, targetPosition) / jumpSpeed;

        float elapsedTime = 0f;
        while (elapsedTime < moveDuration)
        {
            if (player.IsDead)
            {
                // Exit Coroutine if player died & reset movement flag
                isMoving = false;
                yield break;
            }

            float t = elapsedTime / moveDuration;
            player.VisualBody.localPosition = Vector3.Lerp(startPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Force set the position to counteract estimated positioning
        player.VisualBody.localPosition = targetPosition;

        EventManager.Instance.onPlayerFinishedJump.Invoke();

        // Allow orbiting
        isMoving = false;
    }
}
