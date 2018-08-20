using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {

    //int xPos;
    //int yPos;
    bool traversable;
    Vector3 worldPos;
    Vector3Int graphPos;

    public Node parent;
    public int gCost;
    public int hCost;
    public int fCost { get { return gCost + hCost; } }
    public int X { get { return Mathf.RoundToInt(worldPos.x); } }
    public int Y { get { return Mathf.RoundToInt(worldPos.y); } }
    public Vector3Int GraphPosition { get { return graphPos; } }
    public Vector3 WorldPosition { get { return worldPos; } }
    public bool Traversable { get { return traversable; } }

    //public Node(int x, int y, bool traversable)
    //{
    //    xPos = x;
    //    yPos = y;
    //    this.traversable = traversable;
    //}

    public Node(Vector3 worldPosition, Vector3Int graphPosition, bool traversable)
    {
        worldPos = worldPosition;
        graphPos = graphPosition;
        this.traversable = traversable;
    }
}
