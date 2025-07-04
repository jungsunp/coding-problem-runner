using CodingPractice.Leetcode.Trees.Custom;
using System;
using System.Collections.Generic;
using System.Text;
using Node = CodingPractice.Leetcode.Trees.Custom.Node;

namespace CodingPractice.Leetcode.Trees
{
	public class TreeTraversal
	{
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
	}
}
