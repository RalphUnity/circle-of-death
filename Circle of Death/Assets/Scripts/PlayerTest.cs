using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{

    public float waitTime;
    public float points;
    public float maxHealth;
    public float health;

    public GameObject bulletSpawnPoint;
    public GameObject playerObj;
    public GameObject bullet;
    public GameObject forceField;

    List<GameObject> bulletList;

    private Rigidbody rb;
    public Joystick joystick;

    private float movementSpeed = 7f;
    private float hitDist = 0.0f;
    private bool isShield = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

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

        //Character movement
        DetectInput();

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

    public void MoveForward()
    {
        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
    }

    public void MoveLeft()
    {
        transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
    }

    public void MoveBack()
    {
        transform.Translate(Vector3.back * movementSpeed * Time.deltaTime);
    }
    public void MoveRight()
    {
        transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
    }

    void DetectInput()
    {
        float x = joystick.Horizontal;
        float y = joystick.Vertical;

        if (x > 0.5)
        {
            MoveRight();
        }

        if (x < -0.5)
        {
            MoveLeft();
        }

        if (y > 0.5)
        {
            MoveForward();
        }

        if (y < -0.5)
        {
            MoveBack();
        }

        //if (x > 0 && y > 0)
        //{
        //    MoveRight();
        //    MoveForward();
        //}

        //if(x < 0 && y > 0)
        //{
        //    MoveLeft();
        //    MoveForward();
        //}

        //if (x < 0 && y < 0)
        //{
        //    MoveLeft();
        //    MoveBack();
        //}

        //if (x > 0 && y < 0)
        //{
        //    MoveRight();
        //    MoveBack();
        //}

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
