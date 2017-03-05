using UnityEngine;
using System.Collections.Generic;

//Represents the position of each space in the game
public class Node
{
    //Keep track of previous and adjacent nodes
    //Label nodes so it is easier to track them
    //Allow to reset a node
    public List<Node> adjacent = new List<Node>();
    public Node previous = null;
    public string label = "";
    //public int g, h;

    /*public int f
    {
        get
        {
            return g + h;
        }
    }*/
    public void Clear()
    {
        previous = null;
    }
}
