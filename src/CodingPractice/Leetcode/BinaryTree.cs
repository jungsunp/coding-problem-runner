using System;
using System.Collections.Generic;
using System.Text;

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

		// #1448. Count Good Nodes in Binary Tree (DFS)
		// Time: O(n)
		// Space: O(log n) - worst O(n)
		public int GoodNodes(TreeNode root) {
			return 1 + Dfs(root.left, root.val) + Dfs(root.right, root.val);
		}

		private int Dfs(TreeNode node, int max) {
			if (node == null) { return 0; }

			int nodeIsGoodCount = 0;
			if (node.val >= max)
			{
				nodeIsGoodCount = 1;
				max = node.val;
			}

			return nodeIsGoodCount + Dfs(node.left, max) + Dfs(node.right, max);
		}

		// #199. Binary Tree Right Side View (BFS)
		// Time: O(n)
		// Space: O(d) - d is tree diameter
		public IList<int> RightSideView(TreeNode root) {
			List<int> ret = new List<int>();
			if (root == null) { return ret; }

			Queue<TreeNode> queue = new Queue<TreeNode>();
			queue.Enqueue(root);

			// iterate every level of nodes in tree
			while (queue.Count > 0)
			{
				TreeNode node = null;
				int nodeCount  = queue.Count;

				// iterate every node in current level
				for (int i = 0; i < nodeCount; i++)
				{
					node = queue.Dequeue();
					if (node.left != null) { queue.Enqueue(node.left); }
					if (node.right != null) { queue.Enqueue(node.right); }
				}

				ret.Add(node.val); // the node is right most node in current level
			}

			return ret;
		}

		// #700. Search in a Binary Search Tree
		// Time: O(h) - height of BST (i.e O(log n) in average, O(n) worst)
		// Space: O(h)
		public TreeNode SearchBST(TreeNode root, int val) {
			if (root == null) { return null; }
			if (root.val == val) { return root; }
			else if (root.val > val) { return SearchBST(root.left, val); }
			else { return SearchBST(root.right, val); }
		}

		// #1597. Build Binary Expression Tree From Infix Expression
		// Time: O(n)
		// Space: O(n)
		public BinaryTreeNode<char> ExpTree(string s) {
			string postfixStr = ConvertToPostfix(s);

			// Build tree using postfix string
			Stack<BinaryTreeNode<char>> stack = new Stack<BinaryTreeNode<char>>();
			foreach (char c in postfixStr) {
				if (Char.IsNumber(c)) {
					stack.Push(new BinaryTreeNode<char>(c));
				} else { // operator
					BinaryTreeNode<char> node = new BinaryTreeNode<char>(c);
					node.Right = stack.Pop();
					node.Left = stack.Pop();
					stack.Push(node);
				}
			}

			return stack.Peek();
		}

		private string ConvertToPostfix(string s){
			StringBuilder postfix = new StringBuilder();
			Stack<char> opStack = new Stack<char>();

			foreach (char c in s) {
				if (Char.IsNumber(c)) { // operands
					postfix.Append(c);
				}
				else if (c == '(') {
					opStack.Push(c);
				}
				else if (c == ')') {
					while (opStack.Count > 0 && opStack.Peek() != '(') {
						postfix.Append(opStack.Pop());
					}
					opStack.Pop(); // get rid of '(' from stack
				}
				else { // operators
					while (opStack.Count > 0 && OperatorOrder(c) <= OperatorOrder(opStack.Peek())) {
						postfix.Append(opStack.Pop());
					}
					opStack.Push(c);
				}
			}

			while (opStack.Count > 0) {
				postfix.Append(opStack.Pop()); // put remaining operators
			}

			return postfix.ToString();
		}

		private static int OperatorOrder(char op) {
			switch (op) {
				case '*':
				case '/':
					return 2;
				case '+':
				case '-':
					return 1;
				default:
					return -1;
			}
		}
	}
}
