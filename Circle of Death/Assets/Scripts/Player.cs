using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    //Variables
    public float movementSpeed;
    public float waitTime;
    public float points;
    public float maxHealth;
    public float health;

    public GameObject bulletSpawnPoint;
    public GameObject playerObj;
    public GameObject bullet;
    public GameObject forceField;

    //[SerializeField] private VirtualJoystick inputSource;


    List<GameObject> bulletList;

    private float hitDist = 0.0f;
    private bool isShield = false;
    private Rigidbody rb;


    void Start()
    {
        health = maxHealth;

        rb = GetComponent<Rigidbody>();
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

        //Force Field Activation
        if (Input.GetKeyDown(KeyCode.R))
        {
            isShield = !isShield;
            forceField.SetActive(isShield);
        }

        //Player Death
        if (health <= 0)
        {
            Die();
        }
    }

    void Shoot()
    {

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
