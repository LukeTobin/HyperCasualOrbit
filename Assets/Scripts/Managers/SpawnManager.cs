using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    [Header("Spawn Points")]
    [SerializeField] private Vector3[] spawnVectors;

    [Header("External References")]
    [SerializeField] private GameObject collectableObject;
    [SerializeField] private GameObject enemyObject;
    [SerializeField] private GameObject staticCollectableObject;

    private int collectableCount = 0;
    private int enemyCount = 0;

    private const int MAX_COLLECTABLES_IN_SCENE = 4;
    private const int MAX_ENEMIES_IN_SCENE = 5;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        EventManager.Instance.onGameStarted.AddListener(() => StartCoroutine(SpawnCollectables()));
        EventManager.Instance.onGameStarted.AddListener(ResetCounts);
        EventManager.Instance.onPlayerDied.AddListener(() => StopCoroutine(SpawnCollectables()));
        EventManager.Instance.onPlayerFinishedJump.AddListener(SpawnEnemy);
    }

    private void OnDisable()
    {
        EventManager.Instance.onGameStarted.RemoveListener(() => StartCoroutine(SpawnCollectables()));
        EventManager.Instance.onGameStarted.RemoveListener(ResetCounts);
        EventManager.Instance.onPlayerDied.RemoveListener(() => StopCoroutine(SpawnCollectables()));
        EventManager.Instance.onPlayerFinishedJump.RemoveListener(SpawnEnemy);

    }

    private void Spawn(GameObject objectReference)
    {
        // Get corresponding object from pool
        GameObject collectable = ObjectPooler.Instance.Get(objectReference);

        // setup object
        collectable.SetActive(true);
        collectable.transform.GetChild(0).localPosition = spawnVectors[Random.Range(0, spawnVectors.Length)];
        collectable.transform.localRotation = new Quaternion(0, 0, Random.Range(0, 360), 0);
    }

    private IEnumerator SpawnCollectables()
    {
        while (true)
        {
            // Buffer delay, low value used to dictate first spawn
            yield return new WaitForSeconds(1f);

            // GameManager guard clause
            if (!GameManager.Instance.GameRunning) yield break;

            // Collectable count guard clause
            if (collectableCount < MAX_COLLECTABLES_IN_SCENE)
            {
                Spawn(collectableObject);
                collectableCount++;
            }
            
            yield return new WaitForSeconds(Random.Range(4f, 6f));
        }
    }

    private IEnumerator RespawnStaticObject()
    {
        while (true)
        {
            // Buffer delay, low value used to dictate first spawn
            yield return new WaitForSeconds(2f);

            // Reactivate static collectable object
            if(!staticCollectableObject.activeInHierarchy) 
                staticCollectableObject.SetActive(true);
        }
    }

    public void CollectedCollectable()
    {
        collectableCount--;
    }

    private void SpawnEnemy()
    {
        if(enemyCount < MAX_ENEMIES_IN_SCENE)
        {
            Spawn(enemyObject);
            StartCoroutine(RespawnStaticObject());
            enemyCount++;
        }
    }

    private void ResetCounts()
    {
        collectableCount = 0;
        enemyCount = 0;
    }
}