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
    public float currentExp;
    public float maxExp;
    public int level;
}

public class GameManager : MonoBehaviour {
    
    public static GameManager Instance;

    #region Public Variables

    [HeaderAttribute("Game Settings")]
    public float gameDuration = 300f; // 5 Minutes

    [HeaderAttribute("Player Stats")]
    public float statUpMin = 1.2f;
    public float statUpMax = 1.4f;
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
            //Debug.Log(name + ": Game Timer is present");
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
        SendStats();

        if (scene.name != "Menu")
        {
            gameTimerText = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>();
            gameTimer.Set(gameDuration);
            ConfigureTimer();
        }
    }

    public void SendStats()
    {
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
    }

    public float UpgradePlayer(float currentExp)
    {
        float newMaxExp = currentExp * 1.2f;
        stats.maxHealth *= UnityEngine.Random.Range(statUpMin, statUpMax);
        stats.damage *= UnityEngine.Random.Range(statUpMin, statUpMax);
        stats.attackSpeed -= UnityEngine.Random.Range(0.1f, 0.2f);
        stats.weaponSize *= UnityEngine.Random.Range(statUpMin, statUpMax);
        stats.walkSpeed *= UnityEngine.Random.Range(statUpMin, statUpMax);
        stats.currentExp = 0;
        stats.maxExp = newMaxExp;
        stats.level += 1;

        SendStats();

        return newMaxExp;
    }

    public void setExp(float amount)
    {
        stats.currentExp = amount;
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
