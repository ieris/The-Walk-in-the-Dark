using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Searches through the graph
public class Search : MonoBehaviour
{
    //Creating references for nodes, and graph
    /*public Graph graph;
    public List<Node> reachable; //Open
    public List<Node> explored;  //Closed
    public List<Node> path;
    public Node startNode;
    public Node targetNode;

    //Tracks how many iterations have been completed for debuging purposes
    public int iterations;
    public bool finished;

    //Constructor : takes a graph
    public Search(Graph graph)
    {
        this.graph = graph;
    }

    void Awake()
    {
        graph = GetComponent<Graph>();
    }

    //Create the search method which takes in a start and target node
    public void Start(Node start, Node target)
    {
        //Add the start node to the reachable/open list
        reachable = new List<Node>();
        reachable.Add(start);

        targetNode = target;

        //Create the explored/closed list and path list
        explored = new List<Node>();
        path = new List<Node>();
        iterations = 0;

        //Clear the graph in case we have ran this previously
        /*for(var i = 0; i < graph.nodes.Length; i++)
        {
            graph.nodes[i].Clear();
        }*/
    /*}

    //Checks possible moves that can be made
    public void Step()
    {
        if (path.Count > 0)
        {
            return;
        }

        //Check if we ran out of options
        if (reachable.Count == 0)
        {
            finished = true;
            return;
        }

        //Track number of iterations for performance purposes
        iterations++;

        //Pick a node to start the search from
        var node = ChooseNode();

        //Check if the node is the target node
        //Add the node to the path and set the node as the previous node
        if(node == targetNode)
        {
            while(node != null)
            {
                path.Insert(0, node);
                node = node.previous;
            }
            finished = true;
            return;
        }

        //Remove the current node from the open list
        //Add it in the closed list
        reachable.Remove(node);
        explored.Add(node);

        //Iterate through adjacent nodes
        //For all values, add adjacent nodes
        for (var i = 0; i < node.adjacent.Count; i++)
        {
            AddAdjacent(node, node.adjacent[i]);
        }
    }

    //Loops through all adjacent nodes and finds next
    //available options. Makes that node available (open List)
    //and creates connection at previous node.
    public void AddAdjacent(Node node, Node adjacent)
    {
        //If found, we return the node and we have found a new path
        if(FindNode(adjacent, explored) || FindNode(adjacent, reachable))
        {
            return;
        }

        //Set the previous node from adj to current node
        //Add adj node to open list
        adjacent.previous = node;
        reachable.Add(adjacent);
    }
    
    //Finds a node in the list
    public bool FindNode(Node node, List<Node> list)
    {
        return getNodeIndex(node, list) >= 0;
    }

    //Tests is node is in the list
    public int getNodeIndex(Node node, List<Node> list)
    {
        for (var i = 0; i < list.Count; i++)
        {
            if(node == list[i])
            {
                return i;
            }
        }

        return -1;
    }

    //Chooses the node to check

    public Node ChooseNode()
    {
        //return reachable[Random.Range(0, reachable.Count)];

        while(reachable.Count > 0)
        {
            Node currentNode = reachable[0];
            //Check which node has the lowest f cost
            //If the f cost is the same, the one closest to the end node will get chosen
            for (int i = 1; i < reachable.Count; i++)
            {
                if(reachable[i].f < currentNode.f || reachable[i].f == currentNode.f && reachable[i].h < currentNode.h)
                {
                    currentNode = reachable[i];
                }
            }

            //Remove node from open list and add it to the closed list
            reachable.Remove(currentNode);
            explored.Add(currentNode);

            if(currentNode == targetNode)
            {
                return;
            }

            foreach(Node adjacent in currentNode.adjacent)
            {
                if(!adjacent.walkable || explored.Contains(adjacent))
                {
                    continue;
                }

                int newMovementCostToNeighbout = currentNode.g + GetDistance(currentNode, adjacent);

                if(newMovementCostToNeighbout < adjacent.g || !reachable.Contains(adjacent))
                {
                    adjacent.g = newMovementCostToNeighbout;
                    adjacent.h = GetDistance(adjacent, targetNode);
                    adjacent.parent = currentNode;

                    if(!reachable.Contains(adjacent))
                    {
                        reachable.Add(adjacent);
                    }
                }
            }
        }

        return;

    }*/

    public Transform seeker, target;
    Graph grid;

    void Awake()
    {
        grid = GetComponent<Graph>();
    }

    void Update()
    {
        FindPath(seeker.position, target.position);
    }

    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node node = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost)
                {
                    if (openSet[i].hCost < node.hCost)
                        node = openSet[i];
                }
            }

            openSet.Remove(node);
            closedSet.Add(node);

            if (node == targetNode)
            {
                RetracePath(startNode, targetNode);
                return;
            }

            foreach (Node neighbour in grid.GetNeighbours(node))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
                if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = node;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }
    }

    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        grid.path = path;

    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
}
