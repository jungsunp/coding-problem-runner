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
		// Space: O(n) (Call stack: O(log n))
		public static BinaryTree<int> MinimalTree(int[] sortedArray)
		{
			if (sortedArray == null || sortedArray.Length == 0)
			{
				return null;
			}

			var root = MinimalTreeNode(sortedArray, 0, sortedArray.Length - 1);

			return new BinaryTree<int>(root);
		}

		private static BinaryTreeNode<int> MinimalTreeNode(int[] sortedArray, int start, int end)
		{
			if (start > end)
			{
				return null;
			}

			int index = (start + end + 1) / 2;
			var parent = new BinaryTreeNode<int>(sortedArray[index]);
			parent.Left = MinimalTreeNode(sortedArray, start, index -1);
			parent.Right = MinimalTreeNode(sortedArray, index + 1, end);

			return parent;
		}

		// #4.4
		// Time: O(n)
		// Space: O(h) - h: height of tree
		public static bool CheckBalanced<T>(BinaryTree<T> tree)
		{
			if (tree == null ||tree.Root == null)
			{
				throw new InvalidOperationException();
			}

			int leftSubHeight = GetSubTreeHeight(tree.Root.Left);
			if (leftSubHeight < 0)
			{
				return false; // left subtree unbalanced. no need to check further
			}

			int rightSubHeight = GetSubTreeHeight(tree.Root.Right);
			if (rightSubHeight < 0 || Math.Abs(leftSubHeight - rightSubHeight) > 1)
			{
				return false;
			}

			return true;
		}


		// Returns -1 if tree is unbalanced
		private static int GetSubTreeHeight<T>(BinaryTreeNode<T> node)
		{
			if (node == null)
			{
				return 0;
			}

			int leftSubHeight = GetSubTreeHeight(node.Left);
			if (leftSubHeight < 0)
			{
				return -1; // left subtree unbalanced. no need to check further
			}

			int rightSubHeight = GetSubTreeHeight(node.Right);
			if (rightSubHeight < 0 || Math.Abs(leftSubHeight - rightSubHeight) > 1)
			{
				return -1;
			}

			return Math.Max(leftSubHeight, rightSubHeight) + 1;
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


	public class BinaryTreeNode<T>
	{
		public T Value;
		public BinaryTreeNode<T> Left;
		public BinaryTreeNode<T> Right;
		public BinaryTreeNode(T value)
		{
			this.Value = value;
		}
	}

	public class BinaryTree<T>{
		public BinaryTreeNode<T> Root;
		public BinaryTree(T rootVal)
		{
			this.Root = new BinaryTreeNode<T>(rootVal);
		}
		public BinaryTree(BinaryTreeNode<T> root)
		{
			this.Root = root;
		}
	}
}
