using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueCollectable : Collectable
{
    public override void Interact()
    {
        ScoreManager.Instance.Score += collectionPoints;
        gameObject.SetActive(false);
    }
}
