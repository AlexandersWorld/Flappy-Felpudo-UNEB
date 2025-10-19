using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public GameObject straightEnemyPrefab;
    public GameObject chaserEnemyPrefab;

    [Header("Spawn Settings")]
    public float spawnInterval = 2f;
    public float spawnHeightRange = 4f;
    public float spawnXOffset = 20f;

    private float nextSpawnTime = 0f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnEnemy()
    {
        if (!player) return;

        Vector3 spawnPos = new Vector3(player.position.x + spawnXOffset,
                                       Random.Range(-spawnHeightRange, spawnHeightRange),
                                       0f);

        GameObject prefabToSpawn = Random.value > 0.5f ? straightEnemyPrefab : chaserEnemyPrefab;
        Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
    }
}
