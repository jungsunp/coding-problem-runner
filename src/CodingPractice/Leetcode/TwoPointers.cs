using System;

namespace CodingPractice.Leetcode {
	public class TwoPointers {

		// #283. Move Zeroes
		// Time: O(n)
		// Space: O(1)
		public void MoveZeroes(int[] nums) {
			if (nums.Length < 2) { return; }

			int j = 0;
			int k = 1;

			while (k < nums.Length)
			{

				if (nums[j] == 0 && nums[k] == 0)
				{
					k++;
				}
				else if (nums[j] != 0 && nums[k] != 0)
				{
					j += 2;
					k += 2;
				}
				else
				{
					// either one is non-zero
					if (nums[j] == 0)
					{
						nums[j] = nums[k];
						nums[k] = 0;
					}
					j++;
					k++;
				}
			}
		}

		// #11. Container With Most Water
		// Time: O(n)
		// Space: O(1)
		public int MaxArea(int[] height) {
			int left = 0;
			int right = height.Length - 1;
			int max = 0;

			while (left < right)
			{
				int area = Math.Min(height[left], height[right]) * (right - left);
				if (max < area)
				{
					max = area;
				}

				if (height[left] < height[right])
				{
					left++;
				}
				else
				{
					right--;
				}
			}

			return max;
		}
	}
}
