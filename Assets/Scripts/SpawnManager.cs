using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    public GameObject cottonPrefab;

    public bool isGameOver = false;

    private int xRange = 17;

    private float enemySpawnDelay = 2f;
    private float enemySpawnInterval = 1.5f;

    private float powerupSpawnDelay = 6f;
    private float powerupSpawnInterval = 6f;

    private float cottonSpawnDelay = 6f;
    private float cottonSpawnInterval = 6f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemies", enemySpawnDelay, enemySpawnInterval);
        InvokeRepeating("SpawnPowerups", powerupSpawnDelay, powerupSpawnInterval);
        InvokeRepeating("SpawnCotton", cottonSpawnDelay, cottonSpawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            CancelInvoke();
        }
    }

    void SpawnEnemies()
    {
        Instantiate(enemyPrefab, GenerateRandomPos(), enemyPrefab.transform.rotation);
    }

    void SpawnPowerups()
    {
        Instantiate(powerupPrefab, GenerateRandomPos(), powerupPrefab.transform.rotation);
    }

    void SpawnCotton()
    {
        Instantiate(cottonPrefab, GenerateRandomPos(), powerupPrefab.transform.rotation);
    }

    Vector3 GenerateRandomPos()
    {
        int randomX = Random.Range(-xRange, xRange);
        return new Vector3(randomX, 1, 12);
    }
}
