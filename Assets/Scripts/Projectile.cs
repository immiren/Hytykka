using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 6f; // Speed of the projectile
    public float lifespan = 3f; // Lifespan of the projectile
    public bool isMultiHit = false; // Whether the projectile can hit multiple enemies
    public float velocityDecayRate = 0.95f; // Factor by which velocity decreases per frame
    public float randomSpread = 0.1f; // Degree of randomness added to firing direction

    private Rigidbody2D rb2d;
    private HashSet<GameObject> hitEnemies = new HashSet<GameObject>(); // Track enemies already hit

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        SetVelocity();

        // Destroy the projectile after its lifespan
        Destroy(this.gameObject, lifespan);

        // Start gradually reducing velocity
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
        // Check if the projectile hits an enemy (mosquito)
        if (collision.gameObject.GetComponent<MosquitoScript>() != null)
        {
            if (isMultiHit)
            {
                // If it's a multi-hit bubble, ensure we don't hit the same enemy twice
                if (!hitEnemies.Contains(collision.gameObject))
                {
                    hitEnemies.Add(collision.gameObject);
                    collision.gameObject.GetComponent<MosquitoScript>().TakeDamage();
                }
            }
            else
            {
                // Single-hit behavior
                collision.gameObject.GetComponent<MosquitoScript>().TakeDamage();
                Destroy(this.gameObject); // Destroy bubble after hitting once
            }
        }

        // Destroy the projectile upon collision if it's not multi-hit
        if (!isMultiHit)
        {
            Destroy(this.gameObject);
        }
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
