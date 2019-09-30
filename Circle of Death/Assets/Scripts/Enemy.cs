using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    //Variables
    public float health;
    public float pointsToGive;
    public float waitTime;
    public float enemySpeed;

    private float currentTime;
    private bool shot;
    [SerializeField]
    private float range = 10.0f;

    private GameObject player;
    private Transform bulletSpawned;
    private Transform gun;
    private Transform theEnemy;

    List<GameObject> bulletList;

    public GameObject bullet;
    public Transform bulletSpawnPoint;


    //private void Awake()
    //{
    //    theEnemy = this.transform;
    //    player = GameObject.FindGameObjectWithTag("Player");
    //}
    // Start is called before the first frame update
    public void Start()
    {
        player = GameObject.FindWithTag("Player");

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
        enemySpeed = 0.02f;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, enemySpeed);

        if (!bulletSpawnPoint)
        {
            gun = this.transform.GetChild(0);
            bulletSpawnPoint = gun.transform.GetChild(0);
        }
          

        if (health <= 0)
        {
            Die();
        }

        this.transform.LookAt(player.transform);

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
        Destroy(this.gameObject);

        player.GetComponent<Player>().points += pointsToGive;
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

    private float Distance()
    {
        return Vector3.Distance(theEnemy.position, player.transform.position);
    }

}
