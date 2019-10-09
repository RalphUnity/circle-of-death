using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBillboard : MonoBehaviour
{

    public CameraMain cameraBillboard;


    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + cameraBillboard.transform.rotation * Vector3.back, cameraBillboard.transform.rotation * Vector3.up);
    }
}
