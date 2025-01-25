using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth = 3;
    public int currentHealth;
    Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        SetHealth();
        rb2d = GetComponent<Rigidbody2D>(); 
    }

    public void SetHealth()
    {
         //change maxHealth for enemies here
        currentHealth = maxHealth;

    }

    public void TakeDamage()
    {
        currentHealth--;
        Debug.Log("Health: " + currentHealth);
        if(currentHealth <= 0)
        {
            Death();
        }
    }
    
    void Death(){
            rb2d.simulated = false; 
            Debug.Log("Dead :P");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {   
            //enable 4 testing
            //TakeDamage();
        }
    }
}
