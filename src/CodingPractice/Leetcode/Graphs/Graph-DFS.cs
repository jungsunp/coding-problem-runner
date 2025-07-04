using System;
using System.Collections.Generic;
using CodingPractice.Leetcode.Graphs.Custom;

namespace CodingPractice.Leetcode
{
	public class Graph_DFS
	{
		// #547. Number of Provinces (DFS)
		// Time: O(n^2)
		// Space: O(n)
		public int FindCircleNum(int[][] isConnected)
		{
			int numCities = isConnected.Length;
			HashSet<int> visited = new HashSet<int>();
			int numProvinces = 0;

			for (int i = 0; i < numCities; i++)
			{
				if (visited.Contains(i))
				{
					continue; // already visited. move over to next city
				}

				numProvinces++;
				Stack<int> toVisit = new Stack<int>();
				toVisit.Push(i);

				while (toVisit.Count > 0) // Run DFS
				{
					int currentCity = toVisit.Pop();
					visited.Add(currentCity);
					int[] connectedCities = isConnected[currentCity];
					for (int j = 0; j < numCities; j++)  // only need to check upper half of matrix
					{
						if (connectedCities[j] == 1 && !visited.Contains(j))
						{
							toVisit.Push(j);
						}
					}
				}
			}

			return numProvinces;
		}

		//# 1466. Reorder Routes to Make All Paths Lead to the City Zero (DFS)
		// Time: O(n)
		// Space: O(n)
		public int MinReorder(int n, int[][] connections)
		{

			// Build hash map with connections
			Dictionary<int, List<ConnectedCity>> map = new Dictionary<int, List<ConnectedCity>>();
			foreach (int[] connection in connections)
			{
				int origin = connection[0];
				int destination = connection[1];

				// Add original direction
				if (map.ContainsKey(origin))
				{
					map[origin].Add(new ConnectedCity(destination, true));
				}
				else
				{
					map[origin] = new List<ConnectedCity> { new ConnectedCity(destination, true) };
				}

				// Add reverse direction
				if (map.ContainsKey(destination))
				{
					map[destination].Add(new ConnectedCity(origin, false));
				}
				else
				{
					map[destination] = new List<ConnectedCity> { new ConnectedCity(origin, false) };
				}
			}

			// Run DFS
			Stack<int> toVisit = new Stack<int>();
			HashSet<int> visited = new HashSet<int>();
			int reorder = 0;
			toVisit.Push(0); // it's guranteed all cities are connected. can just start with 0 and run bi-directional DFS

			while (toVisit.Count > 0)
			{
				int city = toVisit.Pop();
				visited.Add(city);

				List<ConnectedCity> connectedCities = map[city]; // guranteed to be connected.
				foreach (ConnectedCity cc in connectedCities)
				{
					if (!visited.Contains(cc.city)) // visiting new city
					{
						toVisit.Push(cc.city);
						if (cc.origDir)
						{ // if reached with original direction, need to reverse it
							reorder++;
						}
					}
				}
			}

			return reorder;
		}

		private readonly struct ConnectedCity
		{
			public readonly int city;
			public readonly bool origDir; // original direction (i.e no need to reorder)

			public ConnectedCity(int city, bool origDir)
			{
				this.city = city;
				this.origDir = origDir;
			}
		}

		// #399. Evaluate Division (DFS)
		// Time: O(m * n) - m: number of queries, n: number of equations
		// Space: O(n)
		public double[] CalcEquation(IList<IList<string>> equations, double[] values, IList<IList<string>> queries)
		{

			// Build hash map with graph edges
			Dictionary<string, IList<Edge>> map = new Dictionary<string, IList<Edge>>();
			for (int i = 0; i < equations.Count; i++)
			{
				string numerator = equations[i][0];
				string denominator = equations[i][1];

				// Add original direction edge
				if (!map.ContainsKey(numerator))
				{
					map.Add(numerator, new List<Edge>());
				}
				map[numerator].Add(new Edge(denominator, values[i]));

				// Add reverse direction edge
				if (!map.ContainsKey(denominator))
				{
					map.Add(denominator, new List<Edge>());
				}
				map[denominator].Add(new Edge(numerator, 1 / values[i])); // add edge weight as 1 / value
			}

			// Calculate queries
			double[] ret = new double[queries.Count];
			for (int i = 0; i < queries.Count; i++)
			{
				string numerator = queries[i][0];
				string denominator = queries[i][1];
				ret[i] = -1; // default - unable to calculate

				if (!map.ContainsKey(numerator))
				{
					continue;
				}
				else if (string.Equals(numerator, denominator))
				{
					ret[i] = 1;
					continue;
				}

				// Run DFS (Note: this can also be done with backtracking)
				Stack<Edge> edgeStack = new Stack<Edge>();
				HashSet<string> visited = new HashSet<string>();
				edgeStack.Push(new Edge(numerator, 1));
				while (edgeStack.Count > 0)
				{
					Edge edge = edgeStack.Pop();
					string variable = edge.denominator;
					visited.Add(variable);

					IList<Edge> edgeList = map[variable];
					foreach (Edge nextEdge in edgeList)
					{
						if (!visited.Contains(nextEdge.denominator))
						{
							if (nextEdge.denominator == denominator)
							{
								ret[i] = edge.value * nextEdge.value;
								edgeStack = new Stack<Edge>();
								break;
							}

							edgeStack.Push(new Edge(nextEdge.denominator, edge.value * nextEdge.value));
						}
					}
				}

			}

			return ret;
		}

