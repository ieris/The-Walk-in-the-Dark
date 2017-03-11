using UnityEngine;
using System.Collections;

<<<<<<< HEAD
public class Node {
	
	public bool walkable;
	public Vector3 worldPosition;
	public int gridX;
	public int gridY;

	public int gCost;
	public int hCost;
	public Node parent;
	
	public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY) {
		walkable = _walkable;
		worldPosition = _worldPos;
		gridX = _gridX;
		gridY = _gridY;
	}

	public int fCost {
		get {
			return gCost + hCost;
		}
	}
=======
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
>>>>>>> parent of e05b547... Added Comments
}
