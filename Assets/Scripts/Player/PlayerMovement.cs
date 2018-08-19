using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [HeaderAttribute("Dependencies")]
    public GameObject player;

    // Variables that affect the movement of the player
    [HeaderAttribute("Movement Variables")]
    public float currentSpeed = 0.0f;
    public float walkSpeed = 10.0f;


    // Variables that affect the rotation of the player
    [HeaderAttribute("Rotation Variables")]
    public float cameraRadius = 0.001f;
    public float directionOffset = 0f;

    void Start () {
        currentSpeed = walkSpeed;
    }
	

	void Update () {
        Vector3 currentPosition = transform.position;

        // Player Movement
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * currentSpeed;
        var y = Input.GetAxis("Vertical") * Time.deltaTime * currentSpeed;

        transform.Translate(x, y, 0.0f);
    }

    public void TakeStats(PlayerAttributes stats)
    {
        currentSpeed = stats.walkSpeed;
    }

    //Throwing the rotation in the FixedUpdate means it won't be called when TimeScale == 0.
    void FixedUpdate()
    {
        //Player rotation
        Vector2 playerOnScreen = Camera.main.WorldToViewportPoint(transform.position);

        // Position of Mouse
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        if (Vector2.Distance(playerOnScreen, mouseOnScreen) >= cameraRadius)
        {
            // Angle between player and mouse
            float angle = Mathf.Atan2(playerOnScreen.y - mouseOnScreen.y, playerOnScreen.x - mouseOnScreen.x) * Mathf.Rad2Deg;

            // Rotate
            player.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle + directionOffset));
        }
    }
}
