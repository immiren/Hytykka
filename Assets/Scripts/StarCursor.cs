using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCursor : MonoBehaviour
{
    float timer = 0.7f;//between 0.7 and 1.2
    public float timerStep = 0.05f;
    GameObject star;
    bool isStarScalingUp;
    public float maxSize;
    public float minSize;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //position
        Vector3 mouseLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseLocation.z = 2;
        transform.position = mouseLocation;

        //scale
        if(isStarScalingUp){timer += timerStep;}
        else{timer -= timerStep;}

        if(timer <=minSize){isStarScalingUp = true;}
        else if(timer >= maxSize){isStarScalingUp = false;}
        
        transform.localScale = new Vector3(timer, timer, timer);
    }
}
