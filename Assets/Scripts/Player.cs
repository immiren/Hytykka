using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb2d;
    BubblegunController bc;
    Health health;
    private bool chargingBubble = false; // Whether the player is currently charging a bubble
    private float chargeTime = 0f; // Time the mouse button has been held down

    void Start()
    {
        bc = GetComponentInChildren<BubblegunController>();
        rb2d = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
    }

    void Update()
    {
        // Start charging when the mouse button is pressed
        if (Input.GetKeyDown(KeyCode.Mouse0) && health.currentHealth > 0)
        {
            chargingBubble = true;
            chargeTime = 0f; // Reset charge time when starting to charge
        }

        // Increment charge time while the mouse button is held down
        if (chargingBubble)
        {
            chargeTime += Time.deltaTime;
        }

        // Release and fire the bubble when the mouse button is released
        if (Input.GetKeyUp(KeyCode.Mouse0) && chargingBubble && health.currentHealth > 0)
        {
            bc.Fire(chargeTime); // Pass the charge time to the BubblegunController
            chargingBubble = false; // Reset charging state
            chargeTime = 0f; // Reset charge time
        }
    }
}
