using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrdererMove : MonoBehaviour
{
    public float playerMoveSpeed = 10f;
    private Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 movement = new Vector3(0f, 0f, playerMoveSpeed) * Time.deltaTime;
        
        rb.AddForce(movement);
    }
}
