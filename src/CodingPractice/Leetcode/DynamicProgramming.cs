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

		// #62. Unique Paths
		// Time: O(m * n)
		// Space: O(n)
		public int UniquePaths(int m, int n) {
			if (m == 1 || n == 1) { return 1; }

			if (m < n) { // ensure n <= m
				int tmp = m;
				m = n;
				n = tmp;
			}

			int[] memo = new int[n];
			Array.Fill(memo, 1);

			for (int i = 1; i < m ; i++) {
				for (int j = 1; j < n; j++) {
					memo[j] += memo[j - 1];
				}
			}

			return memo[n - 1];
		}
	}
}
