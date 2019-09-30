using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    //Variables
    public float health;
    public float pointsToGive;
    public float waitTime;

    private float currentTime;
    private bool shot;

    private GameObject player;
    private Transform bulletSpawned;
    private Transform gun;

    public GameObject bullet;
    public Transform bulletSpawnPoint;


    // Start is called before the first frame update
    public void Start()
    {
        player = GameObject.FindWithTag("Player");

        gun = this.transform.GetChild(0);
        bulletSpawnPoint = gun.transform.GetChild(0);

    }

    // Update is called once per frame
    public void Update()
    {
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
            Shoot();

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

        bulletSpawned = Instantiate(bullet.transform, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
        bulletSpawned.rotation = this.transform.rotation;
    }

}
