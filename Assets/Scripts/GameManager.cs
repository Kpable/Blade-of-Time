using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct PlayerAttributes
{
    public float maxHealth;
    public float damage;
    public float attackSpeed;
    public float weaponSize;
    public float walkSpeed;
    public int totalExp;
}

public class GameManager : MonoBehaviour {
    
    public static GameManager Instance;

    #region Public Variables

    [HeaderAttribute("Game Settings")]
    public float gameDuration = 300f; // 5 Minutes

    [HeaderAttribute("Player Stats")]
    public PlayerAttributes stats;

    #endregion

    #region Private Variables


    private Timer gameTimer;
    private TextMeshProUGUI gameTimerText;

    private GameObject playerObj;
    private PlayerStats playerStats;
    private PlayerAttack playerAttack;
    private PlayerMovement playerMovement;

    #endregion

    private void Awake()
    {
        // Make a singleton
        if (GameManager.Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(gameObject);

        gameTimer = GetComponent<Timer>();

        if (gameTimer != null)
        {
            Debug.Log(name + ": Game Timer is present");
            gameTimer.OnSecondsChanged += HandleTimerSecondsChange;
            gameTimer.OnTimeUp += HandleTimerEnd;

            gameTimer.Set(gameDuration);
        }
        else
        {
            Debug.LogError(name + ": Game Timer is null");
        }

    }

    private void ConfigureTimer()
    {
        gameTimer.StartTimer();
        if (gameTimerText) gameTimerText.text = gameDuration.ToString();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find Player
        playerObj = GameObject.Find("Player");
        if (playerObj)
        {
            playerStats = playerObj.GetComponent<PlayerStats>();
            playerAttack = playerObj.GetComponent<PlayerAttack>();
            playerMovement = playerObj.GetComponent<PlayerMovement>();

            playerStats.TakeStats(stats);
            playerAttack.TakeStats(stats);
            playerMovement.TakeStats(stats);
        }
        else
        {
            Debug.Log("GameManager Failed to find Player on scene reload");
        }

        if (scene.name != "Menu")
        {
            gameTimerText = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>();
            gameTimer.Set(gameDuration);
            ConfigureTimer();
        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("sample map");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void HandleTimerEnd()
    {
        //Debug.Log(name + ": Time is Up!");
        HandleRunEnd();

    }

    public void HandleRunEnd()
    {
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
