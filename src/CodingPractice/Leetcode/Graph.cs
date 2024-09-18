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

			while (roomsToVisit.Count > 0)
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
	}
}
