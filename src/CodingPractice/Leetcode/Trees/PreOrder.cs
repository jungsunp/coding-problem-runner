using CodingPractice.Leetcode.Trees.Custom;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodingPractice.Leetcode
{
	public class PreOrder
	{
		// 104. Maximum Depth of Binary Tree (DFS)
		// Time: O(n)
		// Space: O(log n) - worst O(n) for completely unbalanced tree
		public int MaxDepth(TreeNode root)
		{
			if (root == null)
			{
				return 0;
			}
			return 1 + Math.Max(MaxDepth(root.left), MaxDepth(root.right));
		}

		// #1448. Count Good Nodes in Binary Tree (DFS)
		// Time: O(n)
		// Space: O(log n) - worst O(n)
		public int GoodNodes(TreeNode root)
		{
			return 1 + Dfs(root.left, root.val) + Dfs(root.right, root.val);
		}

		private int Dfs(TreeNode node, int max)
		{
			if (node == null)
			{
				return 0;
			}

			int nodeIsGoodCount = 0;
			if (node.val >= max)
			{
				nodeIsGoodCount = 1;
				max = node.val;
			}

			return nodeIsGoodCount + Dfs(node.left, max) + Dfs(node.right, max);
		}

		// #437. Path Sum III
		// Time: O(n)
		// Space: O(log n)
		public int PathSum(TreeNode root, int targetSum)
		{
			if (root == null)
			{
				return 0;
			}

			Dictionary<long, int> hash = new Dictionary<long, int>(); // keeps track of partial sums in paths
			return PathSumDfs(root, targetSum, 0, hash);
		}

		private static int PathSumDfs(TreeNode node, int targetSum, long currSum, Dictionary<long, int> hash)
		{
			if (node == null)
			{
				return 0;
			}

			// Path all the way from the root
			int validPathCnt = 0;
			currSum += node.val;
			if (currSum == (long)targetSum)
			{
				validPathCnt++;
			}

			// Path in middle that match targetSum
			long diff = currSum - (long)targetSum;
			if (hash.ContainsKey(diff))
			{
				validPathCnt += hash[diff];
			}

			// Store current sum into hash
			if (hash.ContainsKey(currSum))
			{
				hash[currSum] += 1;
			}
			else
			{
				hash.Add(currSum, 1);
			}

			// Calculate valid paths using recursion
			int ret = validPathCnt + PathSumDfs(node.left, targetSum, currSum, hash) + PathSumDfs(node.right, targetSum, currSum, hash);

			// Clean up hash after checking the node
			hash[currSum] -= 1;

			return ret;
		}

		// #872. Leaf-Similar Trees (DFS)
		// Time: O(n + m)
		// Space: O(n + m)
		public bool LeafSimilar(TreeNode root1, TreeNode root2)
		{
			List<int> leafList1 = new List<int>();
			List<int> leafList2 = new List<int>();

			LeafDfs(root1, leafList1);
			LeafDfs(root2, leafList2);

			if (leafList1.Count != leafList2.Count)
			{
				return false;
			}

			for (int i = 0; i < leafList1.Count; i++)
			{
				if (leafList1[i] != leafList2[i])
				{
					return false;
				}
			}

			return true;
		}

		private static void LeafDfs(TreeNode node, List<int> leafList)
		{
			if (node == null)
			{
				return;
			}

			if (node.left == null && node.right == null)
			{
				leafList.Add(node.val);
				return;
			}

			LeafDfs(node.left, leafList);
			LeafDfs(node.right, leafList);
		}

		// #236. Lowest Common Ancestor of a Binary Tree (DFS)
		// Time: O(n)
		// Space: O(h)
		public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
		{
			if (root == null || root == p || root == q)
			{
				return root;
			}

			TreeNode left = LowestCommonAncestor(root.left, p, q);
			TreeNode right = LowestCommonAncestor(root.right, p, q);

			if (left != null && right != null)
			{
				return root;
			}

			return left != null ? left : right;
		}

		// # 129. Sum Root to Leaf Numbers
		// Time: O(n)
		// Space: O(h) - h: heigh of tree
		// Note: There is also O(1) space solution using Morris Algorithm
		public int SumNumbers(TreeNode root)
		{
			return SumNumPreorder(root, 0);
		}

		private int SumNumPreorder(TreeNode node, int sum)
		{
			if (node == null)
			{
				return 0;
			}

			sum = sum * 10 + node.val;
			if (node.left == null && node.right == null)
			{ // leaf node
				return sum;
			}

			return SumNumPreorder(node.left, sum) + SumNumPreorder(node.right, sum);
		}

		// #1372. Longest ZigZag Path in a Binary Tree (DFS)
		// Time: O(n) - node count
		// Space: O(h)
		private int maxPath = 0;

		public int LongestZigZag(TreeNode root)
		{
			// Run DFS on each direction
			ZigZagDfs(root.left, true, 1);
			ZigZagDfs(root.right, false, 1);

			return maxPath;
		}

		private void ZigZagDfs(TreeNode node, bool movedLeft, int path)
		{
			if (node == null)
			{
				return;
			}

			maxPath = Math.Max(maxPath, path);

			if (movedLeft)
			{
				ZigZagDfs(node.right, false, path + 1);
				ZigZagDfs(node.left, true, 1);
			}
			else
			{ // moved right
				ZigZagDfs(node.left, true, path + 1);
				ZigZagDfs(node.right, false, 1);
			}
		}
	}
}
