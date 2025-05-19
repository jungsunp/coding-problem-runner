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

	// 303. Range Sum Query - Immutable
	public class NumArray {
		private int[] sumArr;

		// Time: O(n)
		// Space: O(n)
		public NumArray(int[] nums) {
			this.sumArr = new int[nums.Length];

			// compute sum from zero
			int sum = 0;
			for (int i = 0; i < nums.Length; i++) {
				sum += nums[i];
				this.sumArr[i] = sum;
			}
		}

		// Time: O(1)
		// Space: O(1)
		public int SumRange(int left, int right) {
			if (left == 0) {
				return this.sumArr[right];
			}
			return this.sumArr[right] - this.sumArr[left - 1];
		}
	}
}
