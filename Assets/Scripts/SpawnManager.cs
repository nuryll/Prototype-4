using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemyStrongPrefab; // Prefab for the stronger enemy


    private float spawnRange = 9.0f;
    public int enemyCount;
    public int waveNumber = 1;
    public GameObject powerupPrefab;

    private GameManager gameManager;


    void Start()
    {

        SpawnEnemyWave(waveNumber);
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager == null || gameManager.isGameOver) return;

        enemyCount = FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length;

        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        }

        if (gameManager.isGameOver) return;


    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return randomPos;
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        if (gameManager != null && gameManager.isGameOver) return;

        // ?? Only spawn strong enemies starting from wave 2
        if (waveNumber < 2)
        {
            // First wave = only normal enemies
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
            }
        }
        else
        {
            // From wave 2 onward = mix of normal and strong enemies
            int normalEnemyCount = Mathf.RoundToInt(enemiesToSpawn * 0.7f); // 70% normal
            int strongEnemyCount = enemiesToSpawn - normalEnemyCount;       // 30% strong

            for (int i = 0; i < normalEnemyCount; i++)
            {
                Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
            }

            for (int i = 0; i < strongEnemyCount; i++)
            {
                Instantiate(enemyStrongPrefab, GenerateSpawnPosition(), enemyStrongPrefab.transform.rotation);
            }
        }
    }
}
