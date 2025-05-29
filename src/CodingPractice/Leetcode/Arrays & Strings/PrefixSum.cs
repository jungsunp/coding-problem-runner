using System;

namespace CodingPractice.Leetcode
{
	public class PrefixSum {

		// #1732. Find the Highest Altitude
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

		// #238. Product of Array Except Self
		// Time: O(n)
		// Space: O(1)
		public int[] ProductExceptSelf(int[] nums) {
			int [] ret = new int[nums.Length];
			ret[nums.Length - 1] = 1;

			// calculate postfix products and store into ret to save memory
			for (int i = nums.Length - 2; i >= 0; i--) {
				ret[i] = nums[i + 1] * ret[i + 1];
			}

			// calculate prefix product and store into a variable
			int prefix = 1;
			for (int i = 0; i < nums.Length; i++) {
				ret[i] = prefix * ret[i]; // prefix * suffix
				prefix *= nums[i];
			}

			return ret;
		}

		// #3434. Maximum Frequency After Subarray Operation
		// Time: O(n)
		// Space: O(1)
		public int MaxFrequency(int[] nums, int k) {
			int kCount = 0;
			int min = 1;
			int max = 50;

			// count K occurences
			foreach (int num in nums) {
				if (num == k) {
					kCount++;
				}
				else {
					min = Math.Min(min, num);
					max = Math.Max(max, num);
				}
			}

			// iterate and perform Kadane's Algorithm
			// 1 - if == i, -1 if == k, 0 otherwise
			int maxFreq = 0;
			for (int i = min; i <= max; i++) {
				if (i == k) { continue; }
				int currSum = 0;
				foreach (int num in nums) {
					int point = 0;
					if (num == i) { point = 1; }
					else if (num == k) { point = -1; }

					currSum = Math.Max(currSum + point, point);
					maxFreq = Math.Max(maxFreq, currSum);
				}
			}

			return maxFreq + kCount;
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
