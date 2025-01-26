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
    private GameObject bubblePreview;
    [SerializeField] private GameObject bubblePrefab;
    [SerializeField] private Color halfOpaqueColor = new Color(176f, 223f, 255f, 0.5f);
    public float mediumChargeTime = 0.75f;
    public float largeChargetime = 1.5f;

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

            if (bubblePrefab != null)
            {
                bubblePreview = Instantiate(bubblePrefab, transform.position, Quaternion.identity);
                bubblePreview.GetComponentInChildren<SpriteRenderer>().color = halfOpaqueColor;
            }
        }

        // Increment charge time while the mouse button is held down
        if (chargingBubble)
        {
            chargeTime += Time.deltaTime;

            if (bubblePreview != null)
            {
                float bubbleScale = GetBubbleScale(chargeTime);
                bubblePreview.transform.localScale = new Vector3 (bubbleScale, bubbleScale, bubbleScale);

                // Position the bubble slightly in the direction of the mouse cursor
                Vector3 mousePoistion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 direction = (mousePoistion - transform.position).normalized;
                bubblePreview.transform.position = transform.position + direction * 1f;
            }
        }

        // Release and fire the bubble when the mouse button is released
        if (Input.GetKeyUp(KeyCode.Mouse0) && chargingBubble && health.currentHealth > 0)
        {
            bc.Fire(chargeTime); // Pass the charge time to the BubblegunController
            chargingBubble = false; // Reset charging state
            chargeTime = 0f; // Reset charge time

            if (bubblePreview != null)
            {
                Destroy(bubblePreview );
            }
        }
    }
    private float GetBubbleScale(float chargeTime)
    {
        if (chargeTime < mediumChargeTime) return 0.5f; // Small bubble size
        if (chargeTime < largeChargetime) return 1.0f; // Medium bubble size
        else return 2f;                     // Large bubble size
    }
}
