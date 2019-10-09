using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    //Variables
    public float waitTime;
    public float points;
    public float maxHealth;
    public float health;

    //for Speed Skill
    public float speedDurationTime = 10f;
    public GameObject speedIncreaseActivation;

    //for double damage skill
    public float doubleDamageDurationTime = 10f;
    public GameObject doubleDamageActivation;

    //for forcefield skill
    public float forceFieldDurationTime = 10f;
    public GameObject forceFieldActivation;

    public GameObject bulletSpawnPoint;
    public GameObject playerObj;
    public GameObject bullet;
    public GameObject playerPosition;
    public GameObject forceField;
    public GameObject CooldownGO;
    public ParticleSystem deathEffect;
    public AudioSource gunSound;
    public TextMeshProUGUI score;
    public TextMeshProUGUI cooldown;

    //[SerializeField] private VirtualJoystick inputSource;


    List<GameObject> bulletList;

    private float hitDist = 0.0f;
    public float movementSpeed = 7f;

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
        var speedIncreaseCooldown = CooldownGO.GetComponent<Cooldown>().visualCooldownSpeedIncrease;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (speedIncreaseCooldown > 0 && speedIncreaseCooldown < 14)
            {
                cooldown.text = "Ability is on cooldown";
                StartCoroutine(Cooldown());
            }
            else
            {
                speedIncreaseActivation.GetComponent<SpeedDuration>().ButtonSpeedIncrease();
            }
        }


        //Double Damage Activation
        var doubleDamageCooldown = CooldownGO.GetComponent<Cooldown>().visualCooldownDoubleDamage;

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (doubleDamageCooldown > 0 && doubleDamageCooldown < 19)
            {
                cooldown.text = "Ability is on cooldown";
                StartCoroutine(Cooldown());
            }
            else
            {
                doubleDamageActivation.GetComponent<DoubleDamageDuration>().ButtonDoubleDamage();
            }
        }


        //ForceField Activation
        var forceFieldCooldown = CooldownGO.GetComponent<Cooldown>().visualCooldownForceField;

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (forceFieldCooldown > 0 && forceFieldCooldown < 29)
            {
                cooldown.text = "Ability is on cooldown";
                StartCoroutine(Cooldown());
            }
            else
            {
                forceFieldActivation.GetComponent<ForceFieldDuration>().ButtonForceField();
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
        FindObjectOfType<GameManager>().EndGame();
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(3f);
        cooldown.text = "";
    }
}
