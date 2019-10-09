using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDamageDuration : MonoBehaviour
{

    public float doubleDamageDurationTime = 10f;
    private bool counterActiveForDoubleDamage = false;
    public GameObject bullet;


    // Update is called once per frame
    void Update()
    {
        ButtonDoubleDamage();
    }

    public void ButtonDoubleDamage()
    {
        gameObject.SetActive(true);
        bullet.GetComponent<Bullet>().damage = 30f;
        counterActiveForDoubleDamage = true;
        if (doubleDamageDurationTime > 0 && counterActiveForDoubleDamage == true)
        {
            doubleDamageDurationTime -= 1 * Time.deltaTime;
            if (doubleDamageDurationTime <= 0)
            {
                gameObject.SetActive(false);
                bullet.GetComponent<Bullet>().damage = 10f;
                doubleDamageDurationTime = 10f;
                counterActiveForDoubleDamage = false;
            }
        }

    }
}
