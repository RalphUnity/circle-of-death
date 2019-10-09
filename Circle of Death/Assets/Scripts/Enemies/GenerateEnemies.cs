using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour
{
    public PlayerTest playerHealth;
    public GameObject enemies;
    public float spawnTime = 3f;
    public int maxEnemy = 10;
    public int enemyCount;
    public Transform[] spawnPoints;
    public EnemyBoss enemyBoss;

    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    public void Spawn()
    {

        if (playerHealth.maxHealth <= 0f)
        {
            return;
        }

        if (enemyBoss.startHealth <= 0)
        {
            return;
        }

        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        if (enemyCount >= maxEnemy)
            return;

        Instantiate(enemies, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        enemyCount++;
    }

}
