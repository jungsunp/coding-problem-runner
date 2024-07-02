using System;
using System.Collections.Generic;

namespace CodingPractice
{
	public static class TreesAndGraphs
	{
		// #4.1
		public static bool RouteBetweenNodes(Node n1, Node n2)
		{
			if (n1 == null || n2 == null)
			{
				return false;
			}

			if (n1 == n2)
			{
				return true;
			}

			Queue<Node> queue = new Queue<Node>();
			n1.Visited = true;
			queue.Enqueue(n1);

			while (queue.Count > 0)
			{
				var node = queue.Dequeue();
				foreach (var neighbor in node.Adjacent)
				{
					if (neighbor == n2)
					{
						return true;
					}

					if (!neighbor.Visited)
					{
						neighbor.Visited = true;
						queue.Enqueue(neighbor);
					}
				}
			}

			return false;
		}

	}


	public class Node
	{
		public object Value { get; set; }
		public  List<Node> Adjacent { get; set; }
		public bool Visited = false;

		public Node(object value)
		{
			this.Value = value;
			Adjacent = new List<Node>();
		}

		 public void AddAdjacent(Node node)
		{
			Adjacent.Add(node);
		}
	}


	public class Graph
	{
		public List<Node> Nodes { get; private set; }

		public Graph()
		{
			Nodes = new List<Node>();
		}

		public void AddNode(Node node)
		{
			Nodes.Add(node);
		}

		public void AddEdge(Node from, Node to)
		{
			from.AddAdjacent(to);
		}
	}
}
