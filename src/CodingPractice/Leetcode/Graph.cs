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
	}
}
