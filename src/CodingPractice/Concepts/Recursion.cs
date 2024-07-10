using System;
using System.Collections.Generic;
using System.Text;

namespace CodingPractice.Concepts
{
	public static class Recursion
	{
		// #8.1
		// Time: O(n)
		// Space: O(n) (Call stack: O(n))
		public static int TripleStep(int n)
		{
			return TripleStep(n, new int[n + 1]);
		}

		private static int TripleStep(int n, int[] memo)
		{
			if (n == 0) { return 0;}
			if (n == 1) { return 1;}
			if (n == 2) { return 2;}
			if (n == 3) { return 4;}

			if (memo[n] == 0)
			{
				memo[n] = TripleStep(n - 1, memo) + TripleStep(n - 2, memo) + TripleStep(n - 3, memo);
			}

			return memo[n];
		}
	}
}
