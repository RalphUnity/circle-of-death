using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    //Variables
    public float waitTime;
    public float points;
    public float maxHealth;
    public float health;

    //for Speed Skill
    public float speedDurationTime = 10f;
    private bool counterActiveForSpeed = false;

    //for double damage skill
    public float doubleDamageDurationTime = 10f;
    private bool counterActiveForDoubleDamage = false;

    //for forcefield skill
    public float forceFieldDurationTime = 10f;
    private bool counterActiveForForceField = false;

    public GameObject bulletSpawnPoint;
    public GameObject playerObj;
    public GameObject bullet;
    public GameObject playerPosition;
    public GameObject forceField;
    public ParticleSystem deathEffect;
    public AudioSource gunSound;
    public Text score;

    //[SerializeField] private VirtualJoystick inputSource;


    List<GameObject> bulletList;

    private float hitDist = 0.0f;
    private float movementSpeed = 7f;

    [Header("Unity Stuff")]
    public Image healthBar;
    public Image healthBarMain;


    void Start()
    {
        health = maxHealth;

        //Object Pooling Initialization
        bulletList = new List<GameObject>();
        for (int i = 0; i < 12; i++)
        {
            GameObject objBullet = Instantiate(bullet);
            objBullet.SetActive(false);
            bulletList.Add(objBullet);
        }
    }


    // Update is called once per frame
    void Update()
    {
        score.text = points.ToString();

        //Player facing mouse
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (playerPlane.Raycast(ray, out hitDist))
        {
            Vector3 targetPoint = ray.GetPoint(hitDist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            playerObj.transform.rotation = Quaternion.Slerp(playerObj.transform.rotation, targetRotation, 7f * Time.deltaTime);
        }


        //Healthbar animation damage
        healthBar.fillAmount = maxHealth / health;
        healthBarMain.fillAmount = maxHealth / health;

        //Player Movement
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
        }


        //Shooting
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        //Speed Increase Activation
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            movementSpeed = 12f;
            counterActiveForSpeed = true;
        }
        if(speedDurationTime > 0 && counterActiveForSpeed == true)
        {
            speedDurationTime -= 1 * Time.deltaTime;
            if (speedDurationTime <= 0)
            {
                movementSpeed = 7f;
                speedDurationTime = 10f;
                counterActiveForSpeed = false;
            }
        }


        //Double Damage Activation
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            bullet.GetComponent<Bullet>().damage = 30f;
            counterActiveForDoubleDamage = true;
        }
        if(doubleDamageDurationTime > 0 && counterActiveForDoubleDamage == true)
        {
            doubleDamageDurationTime -= 1 * Time.deltaTime;
            if (doubleDamageDurationTime <= 0)
            {
                bullet.GetComponent<Bullet>().damage = 10f;
                doubleDamageDurationTime = 10f;
                counterActiveForDoubleDamage = false;
            }
        }


        //ForceField Activation
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            forceField.SetActive(true);
            counterActiveForForceField = true;
        }
        if(forceFieldDurationTime > 0 && counterActiveForForceField == true)
        {
            forceFieldDurationTime -= 1 * Time.deltaTime;
            if (forceFieldDurationTime <= 0)
            {
                forceField.SetActive(false);
                forceFieldDurationTime = 10f;
                counterActiveForForceField = false;
            }
        }

        //Player Death
        if (maxHealth <= 0)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Die();
        }
    }

    void Shoot()
    {
        gunSound.Play();
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

    public void Die()
    {
        //playerObj.SetActive(false);
        Destroy(gameObject);
    }

}
