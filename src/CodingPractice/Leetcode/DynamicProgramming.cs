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
				(n, m) = (m, n);
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

		// #746. Min Cost Climbing Stairs
		// Time: O(n)
		// Space: O(1)
		public int MinCostClimbingStairs(int[] cost) {
			for (int i = 2; i < cost.Length; i++) {
				cost[i] += Math.Min(cost[i - 2], cost[i - 1]);
			}
			return Math.Min(cost[cost.Length - 2], cost[cost.Length - 1]);
		}

		// #198. House Robber
		// Time: O(n)
		// Space: O(1)
		public int Rob(int[] nums) {
			if (nums.Length == 1) { return nums[0]; }

			if (nums.Length > 2) {
				nums[2] += nums[0]; // base case for at least 3 elements array
			}

			// Run DP - dynamically update with max amount
			for (int i = 3; i < nums.Length; i++) {
				nums[i] += Math.Max(nums[i - 3], nums[i - 2]);
			}

			return Math.Max(nums[nums.Length - 2], nums[nums.Length - 1]);
		}
	}
}
