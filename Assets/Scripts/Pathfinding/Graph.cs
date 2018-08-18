using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Tilemaps;

public class Graph : MonoBehaviour {

    public Vector2 scanStartBottomLeft;
    public Vector2 scanFinishTopRight;
    public Tilemap groundTileMap;               // we want this to know what within our scan is traversable
    public List<Tilemap> collisionTilemaps;

    Node[,] graph;

    public int GraphWidth
    {
        get
        {
            // TODO some caching can be done here
            return Mathf.RoundToInt(Mathf.Abs(scanFinishTopRight.x - scanStartBottomLeft.x));
        }
    }

    public int GraphHeight
    {
        get
        {
            // TODO some caching can be done here
            return Mathf.RoundToInt(Mathf.Abs(scanFinishTopRight.y - scanStartBottomLeft.y)); 
        }
    }

    private void Awake()
    {

        graph = new Node[GraphWidth, GraphHeight];
        Assert.IsNotNull(groundTileMap, "GroundTilemap is null");

        Debug.Log(name + ": GraphWidth:" + GraphWidth + ", GraphHeight:" + GraphHeight);
    }


    void CreateGraphFromTileMap(List<Tilemap> collisionTileMaps)
    {
        // for x,y of scan area
        for (int x = Mathf.RoundToInt(scanStartBottomLeft.x); x < Mathf.RoundToInt(scanFinishTopRight.x); x++)
        {
            for (int y = Mathf.RoundToInt(scanStartBottomLeft.y); y < Mathf.RoundToInt(scanFinishTopRight.y); y++)
            {
                // Check if current position has a ground tile to traverse on
                // TODO Same kind of tile checking is being done elsewhere
                TileBase tile = groundTileMap.GetTile(new Vector3Int(x, y, 0));

                // if not, continue
                if (tile == null)
                {
                    Debug.Log(name + ": Tile not found at position " + "(" + x + ", " + y + ", 0)");
                }
                // if so detect if there are any collisions on this tile
                else
                {
                    //Debug.Log(name + ": Tile found at position " + "(" + x + ", " + y + ", 0)");

                    bool collisionDetected = false;

                    foreach (var collisionMap in collisionTilemaps)
                    {
                        // Perform the same check for a tile at this position
                        // TODO Same kind of tile checking is being done elsewhere
                        TileBase collisionTile = collisionMap.GetTile(new Vector3Int(x, y, 0));

                        // if not, continue
                        if (collisionTile == null)
                        {
                            //Debug.Log(name + ": Collision tile not found at position " + "(" + x + ", " + y + ", 0)");
                        }
                        // else we have found one so set the flag
                        else
                        {
                            collisionDetected = true;
                            //Debug.Log(name + ": Collision tile found at position " + "(" + x + ", " + y + ", 0)");
                        }


                        // Create a node that is traversable if !collisionDetected; not traversable if collisionDetected
                        Node node = new Node(x, y, !collisionDetected);
                        Debug.Log(name + ": Adding node to graph position: " + "(" + (x + GraphWidth / 2 ) + ", " + (y + GraphHeight / 2) + ") with tile position at: " + "(" + x + ", " + y + ", 0)");

                        graph[x + GraphWidth/2, y + GraphHeight / 2] = node;     // add node to the graph

                        if (collisionDetected)
                        {
                            // we have found a collision for this point, we can continue to the next
                            break;  // break out of collisionTilemaps loop
                        }
                    }                    
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(Mathf.Abs(scanFinishTopRight.x - scanStartBottomLeft.x), Mathf.Abs(scanFinishTopRight.y - scanStartBottomLeft.y)));
    }

    // Use this for initialization
    void Start () {
        CreateGraphFromTileMap(collisionTilemaps);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
