using System;
using System.Collections.Generic;

namespace CodingPractice.Leetcode
{
	public class Graph
	{
		// #841. Keys and Rooms
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

		// #547. Number of Provinces
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

		// #1926. Nearest Exit from Entrance in Maze
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
	}
}
