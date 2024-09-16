using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace CodingPractice
{
	public static class TreesAndGraphs
	{
		// #4.1
		public static bool RouteBetweenNodes(GraphNode n1, GraphNode n2)
		{
			if (n1 == null || n2 == null)
			{
				return false;
			}

			if (n1 == n2)
			{
				return true;
			}

			Queue<GraphNode> queue = new Queue<GraphNode>();
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
			if (tree == null || tree.Root == null)
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

		// #4.7
		// Time: O(P + D)
		// Space: O(P + D)
		public static string BuildOrder(string[] projects, string[][] dependencies)
		{
			ProjectGraph graph = BuildProjectGraph(projects, dependencies);

			Stack<Project> stack = GetBuildOrderStack(graph);

			string ret = "";
			while(stack.Count > 0)
			{
				if (ret != "")
				{
					ret += ", ";
				}
				ret += stack.Pop().Name;
			}

			return ret;
		}

		private static ProjectGraph BuildProjectGraph(string[] projects, string[][] dependencies)
		{
			var graph = new ProjectGraph();
			foreach(string proj in projects)
			{
				graph.AddProject(proj);
			}

			foreach(string[] dep in dependencies)
			{
				graph.AddDependency(dep[0], dep[1]);
			}

			return graph;
		}

		private static Stack<Project> GetBuildOrderStack(ProjectGraph graph)
		{
			var stack = new Stack<Project>();

			foreach (Project proj in graph.Projects)
			{
				RunDfsOnProject(proj, stack);
			}

			return stack;
		}

		private static void RunDfsOnProject(Project project, Stack<Project> stack)
		{
			if (project.Status == ProjectStatus.InProgress)
			{
				throw new Exception("Build Error: Circular Dependency!");
			}

			if (project.Status == ProjectStatus.NotStarted)
			{
				project.Status = ProjectStatus.InProgress;
				foreach(Project dep in project.BuildBeforeList)
				{
					RunDfsOnProject(dep, stack);
				}
				project.Status = ProjectStatus.Completed;
				stack.Push(project);
			}
		}
	}

	public class GraphNode
	{
		public object Value { get; set; }
		public  List<GraphNode> Adjacent { get; set; }
		public bool Visited = false;

		public GraphNode(object value)
		{
			this.Value = value;
			Adjacent = new List<GraphNode>();
		}

		 public void AddAdjacent(GraphNode node)
		{
			Adjacent.Add(node);
		}
	}

	public class Graph
	{
		public List<GraphNode> Nodes { get; private set; }

		public Graph()
		{
			Nodes = new List<GraphNode>();
		}

		public void AddNode(GraphNode node)
		{
			Nodes.Add(node);
		}

		public void AddEdge(GraphNode from, GraphNode to)
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

	public class Project
	{
		public string Name { get; private set; }
		public List<Project> BuildBeforeList { get; private set; }
		public ProjectStatus Status { get; set; }

		public Project(string name)
		{
			this.Name = name;
			this.BuildBeforeList = new List<Project>();
			this.Status = ProjectStatus.NotStarted;
		}

		public void AddToBuildBeforeList(Project proj)
		{
			this.BuildBeforeList.Add(proj);
		}
	}

	public class ProjectGraph
	{
		public List<Project> Projects { get; private set; }
		public Dictionary<string, Project> Map { get; }

		public ProjectGraph()
		{
			this.Projects = new List<Project>();
			this.Map = new Dictionary<string, Project>();
		}

		public void AddProject(string name)
		{
			var proj = new Project(name);
			Projects.Add(proj);
			Map.Add(name, proj);
		}

		// P1 needs to be built before P2 (i.e P2 depends on P1)
		public void AddDependency(string p1, string p2)
		{
			var proj1 = Map[p1];
			var proj2 = Map[p2];

			proj1.AddToBuildBeforeList(proj2);
		}
	}

	public enum ProjectStatus
	{
		NotStarted,
		InProgress,
		Completed
	}

	// #4.11
	public class RandomBinaryTree
	{
		public RandomNode Root { get; private set; }

		// Time: O(log n)
		// Space: O(log n)
		public void Insert(int val)
		{
			if (Root == null)
			{
				this.Root = new RandomNode(val);
				return;
			}

			this.Root.Insert(val);
		}



		// Time: O(log n)
		// Space: O(log n)
		public RandomNode Find(int val)
		{
			return this.Root.Find(val);
		}

		// Time: O(log n)
		// Space: O(log n)
		public RandomNode GetRandomNode()
		{
			if (this.Root == null)
			{
				return null;
			}

			Random rnd = new Random();
			// Note: generating random number can be expensive
			int randomNumber = rnd.Next(0, this.Root.Size);

			return this.Root.GetNodeAtIndex(randomNumber);
		}
	}

	public class RandomNode
	{
		public int Value { get; private set; }
		public int Size = 0;
		public RandomNode Left;
		public RandomNode Right;
		public RandomNode(int val)
		{
			this.Value = val;
		}

		public void Insert(int val)
		{
			if (this.Value >= val)
			{
				if (this.Left == null)
				{
					this.Left = new RandomNode(val);
				}
				else
				{
					this.Left.Insert(val);
				}
			}
			else
			{
				if (this.Right == null)
				{
					this.Right = new RandomNode(val);
				}
				else
				{
					this.Right.Insert(val);
				}
			}

			this.Size++;
		}

		public RandomNode Find(int val)
		{
			if (this.Value == val)
			{
				return this;
			}
			else if (this.Value > val)
			{
				return this.Left.Find(val);
			}
			else // parent.value < val
			{
				return this.Right.Find(val);
			}
		}

		public RandomNode GetNodeAtIndex(int index)
		{
			int leftSize = this.Left == null ? 0 : this.Left.Size;

			if (index < leftSize)
			{
				return this.Left.GetNodeAtIndex(index);
			}
			else if (index == leftSize)
			{
				return this;
			}
			else
			{
				return this.Right.GetNodeAtIndex(index - leftSize - 1);
			}
		}
	}

}
