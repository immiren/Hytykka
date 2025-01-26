using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float defaultSpeed;
    private Rigidbody2D rb2d;
    public Animation walkAnimation;

    // Start is called before the first frame update
    void Start()
    {
        defaultSpeed = speed;
        rb2d = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            speed = speed * 0.5f;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            speed = defaultSpeed;
        }
        rb2d.velocity = new Vector3(moveHorizontal* speed, moveVertical*speed);
        if (transform.hasChanged)
        {
            walkAnimation.Play();
        }
        else
        {
            walkAnimation.Stop();
        }
    }
}
