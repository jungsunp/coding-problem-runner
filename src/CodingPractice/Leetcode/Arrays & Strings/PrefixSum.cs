using System;
using System.Collections.Generic;

namespace CodingPractice.Leetcode
{
	public class PrefixSum
	{

		// #1732. Find the Highest Altitude
		// Time: O(n)
		// Space: O(1)
		public int LargestAltitude(int[] gain)
		{
			int height = 0;
			int max = 0;

			foreach (int diff in gain)
			{
				height += diff;
				max = Math.Max(height, max);
			}

			return max;
		}

		// #724. Find Pivot Index
		// Time: O(n)
		// Space: O(1)
		// Note: LinQ's Array.Sum (i.e nums.Sum) is slow
		public int PivotIndex(int[] nums)
		{
			int leftSum = 0;
			int sum = 0;

			foreach (int num in nums)
			{
				sum += num;
			}

			for (int i = 0; i < nums.Length; i++)
			{
				if (leftSum == sum - leftSum - nums[i])
				{
					return i;
				}

				leftSum += nums[i];
			}

			return -1;
		}

		// #238. Product of Array Except Self
		// Time: O(n)
		// Space: O(1)
		public int[] ProductExceptSelf(int[] nums)
		{
			int[] ret = new int[nums.Length];
			ret[nums.Length - 1] = 1;

			// calculate postfix products and store into ret to save memory
			for (int i = nums.Length - 2; i >= 0; i--)
			{
				ret[i] = nums[i + 1] * ret[i + 1];
			}

			// calculate prefix product and store into a variable
			int prefix = 1;
			for (int i = 0; i < nums.Length; i++)
			{
				ret[i] = prefix * ret[i]; // prefix * suffix
				prefix *= nums[i];
			}

			return ret;
		}

		// #560. Subarray Sum Equals K
		// Time: O(n)
		// Space: O(n)
		public int SubarraySum(int[] nums, int k)
		{
			Dictionary<int, int> hash = new(); // stores prefix sum & its count
			hash[0] = 1; // TRICK: handle subarray from 0

			// Iterate prefix sum array and count
			int prefixSum = 0;
			int res = 0;
			foreach (int num in nums)
			{
				prefixSum += num;

				// check if (prefixSum - k) exists in hash
				if (hash.ContainsKey(prefixSum - k))
				{
					res += hash[prefixSum - k];
				}

				if (!hash.ContainsKey(prefixSum))
				{
					hash[prefixSum] = 0;
				}
				hash[prefixSum]++;
			}

			return res;
		}

		// #528. Random Pick with Weight
		// Time: O(n)
		// Space: O(n)
		private Random rnd;
		private int[] prefixSums;
		private int totSum;
		public void Solution(int[] w) // Note: constructor. void is only added as workaround for now
		{
			rnd = new();
			prefixSums = new int[w.Length];
			int current = 0;
			for (int i = 0; i < w.Length; i++)
			{
				current += w[i];
				prefixSums[i] = current;
			}
			totSum = current;
		}

		// Time: O(log n)
		// Space: O(1)
		public int PickIndex()
		{
			int target = rnd.Next(1, totSum + 1); // Tricky! - all weights are positive.

			// Binary Search
			int left = 0;
			int right = prefixSums.Length - 1;
			while (left < right)
			{
				int mid = left + (right - left) / 2;
				if (target > prefixSums[mid])
				{
					left = mid + 1;
				}
				else
				{
					right = mid;
				}
			}

			return left;
		}
	}

	// 303. Range Sum Query - Immutable
	public class NumArray
	{
		private int[] sumArr;

		// Time: O(n)
		// Space: O(n)
		public NumArray(int[] nums)
		{
			this.sumArr = new int[nums.Length];

			// compute sum from zero
			int sum = 0;
			for (int i = 0; i < nums.Length; i++)
			{
				sum += nums[i];
				this.sumArr[i] = sum;
			}
		}

		// Time: O(1)
		// Space: O(1)
		public int SumRange(int left, int right)
		{
			if (left == 0)
			{
				return this.sumArr[right];
			}
			return this.sumArr[right] - this.sumArr[left - 1];
		}
	}
}
