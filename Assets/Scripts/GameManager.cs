using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour {

    public TextMeshProUGUI gameTimerText;
    
    private Timer gameTimer;

    public float gameDuration = 300f; // 5 Minutes

    private void Awake()
    {
        gameTimer = GetComponent<Timer>();

        if (gameTimer != null)
        {
            Debug.Log(name + ": Game Timer is present");
            gameTimer.OnSecondsChanged += HandleTimerSecondsChange;
            gameTimer.OnTimeUp += HandleTimerEnd;

            gameTimer.Set(gameDuration, true);
        }
        else
        {
            Debug.LogError(name + ": Game Timer is null");
        }

        if (gameTimerText) gameTimerText.text = gameDuration.ToString();
    }

    private void HandleTimerEnd()
    {
        Debug.Log(name + ": Time is Up!");
        gameTimerText.text = "0";

    }

    private void HandleTimerSecondsChange(int seconds)
    {
        Debug.Log(name + ": Seconds Changed: " + seconds);
        gameTimerText.text = seconds.ToString();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
