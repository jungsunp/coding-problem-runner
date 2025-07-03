using CodingPractice.Leetcode.Trees.Custom;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodingPractice.Leetcode
{
	public class PostOrder
	{
		// #543. Diameter of Binary Tree
		// Time: O(n)
		// Space: O(h) - h: height of tree
		private int diameter = 0;
		public int DiameterOfBinaryTree(TreeNode root)
		{
			LongestPath(root);
			return diameter;
		}

		public int LongestPath(TreeNode node)
		{
			if (node == null) return -1;

			int leftPath = LongestPath(node.left);
			int rightPath = LongestPath(node.right);

			diameter = Math.Max(diameter, leftPath + rightPath + 2);

			return Math.Max(leftPath, rightPath) + 1; // + 1 for path between node & its child
		}
	}
}
