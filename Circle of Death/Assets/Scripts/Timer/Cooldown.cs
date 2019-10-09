using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cooldown : MonoBehaviour
{

    [Header("Speed Increase Skill")]
    public Image speedIncreaseImageCooldown;
    public TextMeshProUGUI speedIncreaseTextMesh;
    public float speedIncreaseCooldown;
    public float visualCooldownSpeedIncrease;
    bool isVisualCooldownSpeedIncrease;
    bool speedIncreaseisCooldown;

    [Header("Double Damage Skill")]
    public Image doubleDamageImageCooldown;
    public TextMeshProUGUI doubleDamageTextMesh;
    public float doubleDamageCooldown;
    public float visualCooldownDoubleDamage;
    bool isVisualCooldownDoubleDamage;
    bool doubleDamageisCooldown;

    [Header("Force Field Skill")]
    public Image forceFieldImageCooldown;
    public TextMeshProUGUI forceFieldTextMesh;
    public float forceFieldCooldown;
    public float visualCooldownForceField;
    bool isVisualCooldownForceField = false;
    bool forceFieldisCooldown = false;

    // Update is called once per frame
    void Update()
    {

        SpeedIncreaseCooldown();
        DoubleDamageCooldown();
        ForceFieldCooldown();  
    }

    public void SpeedIncreaseCooldown()
    {
        //SpeedIncrease cooldown
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            speedIncreaseisCooldown = true;
        }
        if (speedIncreaseisCooldown)
        {
            speedIncreaseImageCooldown.fillAmount += 1 / speedIncreaseCooldown * Time.deltaTime;

            if (speedIncreaseImageCooldown.fillAmount >= 1)
            {
                speedIncreaseImageCooldown.fillAmount = 0;
                speedIncreaseisCooldown = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isVisualCooldownSpeedIncrease = true;
        }
        if (isVisualCooldownSpeedIncrease)
        {
            visualCooldownSpeedIncrease -= 1 * Time.deltaTime;
            speedIncreaseTextMesh.text = visualCooldownSpeedIncrease.ToString("0");
            if (visualCooldownSpeedIncrease <= 0)
            {
                visualCooldownSpeedIncrease = 15f;
                isVisualCooldownSpeedIncrease = false;
                speedIncreaseTextMesh.text = "";
            }
        }
    }

    public void DoubleDamageCooldown()
    {
        //DoubleDamage cooldown
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            doubleDamageisCooldown = true;
        }
        if (doubleDamageisCooldown)
        {
            doubleDamageImageCooldown.fillAmount += 1 / doubleDamageCooldown * Time.deltaTime;

            if (doubleDamageImageCooldown.fillAmount >= 1)
            {
                doubleDamageImageCooldown.fillAmount = 0;
                doubleDamageisCooldown = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            isVisualCooldownDoubleDamage = true;
        }
        if (isVisualCooldownDoubleDamage)
        {
            visualCooldownDoubleDamage -= 1 * Time.deltaTime;
            doubleDamageTextMesh.text = visualCooldownDoubleDamage.ToString("0");
            if (visualCooldownDoubleDamage <= 0)
            {
                visualCooldownDoubleDamage = 20f;
                isVisualCooldownDoubleDamage = false;
                doubleDamageTextMesh.text = "";
            }
        }
    }

    public void ForceFieldCooldown()
    {
        //forcefield cooldown
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            forceFieldisCooldown = true;
        }
        if (forceFieldisCooldown)
        {
            forceFieldImageCooldown.fillAmount += 1 / forceFieldCooldown * Time.deltaTime;

            if (forceFieldImageCooldown.fillAmount >= 1)
            {
                forceFieldImageCooldown.fillAmount = 0;
                forceFieldisCooldown = false;
            }
        }


        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            isVisualCooldownForceField = true;
        }
        if (isVisualCooldownForceField)
        {
            visualCooldownForceField -= 1 * Time.deltaTime;
            forceFieldTextMesh.text = visualCooldownForceField.ToString("0");
            if (visualCooldownForceField <= 0)
            {
                visualCooldownForceField = 30f;
                isVisualCooldownForceField = false;
                forceFieldTextMesh.text = "";
            }
        }
    }
}
