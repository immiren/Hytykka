using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private float spawnMultiplier = 1.0f;
    public GameObject Mosquito; // Reference to the Mosquito GameObject
    private Camera gameCamera; // Reference to the main camera
    private int mosquitoCount; // Keeps track of the number of mosquitos
    private float secondsBetweenSpawn = 10.0f;
    private float defaultSpawnRate = 5.0f;
    private float elapsedTime = 0.0f;

    void Start()
    {
        mosquitoCount = 0; // Reset count
        gameCamera = Camera.main; // Assign the main camera
        Mosquito = GameObject.FindGameObjectWithTag("Mosquito"); // Find the mosquito prefab by tag
        spawnMosquito();
    }

    void Update()
    {

        elapsedTime += Time.deltaTime;
        if (elapsedTime > secondsBetweenSpawn)
        {
            float newTime = Random.Range(Mathf.Max(defaultSpawnRate, secondsBetweenSpawn - spawnMultiplier), Mathf.Min(defaultSpawnRate, secondsBetweenSpawn + spawnMultiplier));
            secondsBetweenSpawn = newTime;
            elapsedTime = 0;
            Debug.Log(true);
            spawnMosquito();
        }
    }

    void spawnMosquito()
    {
        float height = 2f * gameCamera.orthographicSize; // Camera height in world units
        float width = height * gameCamera.aspect; // Camera width in world units

        // Determine spawn position outside the viewport
        Vector2 spawnPosition = GetRandomSpawnPosition(width, height);

        // Instantiate the mosquito at the spawn position
        Instantiate(Mosquito, spawnPosition, Quaternion.identity);
        mosquitoCount++;
    }

    Vector2 GetRandomSpawnPosition(float width, float height)
    {
        // Randomly choose one of four sides of the viewport to spawn the mosquito
        int side = Random.Range(0, 4);

        Vector2 spawnPosition = Vector2.zero;

        switch (side)
        {
            case 0: // Left side (just outside the left viewport edge)
                spawnPosition = new Vector2(gameCamera.transform.position.x - width / 2 - 1f,
                                            Random.Range(gameCamera.transform.position.y - height / 2, gameCamera.transform.position.y + height / 2));
                break;

            case 1: // Right side (just outside the right viewport edge)
                spawnPosition = new Vector2(gameCamera.transform.position.x + width / 2 + 1f,
                                            Random.Range(gameCamera.transform.position.y - height / 2, gameCamera.transform.position.y + height / 2));
                break;

            case 2: // Bottom side (just outside the bottom viewport edge)
                spawnPosition = new Vector2(Random.Range(gameCamera.transform.position.x - width / 2, gameCamera.transform.position.x + width / 2),
                                            gameCamera.transform.position.y - height / 2 - 1f);
                break;

            case 3: // Top side (just outside the top viewport edge)
                spawnPosition = new Vector2(Random.Range(gameCamera.transform.position.x - width / 2, gameCamera.transform.position.x + width / 2),
                                            gameCamera.transform.position.y + height / 2 + 1f);
                break;
        }

        return spawnPosition;
    }
}

