using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        EventManager.Instance.onPlayerDied.Invoke();
    }
}