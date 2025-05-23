using System;
using System.Collections.Generic;

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

			int[] dp = new int[n];
			Array.Fill(dp, 1);

			for (int i = 1; i < m ; i++) {
				for (int j = 1; j < n; j++) {
					dp[j] += dp[j - 1];
				}
			}

			return dp[n - 1];
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

		// #790. Domino and Tromino Tiling
		// Time: O(n)
		// Space: O(1)
		private const int MOD = 1000000007; // 10 ^ 9 + 7
		public int NumTilings(int n) {
			if (n < 3) { return n; }

			// f: full covered tiling & p: partially covered tiling
			long fn1 = 2; // base case for f(n-1)
			long fn2 = 1; // base case for f(n-2)
			long pn1 = 1; // base case for p(n-1)
			long fn = fn1;
			long pn = pn1;

			// Run DP with following formula
			// f(n) = f(n-1) + f(n-2) + 2 * p(n-1)
			// p(n) = p(n-1) + f(n-2)
			for (int i = 3; i <= n; i++) {
				fn = (fn1 + fn2 + 2 * pn1) % MOD;
				pn = (pn1 + fn2) % MOD;

				(fn1, fn2, pn1) = (fn, fn1, pn);
			}

			return (int) fn1;
		}

		 // #1143. Longest Common Subsequence (with memoization)
		// Time: O(m * n)
		// Space: O(m * n)
		private Dictionary<(int, int), int> memo = new Dictionary<(int, int), int>();
		public int LongestCommonSubsequenceWithMemo(string text1, string text2) {
			return LcsDpWithMemo(text1, text2, text1.Length - 1, text2.Length - 1);
		}

		private int LcsDpWithMemo(string text1, string text2, int index1, int index2) {
			if (index1 < 0 || index2 < 0) {
				return 0;
			}

			if (memo.ContainsKey((index1, index2))) {
				return memo[(index1, index2)];
			}

			int ret;
			if (text1[index1] == text2[index2]) {
				ret = 1 + LcsDpWithMemo(text1, text2, index1 - 1, index2 - 1);
			}
			else {
				ret = Math.Max(LcsDpWithMemo(text1, text2, index1 - 1, index2), LcsDpWithMemo(text1, text2, index1, index2 - 1));
			}

			memo.Add((index1, index2), ret);
			return ret;
		}

		// #1143. Longest Common Subsequence (DP optimized)
		// Time: O(m * n)
		// Space: Min(m, n)
		public int LongestCommonSubsequence(string text1, string text2) {
			if (text1.Length < text2.Length) {
				// ensure text2 is shorter string
				(text1, text2) = (text2, text1);
			}

			// Initially start with 2D array and optimize to 1D for space optimization
			// use shorter string for space optimization
			// add extra of zero for ease of implemenation
			int[] dp = new int[text2.Length + 1];

			// Run DP
			for (int i = text1.Length - 1; i >= 0; i--) {
				int prev = 0; // temp var to store (i + 1, j + 1) case
				for (int j = text2.Length - 1; j >= 0; j--) {
					if (text1[i] == text2[j]) {
						(dp[j], prev) = (1 + prev, dp[j]);
					}
					else {
						prev = dp[j];
						dp[j] = Math.Max(dp[j], dp[j + 1]);
					}
				}
			}

			return dp[0];
		}

		// #714. Best Time to Buy and Sell Stock with Transaction Fee
		// Time: O(n)
		// Space: O(n) - Note: you can further optimize to O(1) by replacing 2 arrays with variables
		public int MaxProfit(int[] prices, int fee) {
			int n = prices.Length;
			int[] hold = new int[n]; // max profit for owning stock at the end of kth day
			int[] free = new int[n]; // max profit for not-owning stock at the end of kth day

			// Run DP
			hold[0] = -prices[0]; // must purchase on day 0 for this conidtion
			for (int k = 1; k < n; k++) {
				hold[k] = Math.Max(hold[k - 1], free[k - 1] - prices[k]);
				free[k] = Math.Max(hold[k - 1] + prices[k] - fee, free[k - 1]);
			}

			return Math.Max(hold[n - 1], free[n - 1]);
		}


		// #72. Edit Distance (with Memo)
		// Time: O(m * n)
		// Space: O(m * n)
	 	Dictionary<(int, int), int> memoDp = new Dictionary<(int, int), int>();

		public int MinDistanceWithMemo(string word1, string word2) {
			return MinDistanceDp(word1, word2, 0, 0);
		}

		private int MinDistanceDp(string word1, string word2, int index1, int index2) {
			if (memoDp.ContainsKey((index1, index2))) {
				return memoDp[(index1, index2)];
			}

			if (index1 >= word1.Length || index2 >= word2.Length) {
				int ret = Math.Max(word1.Length - index1, word2.Length - index2);
				memoDp.Add((index1, index2), ret);
				return ret;
			}

			if (word1[index1] == word2[index2]) {
				int ret = MinDistanceDp(word1, word2, index1 + 1, index2 + 1); // same char => no step
				memoDp.Add((index1, index2), ret);
				return ret;
			}

			int repDistance = MinDistanceDp(word1, word2, index1 + 1, index2 + 1); // replace a char
			int addDistance = MinDistanceDp(word1, word2, index1, index2 + 1); // add a char
			int delDistance = MinDistanceDp(word1, word2, index1 + 1, index2); // delete a char

			int dist = Math.Min(Math.Min(repDistance, addDistance), delDistance) + 1;
			memoDp.Add((index1, index2), dist);
			return dist;
		}

		// #72. Edit Distance - DP Optimized
		// Time: O(m * n)
		// Space: O(Min(m, n))
		public int MinDistance(string word1, string word2) {
			if (word2.Length > word1.Length) {
				(word1, word2) = (word2, word1); // make sure word2 is shorter for space optimization
			}

			if (word2.Length == 0) {
				return word1.Length;
			}

			// set up base case
			int[] dp = new int[word2.Length + 1]; // number of step to insert chars
			for (int i = 0; i <= word2.Length; i++) {
				dp[i] = i;
			}

			// Run DP
			int[] current = new int[word2.Length + 1];
			for (int i = 1; i <= word1.Length; i++) {
				current[0] = i; // cost of deleting i characters to get empty string

				for (int j = 1; j <= word2.Length; j++) {
					int addSteps = current[j - 1] + 1;
					int delSteps = dp[j] + 1;
					int repSteps = word1[i - 1] == word2[j - 1] ? dp[j - 1] : dp[j - 1] + 1;
					current[j] = Math.Min(Math.Min(addSteps, delSteps), repSteps);
				}

				// Swap arrays for next iteration
				(dp, current) = (current, dp);
			}

			return dp[word2.Length];
		}
	}
}
