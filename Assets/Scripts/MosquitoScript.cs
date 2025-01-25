using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosquitoScript : MonoBehaviour
{
    private Transform target; // Reference to the target's Transform
    public float speed = 2f; // Movement speed
    public float minDistance = 1f; // Minimum distance to stop moving
    private float range; // Distance between the mosquito and the target
    private bool isFlyingAway = false; // Whether the mosquito is flying away
    public float flyAwayDuration = 3f; // How long the mosquito flies away
    public float flyAwayDistance = 5f; // Distance to fly away
    private float jitterFrequency; // Frequency of the jitter updates
    private float jitterIntensity; // Intensity of the erratic movement
    private Vector3 jitterOffset; // Stores the current jitter offset

    void Start()

    {
        // Sets Mosquito jitter values
        jitterFrequency = Random.Range(0.1f, 1f);
        jitterIntensity = Random.Range(0.1f, 1f);
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

        // Start applying random jitter
        StartCoroutine(UpdateJitter());
    }

    void Update()
    {
        // If the target is not set or the mosquito is flying away, do nothing
        if (target == null || isFlyingAway) return;

        // Calculate the distance between the mosquito and the target
        range = Vector2.Distance(transform.position, target.position);

        if (range > minDistance)
        {
            // Calculate the direction to the target
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            // Add jitter to the movement
            Vector3 erraticMovement = directionToTarget + jitterOffset;

            // Move towards the player with jitter
            transform.position += erraticMovement * speed * Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // Check if the mosquito hits the player
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit player");
            col.gameObject.GetComponent<Health>().TakeDamage();
            StartCoroutine(FlyAway());
        }
        else if (col.gameObject.CompareTag("Bubble"))
        {
            Debug.Log("Mosquito hit");
            Destroy(this.gameObject);
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

    private IEnumerator UpdateJitter()
    {
        while (true)
        {
            // Generate a random jitter offset
            jitterOffset = new Vector3(
                Random.Range(-jitterIntensity, jitterIntensity),
                Random.Range(-jitterIntensity, jitterIntensity),
                0
            );

            // Wait for the next jitter update
            yield return new WaitForSeconds(jitterFrequency);
        }
    }

    public void TakeDamage()
    {
        Debug.Log("Mosquito killed!");
        Destroy(this.gameObject);
    }
}
