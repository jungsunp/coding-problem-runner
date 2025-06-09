using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingPractice.Leetcode.Graphs
{
	public class Graph_BFS
	{
		// #841. Keys and Rooms (BFS)
		// Time: O(n + e) - e is total number of keys
		// Space: O(n)
		public bool CanVisitAllRooms(IList<IList<int>> rooms)
		{
			Queue<int> roomsToVisit = new Queue<int>();
			HashSet<int> visitedRooms = new HashSet<int>();

			// Start with keys from room 0
			roomsToVisit.Enqueue(0);
			visitedRooms.Add(0);

			while (roomsToVisit.Count > 0) // run BFS
			{
				int room = roomsToVisit.Dequeue();
				foreach (int key in rooms[room])
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

		// #1926. Nearest Exit from Entrance in Maze (BFS)
		// Time: O(m * n)
		// Space: O(m * n)
		public int NearestExit(char[][] maze, int[] entrance)
		{
			int rowCount = maze.Length;
			int colCount = maze[0].Length;

			Queue<int[]> toVisit = new Queue<int[]>();
			toVisit.Enqueue([entrance[0], entrance[1], 0]); // coodrinates & number of steps

			while (toVisit.Count > 0) // Run BFS
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
				if ((row == 0 || col == 0 || row == rowCount - 1 || col == colCount - 1) && steps > 0)
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

		// #994. Rotting Oranges (BFS)
		// Time: O(m * n)
		// Space: O(m * n)
		public int OrangesRotting(int[][] grid)
		{
			// Find rotten Oranges
			Queue<int[]> rottenQueue = new Queue<int[]>();
			int freshCnt = 0;
			for (int i = 0; i < grid.Length; i++)
			{
				for (int j = 0; j < grid[0].Length; j++)
				{
					if (grid[i][j] == 2)
					{
						rottenQueue.Enqueue([i, j]);
					}
					else if (grid[i][j] == 1)
					{
						freshCnt++;
					}
				}
			}

			if (freshCnt == 0)
			{ // no fresh orange
				return 0;
			}

			// Run BFS with rotten Oranges
			int numMins = -1;
			while (rottenQueue.Count > 0)
			{
				int rottenCnt = rottenQueue.Count;
				for (int i = 0; i < rottenCnt; i++)
				{
					int[] rottenOrange = rottenQueue.Dequeue();
					int row = rottenOrange[0];
					int col = rottenOrange[1];

					if (row < grid.Length - 1 && grid[row + 1][col] == 1)
					{
						grid[row + 1][col] = 2;
						rottenQueue.Enqueue([row + 1, col]);
						freshCnt--;
					}
					if (row > 0 && grid[row - 1][col] == 1)
					{
						grid[row - 1][col] = 2;
						rottenQueue.Enqueue([row - 1, col]);
						freshCnt--;
					}
					if (col < grid[0].Length - 1 && grid[row][col + 1] == 1)
					{
						grid[row][col + 1] = 2;
						rottenQueue.Enqueue([row, col + 1]);
						freshCnt--;
					}
					if (col > 0 && grid[row][col - 1] == 1)
					{
						grid[row][col - 1] = 2;
						rottenQueue.Enqueue([row, col - 1]);
						freshCnt--;
					}
				}
				numMins++;
			}

			if (freshCnt > 0)
			{ // at least 1 orage remained fresh after BFS
				return -1;
			}

			return numMins;
		}
	}
}
