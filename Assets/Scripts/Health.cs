using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth = 3;
    public int currentHealth;
    Rigidbody2D rb2d;
    [SerializeField] float invulnerableTimeLength = 1f;
    float timeSinceHit = 2f;
    bool isInvulnerable = false;
    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        SetHealth();
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    void Update(){
        timeSinceHit += Time.deltaTime;

        // Set Invulnerability
        if(timeSinceHit <= invulnerableTimeLength){ // true if invulnerable
            isInvulnerable = true;
        }else{
            isInvulnerable = false; 
        }

        // Set sprite color
        if(isInvulnerable){
            sprite.color = new Color (1, 0.4756839f, 0.4182389f, 1); 
        }else{
            sprite.color = Color.white;   
        }
    }

    public void SetHealth()
    {
         //change maxHealth for enemies here
        currentHealth = maxHealth;

    }

    public void TakeDamage()
    {
        //Debug.Log("Health: " + currentHealth);
        if(!isInvulnerable){
            currentHealth--;
            Debug.Log("timeSinceHit: " + timeSinceHit);
            timeSinceHit = 0f;
            if(currentHealth <= 0)
            {
                currentHealth = 0;
                Death();
            }
            isInvulnerable = true;
        }
        else{
            Debug.Log("Invulnerable");
        }
    }
    
    void Death(){
            rb2d.simulated = false; 
            Debug.Log("Dead :P");
    }
}
