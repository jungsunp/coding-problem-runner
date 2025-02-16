using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CodingPractice.Leetcode.BinaryTree;

namespace CodingPractice.Leetcode
{
	public class BinarySearch
	{
		// #700. Search in a Binary Search Tree
		// Time: O(h) - height of BST (i.e O(log n) in average, O(n) worst)
		// Space: O(h)
		public TreeNode SearchBST(TreeNode root, int val) {
			if (root == null) { return null; }
			if (root.val == val) { return root; }
			else if (root.val > val) { return SearchBST(root.left, val); }
			else { return SearchBST(root.right, val); }
		}

		// #374. Guess Number Higher or Lower
		// Time: O(log n)
		// Space: O(1)
		public int GuessNumber(int n) {
			int left = 1;
			int right = n;

			while (left <= right)
			{
				// int nextGuess = (int)(((long) left + right) / 2); Watch out for int overflow
				int nextGuess = left + (right - left) / 2;
				switch(guess(nextGuess))
				{
					case -1:
						right = nextGuess - 1;
						break;
					case 1:
						left = nextGuess + 1;
						break;
					default: // 0
						return nextGuess;
				}
			}

			throw new Exception("Number not found!");
		}

		private int guess(int guess) {
			throw new NotImplementedException(); // implemented from leetcode.com only
		}

		// #2300. Successful Pairs of Spells and Potions
		// Time: O((n + m) log m)
		// Space: O(log m) - space complexity for sorting
		public int[] SuccessfulPairs(int[] spells, int[] potions, long success) {
			int[] pairs = new int[spells.Length];

			// Sort potions (m log m)
			Array.Sort(potions);

			for (int i = 0; i < spells.Length; i++) {

				// Binary Search of each spell
				int left = 0;
				int right = potions.Length - 1;
				int mid = 0;
				bool found = false; // finding lower bound

				while (left <= right) {
					mid = (left + right) / 2;

					if ((long) spells[i] * potions[mid] >= success) {
						if (mid == 0 || (long) spells[i] * potions[mid - 1] < success) {
							found = true;
							break;
						}
						right = mid - 1;
					}
					else {
						left = mid + 1;
					}
				}

				pairs[i] = found ? potions.Length - mid : 0;
			}

			return pairs;
		}

		// #450. Delete Node in a BST
		// Time: O(log n)
		// Space: O(1)
		public TreeNode DeleteNode(TreeNode root, int key) {
			if (root == null) {
				return null;
			}

			if (root.val == key) {
				return DeleteNodeHelper(root);
			}

			var node = root;
			while (node != null) {
				if (node.val > key) {
					if (node.left != null && node.left.val == key) {
						node.left = DeleteNodeHelper(node.left);
						break;
					}
					node = node.left;
				}
				else {
					if (node.right != null && node.right.val == key) {
						node.right = DeleteNodeHelper(node.right);
						break;
					}
					node = node.right;
				}
			}

			return root;
		}

		private static TreeNode DeleteNodeHelper(TreeNode node) {
				if (node.left == null) {
					return node.right;
				}
				else if (node.right == null) {
					return node.left;
				}
				else {
					// find right most node from left subtree and attach right subtree
					var tmp = node.left;
					while (tmp.right != null) {
						tmp = tmp.right;
					}
					tmp.right = node.right;
					return node.left;
				}
		}

		// #162. Find Peak Element
		// Time: O(log n)
		// Space: O(1)
		public int FindPeakElement(int[] nums) {
			int left = 0;
			int right = nums.Length - 1;
			int mid;

			while (left < right) {
				mid = mid = left + (right - left) / 2;
				if (nums[mid] > nums[mid + 1]) {
					// Mid is in desc sequence => Peak is guranteed on left subtree
					right = mid;
				}
				else {
					// Mid is is asc desquence => Peak is guranteed on right subtree
					left = mid + 1;
				}
			}

			return left;
		}
	}
}
