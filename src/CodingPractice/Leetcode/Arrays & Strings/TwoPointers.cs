using System;

namespace CodingPractice.Leetcode
{
	public class TwoPointers
	{

		// #283. Move Zeroes
		// Time: O(n)
		// Space: O(1)
		public void MoveZeroes(int[] nums)
		{
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
		public int MaxArea(int[] height)
		{
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

		// #42. Trapping Rain Water (HARD)
		// Time: O(n)
		// Space: O(1)
		public int Trap(int[] height)
		{
			int left = 0;
			int right = height.Length - 1;
			int totalSum = 0;
			int leftMax = 0;
			int rightMax = 0;

			while (left < right)
			{
				// Move pointer with smaller height (water is boud by smaller height)
				if (height[left] < height[right])
				{
					leftMax = Math.Max(leftMax, height[left]);
					totalSum += leftMax - height[left];
					left++;
				}
				else
				{
					rightMax = Math.Max(rightMax, height[right]);
					totalSum += rightMax - height[right];
					right--;
				}
			}

			return totalSum;
		}

		// #392. Is Subsequence
		// Time: O(n)
		// Space: O(1)
		public bool IsSubsequence(string s, string t)
		{
			int ps = 0; // pointer s
			int pt = 0; // pointer t

			while (ps < s.Length && pt < t.Length)
			{
				if (s[ps] == t[pt])
				{
					ps++;
				}

				pt++;
			}

			return ps == s.Length;
		}


		// #1679. Max Number of K-Sum Pairs
		// Time: O(n log n)
		// Space: O (log n)
		// Note: there is diff solution to use hash map with O(n) time and space
		public int MaxOperations(int[] nums, int k)
		{
			Array.Sort(nums);

			int left = 0;
			int right = nums.Length - 1;
			int count = 0;

			while (left < right)
			{
				int sum = nums[left] + nums[right];

				if (sum < k)
				{
					left++;
				}
				else if (sum > k)
				{
					right--;
				}
				else
				{
					count++;
					left++;
					right--;
				}
			}

			return count;
		}

		// #88. Merge Sorted Array
		// Time: O(m + n)
		// Space: O(1)
		public void Merge(int[] nums1, int m, int[] nums2, int n)
		{
			if (n == 0) { return; }

			int p1 = m - 1; // pointer for nums1
			int p2 = n - 1; // pointer for nums2

			// Iterate from right side
			for (int i = m + n - 1; i >= 0; i--)
			{
				if (p2 < 0) { break; } // we are done

				if (p1 >= 0 && nums1[p1] > nums2[p2])
				{
					nums1[i] = nums1[p1--];
				}
				else
				{
					nums1[i] = nums2[p2--];
				}
			}
		}

		// #31. Next Permutation (Lexicographic Ordering)
		// Time: O(n)
		// Space: O(1)
		// Note: This is hard.
		//  Start with  Brute force - generate all permutations and sort lexicographically
		public void NextPermutation(int[] nums)
		{
			int n = nums.Length;

			// Step-1: Find largest k such that nums[k] < nums[k+1]
			int k = n - 2;
			while (k >= 0)
			{
				if (nums[k] < nums[k + 1])
				{
					break; // found our k
				}
				k--;
			}

			if (k < 0)
			{
				// end of permutations. reverse the whole array and return.
				this.ReverseArray(nums, 0, n - 1);
				return;
			}

			// Step-2: Find larget l such that nums[k] < nums[l]
			int l = n - 1;
			while (l > k)
			{
				if (nums[k] < nums[l])
				{
					break; // found our l;
				}
				l--;
			}

			// Step-3: Swap k & l
			(nums[k], nums[l]) = (nums[l], nums[k]);

			// Step-4: Reverse from (k+1) .... (n-1)
			this.ReverseArray(nums, k + 1, n - 1);
		}

		private void ReverseArray(int[] nums, int left, int right)
		{
			while (left < right)
			{
				(nums[left], nums[right]) = (nums[right], nums[left]); // swap
				left++;
				right--;
			}
		}
	}
}
