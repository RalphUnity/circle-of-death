using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualJoystickTest : MonoBehaviour
{
    [SerializeField] private VirtualJoystick inputSource;
    private Rigidbody rigid;

    public float speed = 7f;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rigid.velocity = inputSource.Direction * speed;
    }
}
