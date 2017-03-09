using UnityEngine;
using System.Collections.Generic;

//Represents the position of each space in the game
public class Node
{
    //Keep track of previous nodes looked at and adjacent nodes
    /* public List<Node> adjacent = new List<Node>();
     public Node previous = null;

     //Label nodes so it is easier to track them
     public string label = "";

     //A* variables
     public int g, h;
     public bool walkable;
     public Vector3 worldPos;

     //Calculates the heuristic of a node
     public int f
     {
         get
         {
             return g + h;
         }
     }

     //Node constructor
     /*public Node(bool _walkable, Vector3 _worldPos)
     {
         walkable = _walkable;
         worldPos = _worldPos;
     }*/

    //Allow to reset a node
    /* public void Clear()
     {
         previous = null;
     }*/

    /*public class Node
    {*/
    public bool walkable;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;
    public Node parent;

    public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY)
    {
        walkable = _walkable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
}
