using System;
using System.Collections.Generic;

namespace CodingPractice.Leetcode
{
	public class Graph
	{
		// #841. Keys and Rooms (BFS)
		// Time: O(n + e) - e is total number of keys
		// Space: O(n)
		public bool CanVisitAllRooms(IList<IList<int>> rooms) {
			Queue<int> roomsToVisit = new Queue<int>();
			HashSet<int> visitedRooms = new HashSet<int>();

			// Start with keys from room 0
			roomsToVisit.Enqueue(0);
			visitedRooms.Add(0);

			while (roomsToVisit.Count > 0) // run BFS
			{
				int room = roomsToVisit.Dequeue();
				foreach(int key in rooms[room])
				{
					if (!visitedRooms.Contains(key))
					{
						visitedRooms.Add(key);
						roomsToVisit.Enqueue(key);
					}
				}
			}

			return visitedRooms.Count == rooms.Count;
		}

		// #547. Number of Provinces (DFS)
		// Time: O(n^2)
		// Space: O(n)
		public int FindCircleNum(int[][] isConnected) {
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

		// #1926. Nearest Exit from Entrance in Maze (BFS)
		// Time: O(m * n)
		// Space: O(m * n)
		public int NearestExit(char[][] maze, int[] entrance) {
			int rowCount = maze.Length;
			int colCount = maze[0].Length;

			Queue<int[]> toVisit = new Queue<int[]>();
			toVisit.Enqueue([entrance[0], entrance[1], 0]); // coodrinates & number of steps

			while(toVisit.Count > 0) // Run BFS
			{
				int[] current = toVisit.Dequeue();
				int row = current[0];
				int col = current[1];
				int steps = current[2];

				if (row < 0 || col < 0 || row >= rowCount || col >= colCount || maze[row][col] == '+')
				{
					continue;
				}

				// check if exit
				if ((row == 0 || col == 0 || row == rowCount - 1  || col == colCount - 1) && steps > 0)
				{
					return steps;
				}

				maze[row][col] = '+'; // mark as visited (replace in place to reduce additional space)

				toVisit.Enqueue([row - 1, col, steps + 1]); // move top
				toVisit.Enqueue([row, col - 1, steps + 1]); // move left
				toVisit.Enqueue([row + 1, col, steps + 1]); // move bottom
				toVisit.Enqueue([row, col + 1, steps + 1]); // move right
			}

			return -1;
		}

		//# 1466. Reorder Routes to Make All Paths Lead to the City Zero (DFS)
		// Time: O(n)
		// Space: O(n)
		public int MinReorder(int n, int[][] connections) {

			// Build hash map with connections
			Dictionary<int, List<ConnectedCity>> map = new Dictionary<int, List<ConnectedCity>>();
			foreach(int[] connection in connections)
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
						if (cc.origDir) { // if reached with original direction, need to reverse it
							reorder++;
						}
					}
				}
			}

