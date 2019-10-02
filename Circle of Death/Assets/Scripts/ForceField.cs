using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    public bool forceField = false;

    public void ButtonForceField()
    {
        forceField = !forceField;
        gameObject.SetActive(forceField);
    }
}
