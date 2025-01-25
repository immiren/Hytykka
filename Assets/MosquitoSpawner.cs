using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MosquitoSpawner : MonoBehaviour
{
    public GameObject Mosquito; // Refernce to Mosquito GameObject
    private Camera gameCamera; // Refrence to main camera   
    private float multiplier; // Multiplies the rate of Mosquito spawns
    private int mosquitoCount; // Keeps track of number of mosquitos
    // Start is called before the first frame update
    void Start()
    {
        mosquitoCount = 0; // Resets Count
        gameCamera = Camera.main; // Assigns camera
        Mosquito = GameObject.FindGameObjectWithTag("Mosquito");
        spawnMosquito();
    }

    // Update is called once per frame
    void Update()
    {
        float height = 2f * gameCamera.orthographicSize;
        float width = height * gameCamera.aspect;


    }

    void increaseMultiplier()
    {
        multiplier += 1f;
    }

    void spawnMosquito()
    {
        float height = 2f * gameCamera.orthographicSize;
        float width = height * gameCamera.aspect;
        Instantiate(Mosquito, new Vector2(x: gameCamera.transform.position.x + Random.Range(-width, width), y: gameCamera.transform.position.y + Random.Range(height, -height)), Quaternion.identity);
        mosquitoCount++;
    }
}
