using CodingPractice.Leetcode.Trees.Custom;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodingPractice.Leetcode.Trees
{
	public class BinarySearchTree
	{
		// #700. Search in a Binary Search Tree
		// Time: O(h) - height of BST (i.e O(log n) in average, O(n) worst)
		// Space: O(h)
		public TreeNode SearchBST(TreeNode root, int val)
		{
			if (root == null)
			{
				return null;
			}
			if (root.val == val)
			{
				return root;
			}
			else if (root.val > val)
			{
				return SearchBST(root.left, val);
			}
			else
			{
				return SearchBST(root.right, val);
			}
		}

		// #450. Delete Node in a BST
		// Time: O(log n)
		// Space: O(1)
		public TreeNode DeleteNode(TreeNode root, int key)
		{
			if (root == null)
			{
				return null;
			}

			if (root.val == key)
			{
				return DeleteNodeHelper(root);
			}

			var node = root;
			while (node != null)
			{
				if (node.val > key)
				{
					if (node.left != null && node.left.val == key)
					{
						node.left = DeleteNodeHelper(node.left);
						break;
					}
					node = node.left;
				}
				else
				{
					if (node.right != null && node.right.val == key)
					{
						node.right = DeleteNodeHelper(node.right);
						break;
					}
					node = node.right;
				}
			}

			return root;
		}

		private static TreeNode DeleteNodeHelper(TreeNode node)
		{
			if (node.left == null)
			{
				return node.right;
			}
			else if (node.right == null)
			{
				return node.left;
			}
			else
			{
				// find right most node from left subtree and attach right subtree
				var tmp = node.left;
				while (tmp.right != null)
				{
					tmp = tmp.right;
				}
				tmp.right = node.right;
				return node.left;
			}
		}

		// #938. Range Sum of BST
		// Time: O(log n)
		// Space: O(h)
		// Note: easy
		public int RangeSumBST(TreeNode root, int low, int high)
		{
			if (root == null) return 0;
			if (root.val < low)
			{
				return RangeSumBST(root.right, low, high);
			}
			else if (root.val > high)
			{
				return RangeSumBST(root.left, low, high);
			}
			else
			{ // low <= root.val <= high
				return root.val + RangeSumBST(root.left, low, high) + RangeSumBST(root.right, low, high);
			}
		}

		// #270. Closest Binary Search Tree Value
		// Time: O(h)
		// Space: O(1) - h: height of tree
		public int ClosestValue(TreeNode root, double target)
		{
			int res = root.val;

			while (root != null)
			{
				if (root.val == target) return root.val; // can't get any closer

				if (Math.Abs(root.val - target) < Math.Abs(res - target))
				{
					res = root.val;
				}
				else if (Math.Abs(root.val - target) == Math.Abs(res - target) && root.val < res)
				{
					// return smallest one possible
					res = root.val;
				}

				if (root.val > target)
				{
					root = root.left;
				}
				else
				{
					root = root.right;
				}
			}

			return res;
		}
	}
}
