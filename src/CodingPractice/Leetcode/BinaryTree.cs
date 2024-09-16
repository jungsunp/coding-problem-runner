using System;

namespace CodingPractice.Leetcode
{
	public class BinaryTree
	{
		/*
		* Definition for a binary tree node.
		*/
		public class TreeNode {
			public int val;
			public TreeNode left;
			public TreeNode right;
			public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
				this.val = val;
				this.left = left;
				this.right = right;
			}
		}

		// 104. Maximum Depth of Binary Tree (DFS)
		// Time: O(n)
		// Space: O(log n) - worst O(n) for completely unbalanced tree
		public int MaxDepth(TreeNode root) {
			if (root == null) { return 0; }
			return 1 + Math.Max(MaxDepth(root.left), MaxDepth(root.right));
		}
	}
}
