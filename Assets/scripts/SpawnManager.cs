using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    [SerializeField] private GameObject straightEnemyPrefab;
    [SerializeField] private GameObject chaserEnemyPrefab;
    [SerializeField] private GameObject Uruca;
    [SerializeField] private Transform UrucaSpawnPoint;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float spawnHeightRange = 4f;
    [SerializeField] private float spawnXOffset = 20f;

    private float nextSpawnTime = 0f;
    private Transform player;
    private bool bossFightStarts = false;

    private bool canSpawn = true;

    void Start()
    {
        canSpawn = true;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (ControlaJogador._instance.isGameOver()) return;

        if (Time.time >= nextSpawnTime && canSpawn)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval;
        }

        if (GameManager._instance.GetGameTime() <= -5 && !bossFightStarts)
        {
            bossFightStarts = true;
            Instantiate(Uruca, UrucaSpawnPoint.position, Quaternion.identity);
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

    public void StopSpawning()
    {
        canSpawn = false;
    }

    public void StartSpawning()
    {
        canSpawn = true;
    }

    public float GetSpawnInterval()
    {
        return spawnInterval;
    }

    public void SetSpawnInterval(float value)
    {
        spawnInterval = value;
    }
}
