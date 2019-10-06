using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Variables
    public float speed;
    public float maxDistance;
    public float damage;

    private GameObject triggeringEnemy;


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


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.tag == "Enemy")
        {
            triggeringEnemy = other.gameObject;

            // triggeringEnemy.GetComponent<Enemy>().startHealth -= damage;
            Enemy enemy = other.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }

        if (other.gameObject.transform.tag == "Boss")
        {
            triggeringEnemy = other.gameObject;
            EnemyBoss enemyBoss = other.transform.GetComponent<EnemyBoss>();
            if (enemyBoss != null)
            {
                enemyBoss.TakeDamage(damage);
            }
        }

        gameObject.SetActive(false);
    }

}
