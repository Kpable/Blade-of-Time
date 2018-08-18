using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour {

    List<Node> openSet;
    List<Node> closedSet;

    private void Awake()
    {
        openSet = new List<Node>();
        closedSet = new List<Node>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
