using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldDuration : MonoBehaviour
{

    public float forceFieldDurationTime = 10f;
    private bool counterActiveForForceField = false;
    public GameObject forcefield;

    // Update is called once per frame
    void Update()
    {
        ButtonForceField();
    }

    public void ButtonForceField()
    {
        gameObject.SetActive(true);
        forcefield.SetActive(true);
        counterActiveForForceField = true;
        if (forceFieldDurationTime > 0 && counterActiveForForceField == true)
        {
            forceFieldDurationTime -= 1 * Time.deltaTime;
            if (forceFieldDurationTime <= 0)
            {
                gameObject.SetActive(false);
                forcefield.SetActive(false);
                forceFieldDurationTime = 10f;
                counterActiveForForceField = false;
            }
        }

    }
}
