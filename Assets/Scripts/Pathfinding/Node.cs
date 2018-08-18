using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {

    int xPos;
    int yPos;
    bool traversable;

    Node parent;

    int gCost;
    int hCost;
    public int fCost { get { return gCost + hCost; } }

    public Node(int x, int y, bool traversable)
    {
        xPos = x;
        yPos = y;
        this.traversable = traversable;
    }
}
