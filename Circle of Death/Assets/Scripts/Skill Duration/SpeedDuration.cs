using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeedDuration : MonoBehaviour
{

    public float speedDurationTime = 10f;
    private bool counterActiveForSpeed = false;
    public GameObject player;
    public GameObject cooldownGO;

    // Update is called once per frame
    void Update()
    {
        ButtonSpeedIncrease();
    }

    public void ButtonSpeedIncrease()
    {
        gameObject.SetActive(true);
        player.GetComponent<Player>().movementSpeed = 12f;
        counterActiveForSpeed = true;
        if (speedDurationTime > 0 && counterActiveForSpeed == true)
        {
            speedDurationTime -= 1 * Time.deltaTime;
            if (speedDurationTime <= 0)
            {
                gameObject.SetActive(false);
                player.GetComponent<Player>().movementSpeed = 7f;
                speedDurationTime = 10f;
                counterActiveForSpeed = false;
            }
        } 

    }

}
