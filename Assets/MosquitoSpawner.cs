using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UIElements.UxmlAttributeDescription;

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

        
        if (Input.GetKey("space"))
            spawnMosquito();
    }

    void increaseMultiplier()
    {
        multiplier += 1f;
    }

    void spawnMosquito()
    {
        float height = 2f * gameCamera.orthographicSize;
        float width = height * gameCamera.aspect;
        Vector2 vPos = gameCamera.ViewportToWorldPoint(new Vector2(1.1f, 0.5f));
        Vector2 vPos2 = new Vector2(x: gameCamera.transform.position.x + Random.Range(-width, width), y: gameCamera.transform.position.y + Random.Range(height, -height));
        Instantiate(Mosquito, vPos2, Quaternion.identity);
        mosquitoCount++;
    }
}

