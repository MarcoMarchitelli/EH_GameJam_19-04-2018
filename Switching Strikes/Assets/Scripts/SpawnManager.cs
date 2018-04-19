using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    [Header("Time Values")]
    public float TimeBetweenSpawn;
    public float TimeToAddMaxEnemies;
    float spawnTimer, speedTimer, maxEnemiesTimer, timer;

    [HideInInspector]
    public int enemiesAlive = 0;
    public float enemiesSpeed = 7f;

    public Transform[] spawnPoints;

    public Transform[] enemies;

    public static SpawnManager instance;

    int maxEnemies = 2;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        spawnTimer = TimeBetweenSpawn;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(TimeBetweenSpawn > 1f)
            TimeBetweenSpawn -= timer / TimeBetweenSpawn * 0.0001f;

        spawnTimer -= Time.deltaTime;
        enemiesSpeed += timer / enemiesSpeed * 0.0001f;

        if (spawnTimer <= 0)
        {
            if(enemiesAlive < maxEnemies)
            {
                Spawn();
                spawnTimer = TimeBetweenSpawn;
            }
        }

        maxEnemiesTimer -= Time.deltaTime;
        if (maxEnemiesTimer <= 0 && maxEnemies <= 8)
        {
            maxEnemies += 2;
            maxEnemiesTimer = TimeToAddMaxEnemies;
        }

    }

    void Spawn()
    {
        Vector3 spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
        Transform enemyToSpawn = Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPoint, Quaternion.identity);
        enemiesAlive++;
    }

}
