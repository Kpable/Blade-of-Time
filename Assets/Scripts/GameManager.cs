using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    #region Public Variables

    [HeaderAttribute("Dependencies")]
    public TextMeshProUGUI gameTimerText;
    
    [HeaderAttribute("Game Settings")]
    public float gameDuration = 300f; // 5 Minutes

    #endregion

    #region Private Variables

    private Timer gameTimer;

    #endregion

    private void Awake()
    {
        gameTimer = GetComponent<Timer>();

        if (gameTimer != null)
        {
            //Debug.Log(name + ": Game Timer is present");
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
        //Debug.Log(name + ": Time is Up!");
        gameTimerText.text = "0";
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    private void HandleTimerSecondsChange(int seconds)
    {
        //Debug.Log(name + ": Seconds Changed: " + seconds);
        gameTimerText.text = seconds.ToString();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