		private class Edge
		{
			public string denominator;
			public double value;

			public Edge(string denominator, double value)
			{
				this.denominator = denominator;
				this.value = value;
			}
		}

		// #200. Number of Islands (DFS)
		// Time: O(m * n)
		// Space: O(m * n)
		public int NumIslands(char[][] grid)
		{
			int numIslands = 0;

			for (int i = 0; i < grid.Length; i++)
			{
				for (int j = 0; j < grid[i].Length; j++)
				{

					if (grid[i][j] != '1')
					{
						continue;
					}

					numIslands++; // new island found!

					// Run DFS
					this.IslandDfs(grid, i, j);
				}
			}

			return numIslands;
		}

		private void IslandDfs(char[][] grid, int i, int j)
		{
			Stack<(int, int)> stack = new Stack<(int, int)>();
			stack.Push((i, j));

			while (stack.Count > 0)
			{
				(int row, int col) = stack.Pop();

				grid[row][col] = 'v'; // mark visited

				// Move down
				if (row < grid.Length - 1 && grid[row + 1][col] == '1')
				{
					stack.Push((row + 1, col));
				}

				// Move up
				if (row > 0 && grid[row - 1][col] == '1')
				{
					stack.Push((row - 1, col));
				}

				// Move right
				if (col < grid[i].Length - 1 && grid[row][col + 1] == '1')
				{
					stack.Push((row, col + 1));
				}

				// Move left
				if (col > 0 && grid[row][col - 1] == '1')
				{
					stack.Push((row, col - 1));
				}
			}
		}

		// #210. Course Schedule II (DFS)
		// Time: O(n + m) - n: number of courses, m: number of prerequisites
		// Space: O(n + m)
		public int[] FindOrder(int numCourses, int[][] prerequisites)
		{
			// Build prereq map
			Dictionary<int, List<int>> map = new Dictionary<int, List<int>>();
			foreach (int[] prereq in prerequisites)
			{
				if (!map.ContainsKey(prereq[0]))
				{
					map.Add(prereq[0], new List<int>());
				}
				map[prereq[0]].Add(prereq[1]);
			}

			// Run Topological Sort via DFS
			Stack<int> sortStack = new Stack<int>();
			int[] courseStatus = new int[numCourses]; // 0 = not taken, 1 = taking, 2 = taken
			for (int i = 0; i < numCourses; i++)
			{
				if (courseStatus[i] == 0)
				{
					if (!this.topologicalSortDfs(i, map, sortStack, courseStatus))
					{
						return []; // cylce detected from DFS
					}
				}
			}

			// Topological Sorting Success
			int[] ret = new int[numCourses];
			for (int i = numCourses - 1; i >= 0; i--)
			{
				ret[i] = sortStack.Pop();
			}
			return ret;
		}

		private bool topologicalSortDfs(
			int course,
			Dictionary<int, List<int>> map,
			Stack<int> sortStack,
			int[] courseStatus
		)
		{
			courseStatus[course] = 1; // taking

			if (map.ContainsKey(course))
			{
				foreach (int prereq in map[course])
				{
					if (courseStatus[prereq] == 0)
					{
						if (!topologicalSortDfs(prereq, map, sortStack, courseStatus))
						{
							return false; // cycle detected
						}
					}
					else if (courseStatus[prereq] == 1)
					{
						// cycle detected
						return false;
					}
				}
			}

			courseStatus[course] = 2; // taken
			sortStack.Push(course);
			return true;
		}


