using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    //Variables
    public float speed;
    public float maxDistance;
    public float damage;

    private GameObject triggeringEnemy;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        maxDistance += 1 * Time.deltaTime;

    }

    private void OnEnable()
    {

        Invoke("hideBullet", 2.0f);
    }

    void hideBullet()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }


    public void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            gameObject.SetActive(false);
        }

        if (collision.transform.tag == "Player")
        {
            player.GetComponent<Player>().maxHealth -= 20;
            player.GetComponent<PlayerTest>().maxHealth -= 20;
            gameObject.SetActive(false);
        }

        if (collision.transform.tag == "ForceField")
        {
            gameObject.SetActive(false);
        }

    }
}
