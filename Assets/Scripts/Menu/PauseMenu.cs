using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    [HeaderAttribute("Dependencies")]
    public GameObject pauseButton;
    public GameObject pauseMenu;

    private bool menuOpen = false;

    // Use this for initialization
    void Start () {
		
	}

    public void ToggleMenu()
    {
        Debug.Log(name + ": Toggling Menu");

        menuOpen = !menuOpen;
        pauseButton.SetActive(!menuOpen);
        pauseMenu.SetActive(menuOpen);
    }

    public void OnMainMenuButtonPressed()
    {
        Debug.Log(name + ": Main Menu Button Pressed");


    }

    public void OnRestartButton()
    {
        Debug.Log(name + ": Restart Button Pressed");
        GameManager.Instance.HandleRunEnd();
    }

}
