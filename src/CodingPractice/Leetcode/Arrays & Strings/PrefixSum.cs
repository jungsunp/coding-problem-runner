using System;

namespace CodingPractice.Leetcode
{
	public class PrefixSum {

		// Time: O(n)
		// Space: O(1)
		public int LargestAltitude(int[] gain) {
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
		public int PivotIndex(int[] nums) {
			int leftSum = 0;
			int sum = 0;

			foreach (int num in nums) {
				sum += num;
			}

			for (int i = 0; i < nums.Length; i++) {
				if (leftSum == sum - leftSum - nums[i]) {
					return i;
				}

				leftSum += nums[i];
			}

			return -1;
		}
	}
}
