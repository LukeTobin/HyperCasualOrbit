using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("External References")]
    [SerializeField] private TMP_Text tapToStartText;

    public bool GameRunning { get; private set; }

    private void Awake()
    {
        Instance = this;

        GameRunning = false;
    }

    private void OnEnable()
    {
        EventManager.Instance.onPlayerDied.AddListener(EndGame);
        EventManager.Instance.onGameStarted.AddListener(StartGame);
    }

    private void OnDisable()
    {
        EventManager.Instance.onPlayerDied.RemoveListener(EndGame);
        EventManager.Instance.onGameStarted.RemoveListener(StartGame);
    }

    private void Update()
    {
        // Mobile Input
        if (Input.touchCount > 0 && !GameRunning)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                EventManager.Instance.onGameStarted.Invoke();
            }
        }

        // Mouse Input
        if (Input.GetMouseButtonDown(0) && !GameRunning)
        {
            EventManager.Instance.onGameStarted.Invoke();
        }
    }

    public void EndGame()
    {
        GameRunning = false;
        tapToStartText.gameObject.SetActive(true);
        ObjectPooler.Instance.ReleaseAllPools();
    }

    public void StartGame()
    {
        GameRunning = true;
        tapToStartText.gameObject.SetActive(false);
    }
}
