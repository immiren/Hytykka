using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb2d;
    BubblegunController bc;
    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponentInChildren<BubblegunController>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bc.Fire();
        }
    }
}
