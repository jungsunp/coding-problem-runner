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

		// #733. Flood Fill (BFS)
		// Time: O(m * n)
		// Space: O(m * n)
		// Note: can also be solved with DFS
		public int[][] FloodFill(int[][] image, int sr, int sc, int color)
		{

			// Check if target color is same with color from start index
			int origColor = image[sr][sc];
			if (color == origColor)
			{
				return image;
			}

			// Run BFS using queue
			Queue<(int, int)> bfsQueue = new();
			bfsQueue.Enqueue((sr, sc));
			while (bfsQueue.Count > 0)
			{
				(int row, int col) = bfsQueue.Dequeue();
				image[row][col] = color;

				if (row > 0 && image[row - 1][col] == origColor)
				{ // bottom
					bfsQueue.Enqueue((row - 1, col));
				}
				if (row < image.Length - 1 && image[row + 1][col] == origColor)
				{ // top
					bfsQueue.Enqueue((row + 1, col));
				}
				if (col > 0 && image[row][col - 1] == origColor)
				{ // left
					bfsQueue.Enqueue((row, col - 1));
				}
				if (col < image[0].Length - 1 && image[row][col + 1] == origColor)
				{ // right
					bfsQueue.Enqueue((row, col + 1));
				}
			}

			return image;
		}

		// #1091. Shortest Path in Binary Matrix
		// Time: O(N)
		// Space: O(N) - N: total number of cells i.e n^2
		// Note: A* has O(N log N) time but perform well in practice
		public int ShortestPathBinaryMatrix(int[][] grid)
		{
			int n = grid.Length;
			if (grid[0][0] != 0 || grid[n - 1][n - 1] != 0) return -1;

			int[][] directions = new int[][] {
			[0, -1], [1, -1], [1,0], [1, 1], [0, 1], [-1, 1], [-1, 0], [-1, -1]
		};

			// Run BFS
			Queue<(int, int)> queue = new();
			bool[,] visited = new bool[n, n]; // NOTE!! bool matrix is more efficient than hashset
			queue.Enqueue((0, 0));
			visited[0, 0] = true;
			int step = 0;
			while (queue.Count > 0)
			{
				int cnt = queue.Count;
				step++;
				for (int i = 0; i < cnt; i++)
				{
					(int x, int y) = queue.Dequeue();

					if (x == n - 1 && y == n - 1) return step; // reached end!

					foreach (int[] direction in directions)
					{
						if (IsInRange(x, y, direction, n))
						{
							int nextX = x + direction[0];
							int nextY = y + direction[1];
							if (grid[nextX][nextY] == 0 && !visited[nextX, nextY])
							{
								queue.Enqueue((nextX, nextY));
								visited[nextX, nextY] = true; // NOTE!! important to mark after enqueue to avoid duplicates & race conditions
							}
						}
					}
				}
			}

			return -1;
		}

		private bool IsInRange(int x, int y, int[] direction, int n)
		{
			x += direction[0];
			y += direction[1];
			if (x >= 0 && x < n && y >= 0 && y < n) return true;
			return false;
		}
	}
}
