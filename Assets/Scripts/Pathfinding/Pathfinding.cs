using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour {

    List<Node> openSet;
    List<Node> closedSet;

    public Graph graph;
    Transform start, target;

    public List<Node> pathToFollow;

    Vector3 offset = new Vector3(0.5f, 0.5f, 0f);

    // Use this for initialization
    void Start () {
        //FindPath(startPosition.position, targetPosition.position);

        start = transform;
        target = GameObject.Find("Player").transform;

    }
	
	// Update is called once per frame
	void Update () {
        FindPath(start.position - offset, target.position - offset);
            
	}

    private void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        //Debug.Log(name +  ": Finding path");
        Node startNode = graph.NodeFromWorldPoint(startPos);
        Node targetNode = graph.NodeFromWorldPoint(targetPos);

        // prevents changing the path if we happen to be starting closest to node thats not traversable
        if (!startNode.Traversable) return;

        openSet = new List<Node>();
        closedSet = new List<Node>();

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
                // Debug
                //for (int i = 0; i < graph.path.Count; i++)
                //{
                //    Debug.Log(name + ": Node to graph position: " + graph.path[i].GraphPosition  + " with tile position at: " + graph.path[i].WorldPosition);

                //}
                // End Debug
                return;
            }

            foreach (var neighbor in graph.GetNeighbors(node))
            {
                if (neighbor == null) Debug.Log("no neighbor");
                // If neighbor is not traversable or we've already processed it, move on
                if (!neighbor.Traversable || closedSet.Contains(neighbor))
                    continue;

                int newNeighborCost = node.gCost + GetDistance(node, neighbor);
                if(newNeighborCost < neighbor.gCost || !openSet.Contains(neighbor))
                {
                    neighbor.gCost = newNeighborCost;
                    neighbor.hCost = GetDistance(neighbor, targetNode);
                    neighbor.parent = node;

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }

    }

    private int GetDistance(Node node, Node neighbor)
    {
        // using multiply by 10 and 14 approach for calculations
        // uses the Euclidean distance for diagonals which is ~1.4
        int xDistance = Mathf.Abs(node.GraphPosition.x - neighbor.GraphPosition.x);
        int yDistance = Mathf.Abs(node.GraphPosition.y - neighbor.GraphPosition.y);
        //Debug.Log(name + ": Distance: " + "(" + xDistance  + ", " + yDistance + ")");
        // This produces (1,0) (1, 1) or (0,1) pairs.
        

        if (xDistance > yDistance)
        {
            // (1, 0) 14 * 1 + 10 * (1 - 0) = 28           
            return 14 * yDistance + 10 * (xDistance - yDistance);
        }

        // (0, 1) 14 * 0 + 10 * (1 - 0) = 10
        // (1, 1) 14 * 1 + 10 * (1 - 1) = 14

        return 14 * xDistance + 10 * (yDistance - xDistance);
    }

    private void RetracePath(Node startNode, Node targetNode)
    {
        //Debug.Log(name + ": Retracing path");

        List<Node> path = new List<Node>();
        Node currentNode = targetNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        pathToFollow = path;

        if(graph.path != null)
            graph.path.Clear();
        graph.path = path;
    }
}
