using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour
{
    //public GameObject theEnemy;
    //public int xPos;
    //public int zPos;
    //public int enemyCount;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    StartCoroutine(EnemyDrop());
    //}

    //IEnumerator EnemyDrop()
    //{
    //    while(enemyCount < 10)
    //    {
    //        xPos = Random.Range(1, 50);
    //        zPos = Random.Range(1, 31);
    //        Instantiate(theEnemy, new Vector3(xPos, 1.39f, zPos), Quaternion.identity);
    //        yield return new WaitForSeconds(0.1f);
    //        enemyCount += 1;
    //    }
    //}

    public PlayerTest playerHealth;
    public GameObject enemies;
    public float spawnTime = 3f;
    public int maxEnemy = 10;
    public int enemyCount;
    public Transform[] spawnPoints;
    public GameObject managerGameObject;

    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    public void Spawn()
    {

        if(playerHealth.maxHealth <= 0f)
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
