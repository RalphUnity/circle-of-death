using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectObject : MonoBehaviour
{

    public float time;

    public bool shakeCamera;
    [Range(0f, 1f)]
    public float duration;
    [Range(0f, 1f)]
    public float magnitude;

    // Start is called before the first frame update
    void Start()
    {
        if(shakeCamera)
        StartCoroutine(FindObjectOfType<CameraShake>().Shake(duration, magnitude));
        Destroy(gameObject, time);
    }

    void Awake()
    {
        Debug.Log(this.gameObject);
    }

}
