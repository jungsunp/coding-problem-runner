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

		// #437. Path Sum III
		// Time: O(n)
		// Space: O(log n)
		public int PathSum(TreeNode root, int targetSum) {
			if (root == null) {
				return 0;
			}

			Dictionary<long, int> hash = new Dictionary<long, int>(); // keeps track of partial sums in paths
			return PathSumDfs(root, targetSum, 0, hash);
		}

		private static int PathSumDfs(TreeNode node, int targetSum, long currSum, Dictionary<long, int> hash) {
			if (node == null) {
				return 0;
			}

			// Path all the way from the root
			int validPathCnt = 0;
			currSum += node.val;
			if (currSum == (long)targetSum) {
				validPathCnt++;
			}

			// Path in middle that match targetSum
			long diff = currSum - (long)targetSum;
			if (hash.ContainsKey(diff)) {
				validPathCnt += hash[diff];
			}

			// Store current sum into hash
			if (hash.ContainsKey(currSum)) {
				hash[currSum] += 1;
			}
			else {
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
		public bool LeafSimilar(TreeNode root1, TreeNode root2) {
			List<int> leafList1 = new List<int>();
			List<int> leafList2 = new List<int>();

			LeafDfs(root1, leafList1);
			LeafDfs(root2, leafList2);

			if (leafList1.Count != leafList2.Count) {
				return false;
			}

			for (int i = 0; i < leafList1.Count; i++) {
				if (leafList1[i] != leafList2[i]) {
					return false;
				}
			}

			return true;
		}


		private static void LeafDfs(TreeNode node, List<int> leafList) {
			if (node == null) {
				return;
			}

			if (node.left == null && node.right == null) {
				leafList.Add(node.val);
				return;
			}

			LeafDfs(node.left, leafList);
			LeafDfs(node.right, leafList);
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

		// #1372. Longest ZigZag Path in a Binary Tree (DFS)
		// Time: O(n) - node count
		// Space: O(log n) - for balanaced tree (O(n) worst)

		private int maxPath = 0;

		public int LongestZigZag(TreeNode root) {

			// Run DFS on each direction
			this.ZigZagDfs(root.left, true, 1);
			this.ZigZagDfs(root.right, false, 1);

			return this.maxPath;
		}

		private void ZigZagDfs(TreeNode node, bool movedLeft, int path) {
			if (node == null) {
				return;
			}

			this.maxPath = Math.Max(this.maxPath, path);

			if (movedLeft) {
				ZigZagDfs(node.right, false, path  + 1);
				ZigZagDfs(node.left, true, 1);
			}
			else { // moved right
				ZigZagDfs(node.left, true, path + 1);
				ZigZagDfs(node.right, false, 1);
			}
		}

		// #236. Lowest Common Ancestor of a Binary Tree (DFS)
		// Time: O(n)
		// Space: O(h)
		public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q) {
			if (root == null || root == p || root == q) {
				return root;
			}

			TreeNode left = LowestCommonAncestor(root.left, p, q);
			TreeNode right = LowestCommonAncestor(root.right, p, q);

			if (left != null && right != null) {
				return root;
			}

			return left != null ? left : right;
		}
	}
}
