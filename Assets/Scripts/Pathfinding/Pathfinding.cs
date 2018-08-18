using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour {

    List<Node> openSet;
    List<Node> closedSet;


    public Graph graph;
    public Transform startPosition, targetPosition;

    private void Awake()
    {
        openSet = new List<Node>();
        closedSet = new List<Node>();
    }

    // Use this for initialization
    void Start () {
        FindPath(startPosition.position, targetPosition.position);
    }
	
	// Update is called once per frame
	void Update () {
        //FindPath(startPosition.position, targetPosition.position);
	}

    private void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Node startNode = graph.NodeFromWorldPoint(startPos);
        Node targetNode = graph.NodeFromWorldPoint(targetPos);

        openSet.Add(startNode);

        while(openSet.Count > 0)
        {
            Node node = openSet[0];
            for (int i = 0; i < openSet.Count; i++)
            {
                if(openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost)
                {
                    if (openSet[i].hCost < node.hCost)
                        node = openSet[i];
                }
            }

            openSet.Remove(node);
            closedSet.Add(node);

            if(node == targetNode)
            {
                RetracePath(startNode, targetNode);
                return;
            }

            foreach (var neighbor in graph.GetNeighbors(node))
            {

            }
        }

    }

    private void RetracePath(Node startNode, Node targetNode)
    {
        throw new NotImplementedException();
    }
}
