using CodingPractice.Leetcode.Trees.Custom;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodingPractice.Leetcode
{
	public class LevelOrder
	{

		// #199. Binary Tree Right Side View (BFS)
		// Time: O(n)
		// Space: O(d) - d is tree diameter
		public IList<int> RightSideView(TreeNode root)
		{
			List<int> ret = new List<int>();
			if (root == null)
			{
				return ret;
			}

			Queue<TreeNode> queue = new Queue<TreeNode>();
			queue.Enqueue(root);

			// iterate every level of nodes in tree
			while (queue.Count > 0)
			{
				TreeNode node = null;
				int nodeCount = queue.Count;

				// iterate every node in current level
				for (int i = 0; i < nodeCount; i++)
				{
					node = queue.Dequeue();
					if (node.left != null)
					{
						queue.Enqueue(node.left);
					}
					if (node.right != null)
					{
						queue.Enqueue(node.right);
					}
				}

				ret.Add(node.val); // the node is right most node in current level
			}

			return ret;
		}

		// # 1161. Maximum Level Sum of a Binary Tree (BFS)
		// Time: O(n)
		// Space: O(n) - width of tree at last level ~ n/2
		public int MaxLevelSum(TreeNode root)
		{
			var queue = new Queue<TreeNode>();
			queue.Enqueue(root);

			int maxSum = Int32.MinValue;
			int maxLevel = 1;
			int level = 1;

			while (queue.Count > 0)
			{
				int nodeCount = queue.Count;
				int sum = 0; // calculate level sum
				for (int i = 0; i < nodeCount; i++)
				{
					TreeNode node = queue.Dequeue();
					sum += node.val;
					if (node.left != null)
					{
						queue.Enqueue(node.left);
					}
					if (node.right != null)
					{
						queue.Enqueue(node.right);
					}
				}

				if (sum > maxSum)
				{
					maxSum = sum;
					maxLevel = level;
				}

				level++;
			}

			return maxLevel;
		}

		// #314. Binary Tree Vertical Order Traversal
		// Time: O(n)
		// Space: O(n)
		public IList<IList<int>> VerticalOrder(TreeNode root)
		{
			List<IList<int>> res = new();
			if (root == null)
				return res;
			Queue<(TreeNode, int)> queue = new(); // node & column number
			Dictionary<int, List<int>> hash = new();
			int minCol = 0;
			int maxCol = 0;

			// Run BFS
			queue.Enqueue((root, 0));
			while (queue.Count > 0)
			{
				int cnt = queue.Count;
				for (int i = 0; i < cnt; i++)
				{
					(TreeNode node, int col) = queue.Dequeue();
					if (!hash.ContainsKey(col))
					{
						hash[col] = new List<int>();
					}
					hash[col].Add(node.val);
					minCol = Math.Min(minCol, col);
					maxCol = Math.Max(maxCol, col);

					if (node.left != null)
					{
						queue.Enqueue((node.left, col - 1));
					}
					if (node.right != null)
					{
						queue.Enqueue((node.right, col + 1));
					}
				}
			}

			// Iterate hash for each column
			for (int i = minCol; i <= maxCol; i++)
			{
				res.Add(hash[i]);
			}
			return res;
		}
	}
}
