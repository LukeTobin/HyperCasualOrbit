using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    /// <summary>
    /// Since the interactable variable only needs to be accessed once, using a getter is more performant
    /// </summary>
    IInteractable interactable
    {
        get
        {
            return (IInteractable)GetComponentInParent(typeof(IInteractable));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponentInParent<Player>() != null)
        {
            interactable?.Interact();
        }
    }
}
