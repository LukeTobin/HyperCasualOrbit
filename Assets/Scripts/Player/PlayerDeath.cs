using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private ParticleSystem deathPfx;

    public Player player { private get; set; }

    public bool isDead { get; private set; }

    private void OnEnable()
    {
        // Init death state, wait for user to start game
        isDead = true;

        EventManager.Instance.onPlayerDied.AddListener(Kill);
        EventManager.Instance.onGameStarted.AddListener(ResetPlayer);
    }

    private void OnDisable()
    {
        EventManager.Instance.onPlayerDied.RemoveListener(Kill);
        EventManager.Instance.onGameStarted.RemoveListener(ResetPlayer);
    }

    /// <summary>
    /// Disable the visuals of the player object and play the explosion PFX. Pause all jump & movements occoruing
    /// </summary>
    private void Kill()
    {
        isDead = true;
        
        player.VisualBody.gameObject.SetActive(false);

        deathPfx.gameObject.transform.localPosition = player.VisualBody.localPosition;
        deathPfx.Play();

        if (player.IsMoving) StopCoroutine(player.movement.MoveVisualBody());
    }

    /// <summary>
    /// Reset the player position and kill any active pfx playing
    /// </summary>
    private void ResetPlayer()
    {
        isDead = false;

        player.VisualBody.gameObject.SetActive(true);
        player.VisualBody.transform.localPosition = new Vector3(12, 0, 0);

        if (deathPfx.isPlaying) deathPfx.Stop();
    }
}
