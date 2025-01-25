using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb2d;
    BubblegunController bc;
    Health health;
    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponentInChildren<BubblegunController>();
        rb2d = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) & health.currentHealth >0)
        {
            bc.Fire();
        }
    }
}
