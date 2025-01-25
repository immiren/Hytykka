using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosquitoScript : MonoBehaviour
{
    private Transform target; // Reference to the target's Transform
    public float speed = 2f; // Movement speed
    private float minDistance = 1f; // Minimum distance to stop moving
    private float range; // Distance between the mosquito and the target
    private bool isFlyingAway = false; // Whether the mosquito is flying away
    public float flyAwayDuration = 3f; // How long the mosquito flies away
    public float flyAwayDistance = 5f; // Distance to fly away

    void Start()
    {
        // Find the GameObject with the tag "Player" and set it as the target
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            target = player.transform;
        }
        else
        {
            Debug.LogError("No GameObject with the tag 'Player' found in the scene!");
        }
    }

    void Update()
    {
        // If the target is not set or the mosquito is flying away, do nothing
        if (target == null || isFlyingAway) return;

        // Calculate the distance between the mosquito and the target
        range = Vector2.Distance(transform.position, target.position);

        if (range > minDistance)
        {
            // Move towards the target
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the mosquito hits the player
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Mosquito hit the player!");
            StartCoroutine(FlyAway());
        }
    }

    private IEnumerator FlyAway()
    {
        isFlyingAway = true;

        // Fly in a random direction
        Vector2 randomDirection = Random.insideUnitCircle.normalized * flyAwayDistance;
        Vector3 targetPosition = transform.position + (Vector3)randomDirection;

        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        while (elapsedTime < flyAwayDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / flyAwayDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // After flying away, re-enable chasing
        isFlyingAway = false;
    }

}
