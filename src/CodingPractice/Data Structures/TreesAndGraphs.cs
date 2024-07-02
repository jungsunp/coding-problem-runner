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

		// #4.2
		// Time: O(n)
		// Space: O(n)
		public static Tree<int> MinimalTree(int[] sortedArray)
		{
			if (sortedArray == null || sortedArray.Length == 0)
			{
				return null;
			}

			var root = MinimalTreeNode(sortedArray, 0, sortedArray.Length - 1);

			return new Tree<int>(root);
		}

		private static TreeNode<int> MinimalTreeNode(int[] sortedArray, int start, int end)
		{
			if (start > end)
			{
				return null;
			}

			int index = (start + end + 1) / 2;
			var parent = new TreeNode<int>(sortedArray[index]);
			var leftChild = MinimalTreeNode(sortedArray, start, index -1);
			var rightChild = MinimalTreeNode(sortedArray, index + 1, end);

			if (leftChild != null)
			{
				parent.AddChild(leftChild);
			}

			if (rightChild != null)
			{
				parent.AddChild(rightChild);
			}

			return parent;
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

	public class TreeNode<T>
	{
		public T Value;
		public List<TreeNode<T>> Children;
		public TreeNode(T value)
		{
			this.Value = value;
			this.Children = new List<TreeNode<T>>();
		}
		public void AddChild(TreeNode<T> child)
		{
			this.Children.Add(child);
		}
	}

	public class Tree<T>{
		public TreeNode<T> Root;
		public Tree(T rootVal)
		{
			this.Root = new TreeNode<T>(rootVal);
		}
		public Tree(TreeNode<T> root)
		{
			this.Root = root;
		}
	}
}
