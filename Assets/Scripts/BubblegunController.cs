using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblegunController : MonoBehaviour
{
    [SerializeField] GameObject bubblePrefab;
    [SerializeField] int baseSpeed = 10; // Base speed for medium bubbles
    [SerializeField] float baseLifespan = 3f; // Base lifespan for medium bubbles

    public void Fire(float chargeTime)
    {
        GameObject bubbleInstance = Instantiate(bubblePrefab, transform.position, transform.rotation);

        Projectile bubbleProjectile = bubbleInstance.GetComponent<Projectile>();

        if (chargeTime < 0.5f) // Small bubble
        {
            bubbleInstance.transform.localScale = Vector3.one * 0.5f; // Half size
            bubbleProjectile.speed = baseSpeed * 0.5f; // Half speed
            bubbleProjectile.lifespan = 1f; // Short lifespan
            bubbleProjectile.isMultiHit = false; // Single hit
        }
        else if (chargeTime < 1.0f) // Medium bubble
        {
            bubbleInstance.transform.localScale = Vector3.one; // Normal size
            bubbleProjectile.speed = baseSpeed; // Normal speed
            bubbleProjectile.lifespan = baseLifespan; // Normal lifespan
            bubbleProjectile.isMultiHit = false; // Single hit
        }
        else // Large bubble
        {
            bubbleInstance.transform.localScale = Vector3.one * 2f; // Double size
            bubbleProjectile.speed = baseSpeed * 0.5f; // Same speed as small bubble
            bubbleProjectile.lifespan = baseLifespan; // Same lifespan as medium bubble
            bubbleProjectile.isMultiHit = true; // Can hit multiple enemies
        }
    }
}
