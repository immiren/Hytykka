using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 6f; // Initial speed of the projectile
    public float velocityDecayRate = 0.95f; // Factor by which velocity decreases per frame
    public float randomSpread = 0.1f; // Degree of randomness added to firing direction
    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        SetVelocity();
        Destroy(this.gameObject, 3f); // Destroy the projectile after 3 seconds
        StartCoroutine(GraduallyReduceVelocity());
    }

    public void SetVelocity()
    {
        Vector3 shootDirection;
        shootDirection = Input.mousePosition;
        shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        shootDirection = shootDirection - transform.position;
        shootDirection.z = 0.0f;

        // Normalize the direction and add random spread
        shootDirection = Vector3.Normalize(shootDirection);
        shootDirection += new Vector3(
            Random.Range(-randomSpread, randomSpread),
            Random.Range(-randomSpread, randomSpread),
            0
        );

        rb2d.velocity = new Vector2(shootDirection.x * speed, shootDirection.y * speed);
    }

    private void OnEnable()
    {
        if (rb2d != null)
        {
            rb2d.velocity = transform.up * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb2d.velocity = Vector2.zero;

        // Check for mosquito collision
        if (collision.gameObject.GetComponent<MosquitoScript>() != null)
        {
            collision.gameObject.GetComponent<MosquitoScript>().TakeDamage();
        }

        // Destroy the projectile upon collision
        Destroy(this.gameObject);
    }

    private IEnumerator GraduallyReduceVelocity()
    {
        while (rb2d.velocity.magnitude > 0.1f) // Stop reducing when velocity is very low
        {
            rb2d.velocity *= velocityDecayRate; // Reduce velocity by decay rate
            yield return new WaitForSeconds(0.1f); // Adjust decay frequency if needed
        }

        // Once the projectile slows down significantly, destroy it
        Destroy(this.gameObject);
    }
}
