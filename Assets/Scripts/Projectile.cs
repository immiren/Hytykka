using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int speed = 6;
    Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        SetVelocity();
    }
    public void SetVelocity()
    {
        Vector3 shootDirection;
        shootDirection = Input.mousePosition;
        shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        shootDirection = shootDirection-transform.position;
        shootDirection.z = 0.0f;
        shootDirection = Vector3.Normalize(shootDirection);
        rb2d.velocity = new Vector2(shootDirection.x * speed, shootDirection.y * speed);
    }

    // Update is called once per frame
    private void OnEnable()
    {
        if(rb2d != null)
        {
            rb2d.velocity = transform.up * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb2d.velocity = Vector2.zero;
        //switch for mosquito death
        if (collision.gameObject.GetComponent<MosquitoScript>() != null)
        {
            collision.gameObject.GetComponent<MosquitoScript>().TakeDamage(); //lol älä käytä viel
        }
        Destroy(this.gameObject);
    }
}
