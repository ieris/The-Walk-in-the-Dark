using UnityEngine;
using System.Collections;

//A graph is used to link the nodes together
public class Graph
{
    public int rows = 0;
    public int columns = 0;
    public Node[] nodes;

    //constructor: accepts 2d array
    public Graph(int[,] grid)
    {
        rows = grid.GetLength(0);
        columns = grid.GetLength(1);

        //Create an empty node for each space in the graph
        nodes = new Node[grid.Length];
        for (var i = 0; i < nodes.Length; i++)
        {
            var node = new Node();
            node.label = i.ToString();
            nodes[i] = node;
        }

        //Fill the array with empty nodes and label each node with its position
        for (var r = 0; r < rows; r++)
        {
            for (var c = 0; c < columns; c++)
            {
                var node = nodes[columns * r + c];

                //Open node is represented by 0
                //Closed node is represented by 1
                if(grid[r,c] == 1)
                {
                    continue;
                }

                //Up
                //Add any node above the current one
                if(r > 0)
                {
                    node.adjacent.Add(nodes[columns * (r - 1) + c]);
                }

                //Right
                //Looks for any node to the right of the current node
                if (c < columns - 1)
                {
                    node.adjacent.Add(nodes[columns * r + c + 1]);
                }

                //Down
                //Looks for any nodes below the current one
                if (r < rows - 1)
                {
                    node.adjacent.Add(nodes[columns * (r + 1) + c]);
                }

                //Left
                //Looks for any nodes to the left of the current node
                if (c > 0)
                {
                    node.adjacent.Add(nodes[columns * r + c - 1]);
                }
            }
        }
    }
}
