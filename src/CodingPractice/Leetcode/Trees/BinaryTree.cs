using CodingPractice.Leetcode.Trees.Custom;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace CodingPractice.Leetcode
{
	public class BinaryTree
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

		// #1597. Build Binary Expression Tree From Infix Expression
		// Time: O(n)
		// Space: O(n)
		public BinaryTreeNode<char> ExpTree(string s)
		{
			string postfixStr = ConvertToPostfix(s);

			// Build tree using postfix string
			Stack<BinaryTreeNode<char>> stack = new Stack<BinaryTreeNode<char>>();
			foreach (char c in postfixStr)
			{
				if (Char.IsNumber(c))
				{
					stack.Push(new BinaryTreeNode<char>(c));
				}
				else
				{ // operator
					BinaryTreeNode<char> node = new BinaryTreeNode<char>(c);
					node.Right = stack.Pop();
					node.Left = stack.Pop();
					stack.Push(node);
				}
			}

			return stack.Peek();
		}

		private string ConvertToPostfix(string s)
		{
			StringBuilder postfix = new StringBuilder();
			Stack<char> opStack = new Stack<char>();

			foreach (char c in s)
			{
				if (Char.IsNumber(c))
				{ // operands
					postfix.Append(c);
				}
				else if (c == '(')
				{
					opStack.Push(c);
				}
				else if (c == ')')
				{
					while (opStack.Count > 0 && opStack.Peek() != '(')
					{
						postfix.Append(opStack.Pop());
					}
					opStack.Pop(); // get rid of '(' from stack
				}
				else
				{ // operators
					while (opStack.Count > 0 && OperatorOrder(c) <= OperatorOrder(opStack.Peek()))
					{
						postfix.Append(opStack.Pop());
					}
					opStack.Push(c);
				}
			}

			while (opStack.Count > 0)
			{
				postfix.Append(opStack.Pop()); // put remaining operators
			}

			return postfix.ToString();
		}

		private static int OperatorOrder(char op)
		{
			switch (op)
			{
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

		public int LongestZigZag(TreeNode root)
		{

			// Run DFS on each direction
			this.ZigZagDfs(root.left, true, 1);
			this.ZigZagDfs(root.right, false, 1);

			return this.maxPath;
		}

		private void ZigZagDfs(TreeNode node, bool movedLeft, int path)
		{
			if (node == null)
			{
				return;
			}

			this.maxPath = Math.Max(this.maxPath, path);

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

		// #1650. Lowest Common Ancestor of a Binary Tree III
		// Time: O(h)
		// Space: O(h) - h: height of tree.
		// Note: h = O(log n) for balanced tree
		//  h = O(n) for unbalanced tree
		// Note: There is also 2 pointer solution with O(1) Space
		public Node LowestCommonAncestor(Node p, Node q)
		{
			HashSet<Node> hash = new(); // record parents of P

			// Iterate p => root and record parents
			while (p != null)
			{
				hash.Add(p);
				p = p.parent;
			}

			// Iterate q => root to check LCA
			while (q != null)
			{
				if (hash.Contains(q))
				{
					return q;
				}
				q = q.parent;
			}

			return null; // invalid
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
	}
}
