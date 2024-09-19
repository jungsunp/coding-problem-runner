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
	}
}