		// #207. Course Schedule (DFS)
		// Time: O(m + n)  - m: number of prereqs/edges, n: numCourses
		// Space: O(m + n)
		// Note: you can also solve this using BFS(Kahn's algorithm)
		public bool CanFinish(int numCourses, int[][] prerequisites)
		{
			// Build course map
			Dictionary<int, List<int>> map = new();
			foreach (int[] prereq in prerequisites)
			{
				if (!map.ContainsKey(prereq[0]))
				{
					map.Add(prereq[0], new List<int>());
				}
				map[prereq[0]].Add(prereq[1]);
			}

			// Keep track of course status
			// 0 - not taken, 1 - taken, 2 - taking
			int[] courseStatus = new int[numCourses];

			// Run DFS to check if it has cycle
			for (int i = 0; i < numCourses; i++)
			{
				if (courseStatus[i] == 0)
				{
					if (!CourseScheduleDfs(i, map, courseStatus))
					{
						return false; // cycle detected
					}
				}
			}

			return true;
		}

		// return false it cylce detected. true otherwise
		private bool CourseScheduleDfs(int course, Dictionary<int, List<int>> map, int[] courseStatus)
		{
			if (courseStatus[course] == 1)
			{ // taken
				return true;
			}
			if (courseStatus[course] == 2)
			{ // taking
				return false; // cycle detected
			}

			courseStatus[course] = 2; // mark taking

			// attemp to take all prereqs
			if (map.ContainsKey(course))
			{
				foreach (int prereq in map[course])
				{
					if (!CourseScheduleDfs(prereq, map, courseStatus))
					{
						return false;
					}
				}
			}

			courseStatus[course] = 1; // mark taken
			return true;
		}

		// #323. Number of Connected Components in an Undirected Graph (DFS)
		// Time: O(n)
		// Space: O(n + m) - m: number of edges
		// Note: There is faster approach usign Union-Find approach
		//  but stick with DFS for follow up convo.
		public int CountComponents(int n, int[][] edges)
		{
			// Build connection map
			Dictionary<int, List<int>> map = new();
			foreach (int[] edge in edges)
			{
				foreach (int node in edge)
				{ // set up for both directions
					if (!map.ContainsKey(node))
					{
						map[node] = new List<int>();
					}
				}
				map[edge[0]].Add(edge[1]);
				map[edge[1]].Add(edge[0]);
			}

			// Run DFS for each node
			int res = 0;
			Stack<int> dfsStack = new();
			HashSet<int> visited = new();

			for (int i = 0; i < n; i++)
			{
				if (visited.Contains(i))
				{
					continue;
				}

				res++; // new component
				dfsStack.Push(i);
				while (dfsStack.Count > 0)
				{
					int node = dfsStack.Pop();
					if (visited.Contains(node))
					{
						continue;
					}
					visited.Add(node);
					if (map.ContainsKey(node))
					{
						foreach (int neighbor in map[node])
						{
							if (!visited.Contains(neighbor))
							{
								dfsStack.Push(neighbor);
							}
						}
					}
				}
			}

			return res;
		}

		// #339. Nested List Weight Sum
		// Time: O(n)
		// Space: O(d) - d: max depth < n (depth is bound by number of elements)
		public int DepthSum(IList<NestedInteger> nestedList)
		{
			int res = 0;
			foreach (var item in nestedList)
			{
				res += DepthSumHelper(item, 1);
			}
			return res;
		}

		private int DepthSumHelper(NestedInteger nestedInteger, int depth)
		{
			if (nestedInteger.IsInteger())
			{
				return nestedInteger.GetInteger() * depth;
			}

			int res = 0;
			foreach (NestedInteger item in nestedInteger.GetList())
			{
				res += DepthSumHelper(item, depth + 1);
			}
			return res;
		}

		// #133. Clone Graph
		// Time: O(n)
		// Space: O(n)
		public Node CloneGraph(Node node)
		{
			if (node == null) return null;

			Queue<Node> queue = new();
			Dictionary<Node, Node> mapping = new(); // map from original to copy
			CloneNode(node, mapping);

			// BFS
			queue.Enqueue(node);
			while (queue.Count > 0)
			{
				Node n = queue.Dequeue();
				Node clone = mapping[n];
				foreach (Node neighbor in n.neighbors)
				{
					if (mapping.ContainsKey(neighbor))
					{
						clone.neighbors.Add(mapping[neighbor]);
					}
					else
					{
						Node cloneNeighbor = CloneNode(neighbor, mapping);
						clone.neighbors.Add(cloneNeighbor);
						queue.Enqueue(neighbor);
					}
				}
			}

			return mapping[node];
		}

		private Node CloneNode(Node node, Dictionary<Node, Node> mapping)
		{
			Node clone = new Node(node.val, new List<Node>());
			mapping[node] = clone;
			return clone;
		}
	}
}
