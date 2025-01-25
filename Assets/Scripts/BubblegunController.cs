using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblegunController : MonoBehaviour
{
    [SerializeField] GameObject bubble;
    GameObject bubbleInstance;
    Projectile bubbleProjectile;
    [SerializeField] int speed = 10; //changeable
    private float nextShot = 0.15f; 
    [SerializeField] private float fireDelay = 0.1f; //changeable

    public void Fire()
    {
        if (Time.time > nextShot){
            bubbleInstance = Instantiate(bubble, transform.position, transform.rotation) as GameObject; // vaiha rotation
            bubbleProjectile = bubble.GetComponent<Projectile>();
            bubbleProjectile.speed = speed;

            nextShot = Time.time + fireDelay;
        }        
    }
}
