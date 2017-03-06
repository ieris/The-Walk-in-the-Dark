using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Searches through the graph
public class Search
{
    //Creating references for nodes, and graph
    public Graph graph;
    public List<Node> reachable; //Open
    public List<Node> explored;  //Closed
    public List<Node> path;
    public Node targetNode;

    //Tracks how many iterations have been completed for debug purposes
    public int iterations;
    public bool finished;

    //Constructor : takes a graph
    public Search(Graph graph)
    {
        this.graph = graph;
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
        for(var i = 0; i < graph.nodes.Length; i++)
        {
            graph.nodes[i].Clear();
        }
    }

    //
    public void Step()
    {
        if (path.Count > 0)
        {
            return;
        }
        if (reachable.Count == 0)
        {
            finished = true;
            return;
        }

        iterations++;

        var node = ChooseNode();
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

        reachable.Remove(node);
        explored.Add(node);

        for(var i = 0; i < node.adjacent.Count; i++)
        {
            AddAdjacent(node, node.adjacent[i]);
        }
    }

    public void AddAdjacent(Node node, Node adjacent)
    {
        if(FindNode(adjacent, explored) || FindNode(adjacent, reachable))
        {
            return;
        }

        adjacent.previous = node;
        reachable.Add(adjacent);
    }
    
    public bool FindNode(Node node, List<Node> list)
    {
        return getNodeIndex(node, list) >= 0;
    }

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

    public Node ChooseNode()
    {
        return reachable[Random.Range(0, reachable.Count)];
    }
}
