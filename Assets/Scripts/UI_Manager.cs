using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public TextMeshProUGUI healthTextbox;
    public TextMeshProUGUI scoreTextbox;
    public TextMeshProUGUI timerTextbox;
    private float health = 100;
    private float score = 0;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        timer = Mathf.FloorToInt(timer);
        healthTextbox.text = "Health: " + health;
        scoreTextbox.text = "Score: " + score;
        timerTextbox.text = "Time: " + timer;
    }
}
