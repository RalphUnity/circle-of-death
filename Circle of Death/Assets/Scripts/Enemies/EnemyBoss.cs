using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyBoss : MonoBehaviour
{
    //Variables
    public float startHealth = 100;

    public float pointsToGive;
    public float waitTime;
    public float enemySpeed;
    public int speed = 5;

    private float currentTime;
    private bool shot;
    public float health;

    private GameObject player;
    [HideInInspector]
    public GameObject playerPoints;
    private Transform gun;

    List<GameObject> bulletList;

    public GameObject bullet;
    [HideInInspector]
    public Transform bulletSpawnPoint;
    public ParticleSystem deathEffect;
    public GameObject floatingTextPrefab;
    public GameObject bossHealthBar;



    [Header("Unity Stuff")]
    public Image healthBar;
    public Image healthBarMain;

    // Start is called before the first frame update
    public void Start()
    {
        player = GameObject.FindWithTag("PlayerPosition");
        playerPoints = GameObject.FindWithTag("Player");
        health = startHealth;

        //Object Pooling Initialization
        bulletList = new List<GameObject>();
        for (int i = 0; i < 4; i++)
        {
            GameObject objBullet = Instantiate(bullet);
            objBullet.SetActive(false);
            bulletList.Add(objBullet);
        }

        gun = this.transform.GetChild(0);
        bulletSpawnPoint = gun.transform.GetChild(0);


    }

    // Update is called once per frame
    public void Update()
    {
        //enemySpeed = 0.02f;
       transform.position = Vector3.MoveTowards(transform.position, player.transform.position, enemySpeed);

        healthBar.fillAmount = startHealth / health;
        healthBarMain.fillAmount = startHealth / health;

        if (!bulletSpawnPoint)
        {
            gun = this.transform.GetChild(0);
            bulletSpawnPoint = gun.transform.GetChild(0);
        }


        transform.LookAt(player.transform);

        if (currentTime == 0)
        {
            Shoot();
        }


        if (shot && currentTime < waitTime)
            currentTime += 1 * Time.deltaTime;

        if (currentTime >= waitTime)
            currentTime = 0;

    }

    public void Die()
    {
        
        gameObject.SetActive(false);
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        playerPoints.GetComponent<Player>().points += pointsToGive;
    }

    public void Shoot()
    {
        shot = true;

        //Shoot objects using object pooling
        for (int i = 0; i < bulletList.Count; i++)
        {
            if (!bulletList[i].activeInHierarchy)
            {
                bulletList[i].transform.position = bulletSpawnPoint.transform.position;
                bulletList[i].transform.rotation = bulletSpawnPoint.transform.rotation;
                bulletList[i].SetActive(true);
                break;
            }
        }

    }

    public void TakeDamage(float amount)
    {
        startHealth -= amount;

        if (startHealth <= 0)
        {
            bossHealthBar.SetActive(false);
            Die();
        }

        if (floatingTextPrefab && startHealth > 0)
        {
            var floatTextGO = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, transform);
            floatTextGO.GetComponent<TextMeshPro>().text = startHealth.ToString();
        }
    }

}
