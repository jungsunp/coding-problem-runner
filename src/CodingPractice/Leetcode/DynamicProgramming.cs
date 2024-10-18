using System;

namespace CodingPractice.Leetcode
{
	public class DynamicProgramming
	{
		// #1137. N-th Tribonacci Number
		// Time: O(n)
		// Space: O(1)
		public int Tribonacci(int n) {
			if (n == 0) { return 0; }

			int ret = 1; // T(n+3)
			int prev0 = 0; // T(n)
			int prev1  = 1; // T(n+1)
			int prev2 = 1; // T(n+2)

			for (int i = 3; i <= n; i++) {
				ret = prev0 + prev1 + prev2;

				prev0 = prev1;
				prev1 = prev2;
				prev2 = ret;
			}

			return ret;
		}
	}
}
