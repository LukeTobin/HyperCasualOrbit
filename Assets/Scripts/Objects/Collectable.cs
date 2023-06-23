using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour, IInteractable
{
    [SerializeField] protected int collectionPoints = 20;
    
    /// <summary>
    /// Increment score & return object to pool
    /// </summary>
    public virtual void Interact()
    {
        ScoreManager.Instance.Score += collectionPoints;
        gameObject.SetActive(false);
        SpawnManager.Instance.CollectedCollectable();
    }
}