			return reorder;
		}

		private readonly struct ConnectedCity {
			public readonly int city;
			public readonly bool origDir; // original direction (i.e no need to reorder)

			public ConnectedCity(int city, bool origDir) {
				this.city = city;
				this.origDir = origDir;
			}
		}

		// #399. Evaluate Division
		// Time: O(m * n) - m: number of queries, n: number of equations
		// Space: O(n)
		public double[] CalcEquation(IList<IList<string>> equations, double[] values, IList<IList<string>> queries) {

			// Build hash map with graph edges
			Dictionary<string, IList<Edge>> map = new Dictionary<string, IList<Edge>>();
			for(int i = 0; i < equations.Count; i++) {
				string numerator = equations[i][0];
				string denominator = equations[i][1];

				// Add original direction edge
				if (!map.ContainsKey(numerator)) {
					map.Add(numerator, new List<Edge>());
				}
				map[numerator].Add(new Edge(denominator, values[i]));

				// Add reverse direction edge
				if (!map.ContainsKey(denominator)) {
					map.Add(denominator, new List<Edge>());
				}
				map[denominator].Add(new Edge(numerator, 1 / values[i])); // add edge weight as 1 / value
			}

			// Calculate queries
			double[] ret = new double[queries.Count];
			for (int i = 0; i < queries.Count; i++) {
				string numerator = queries[i][0];
				string denominator = queries[i][1];
				ret[i] = -1; // default - unable to calculate

				if (!map.ContainsKey(numerator)) {
					continue;
				}
				else if (string.Equals(numerator, denominator)) {
					ret[i] = 1;
					continue;
				}

				// Run DFS (Note: this can also be done with backtracking)
				Stack<Edge> edgeStack = new Stack<Edge>();
				HashSet<string> visited = new HashSet<string>();
				edgeStack.Push(new Edge(numerator, 1));
				while (edgeStack.Count > 0) {
					Edge edge = edgeStack.Pop();
					string variable = edge.denominator;
					visited.Add(variable);

					IList<Edge> edgeList = map[variable];
					foreach (Edge nextEdge in edgeList) {
						if (!visited.Contains(nextEdge.denominator)) {
							if (nextEdge.denominator == denominator) {
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

		private class Edge {
			public string denominator;
			public double value;

			public Edge (string denominator, double value) {
				this.denominator = denominator;
				this.value = value;
			}
		}

		// #994. Rotting Oranges
		// Time: O(m * n)
		// Space: O(m * n)
		public int OrangesRotting(int[][] grid) {
			// Find rotten Oranges
			Queue<int[]> rottenQueue = new Queue<int[]>();
			int freshCnt = 0;
			for (int i = 0; i < grid.Length; i++) {
				for (int j = 0; j < grid[0].Length; j++) {
					if (grid[i][j] == 2) {
						rottenQueue.Enqueue([i, j]);
					} else if (grid[i][j] == 1) {
						freshCnt++;
					}
				}
			}

			if (freshCnt == 0) { // no fresh orange
				return 0;
			}

			// Run BFS with rotten Oranges
			int numMins = -1;
			while (rottenQueue.Count > 0) {
				int rottenCnt = rottenQueue.Count;
				for (int i = 0; i < rottenCnt; i++) {
					int[] rottenOrange = rottenQueue.Dequeue();
					int row = rottenOrange[0];
					int col = rottenOrange[1];

					if (row < grid.Length - 1 && grid[row + 1][col] == 1) {
						grid[row + 1][col] = 2;
						rottenQueue.Enqueue([row + 1, col]);
						freshCnt--;
					}
					if (row > 0 && grid[row - 1][col] == 1) {
						grid[row - 1][col] = 2;
						rottenQueue.Enqueue([row - 1, col]);
						freshCnt--;
					}
					if (col < grid[0].Length - 1 && grid[row][col + 1] == 1) {
						grid[row][col + 1] = 2;
						rottenQueue.Enqueue([row, col + 1]);
						freshCnt--;
					}
					if (col > 0 && grid[row][col - 1] == 1) {
						grid[row][col - 1] = 2;
						rottenQueue.Enqueue([row, col - 1]);
						freshCnt--;
					}
				}
				numMins++;
			}

			if (freshCnt > 0) { // at least 1 orage remained fresh after BFS
				return -1;
			}

			return numMins;
		}

		// #200. Number of Islands
		// Time: O(m * n)
		// Space: O(m * n)
		public int NumIslands(char[][] grid) {
			int numIslands = 0;

			for (int i = 0; i < grid.Length; i++) {
				for (int j = 0; j < grid[i].Length; j++) {

					if (grid[i][j] != '1') { continue; }

					numIslands++; // new island found!

					// Run DFS
					this.IslandDfs(grid, i, j);
				}
			}

			return numIslands;
		}

		private void IslandDfs(char[][] grid, int i, int j) {
			Stack<(int, int)> stack = new Stack<(int, int)>();
			stack.Push((i, j));

			while (stack.Count > 0) {
				(int row, int col) = stack.Pop();

				grid[row][col] = 'v'; // mark visited

				// Move down
				if (row < grid.Length - 1 && grid[row + 1][col] == '1') {
					stack.Push((row + 1, col));
				}

				// Move up
				if (row > 0 && grid[row - 1][col] == '1') {
					stack.Push((row - 1, col));
				}

				// Move right
				if (col < grid[i].Length - 1 && grid[row][col + 1] == '1') {
					stack.Push((row, col + 1));
				}

				// Move left
				if (col > 0 && grid[row][col - 1] == '1') {
					stack.Push((row, col - 1));
				}
			}
		}

		// #210. Course Schedule II
		// Time: O(n + m) - n: number of courses, m: number of prerequisites
		// Space: O(n + m)
		public int[] FindOrder(int numCourses, int[][] prerequisites) {
			// Build prereq map
			Dictionary<int, List<int>> map = new Dictionary<int, List<int>>();
			foreach (int[] prereq in prerequisites) {
				if (!map.ContainsKey(prereq[0])) {
					map.Add(prereq[0], new List<int>());
				}
				map[prereq[0]].Add(prereq[1]);
			}

			// Run Topological Sort via DFS
			Stack<int> sortStack = new Stack<int>();
			int[] courseStatus = new int[numCourses]; // 0 = not taken, 1 = taking, 2 = taken
			for (int i = 0; i < numCourses; i++) {
				if (courseStatus[i] == 0) {
					if (!this.topologicalSortDfs(i, map, sortStack, courseStatus)) {
						return []; // cylce detected from DFS
					}
				}
			}

			// Topological Sorting Success
			int[] ret = new int[numCourses];
			for (int i = numCourses - 1; i >= 0; i--) {
				ret[i] = sortStack.Pop();
			}
			return ret;
		}

		private bool topologicalSortDfs(
			int course,
			Dictionary<int, List<int>> map,
			Stack<int> sortStack,
			int[] courseStatus
		) {
			courseStatus[course] = 1; // taking

			if (map.ContainsKey(course)) {
				foreach (int prereq in map[course]) {
					if (courseStatus[prereq] == 0) {
						if (!topologicalSortDfs(prereq, map, sortStack, courseStatus)) {
							return false; // cycle detected
						}
					}
					else if (courseStatus[prereq] == 1) {
						// cycle detected
						return false;
					}
				}
			}

			courseStatus[course] = 2; // taken
			sortStack.Push(course);
			return true;
		}
	}
}
