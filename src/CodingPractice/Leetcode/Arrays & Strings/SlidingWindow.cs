using System;
using System.Collections.Generic;

namespace CodingPractice.Leetcode
{
	public class SlidingWindow {

		// #643. Maximum Average Subarray I
		// Time: O(n)
		// Space: O(1)
		public double FindMaxAverage(int[] nums, int k) {
			int sum = 0;
			for (int i = 0; i < k; i++)
			{
				sum += nums[i];
			}

			int max = sum;
			for (int i = k; i < nums.Length; i++)
			{
				sum += nums[i] - nums[i - k];
				if (sum > max)
				{
					max = sum;
				}
			}

			return (double) max / k;
		}

		// #1456. Maximum Number of Vowels in a Substring of Given Length
		// Time: O(n)
		// Space: O(1)
		private readonly HashSet<char> vowelHash = new HashSet<char> () { 'a', 'e', 'i', 'o', 'u' };

		public int MaxVowels(string s, int k) {
			int numVowels = 0;
			for (int i = 0; i < k; i++)
			{
				if (vowelHash.Contains(s[i])) { numVowels++; }
			}

			int maxVowels = numVowels;
			for (int i = k; i < s.Length; i++)
			{
				if (maxVowels == k) { return k; } // maxVowels is always less than equal to k
				numVowels += vowelHash.Contains(s[i]) ? 1 : 0;
				numVowels -= vowelHash.Contains(s[i-k]) ? 1 : 0;
				maxVowels = Math.Max(numVowels, maxVowels);
			}

			return maxVowels;
		}

		// #1004. Max Consecutive Ones III
		// Time: O(n)
		// Space: O(1)
		public int LongestOnes(int[] nums, int k) {
			int i = 0;
			int numZeros = 0;

			// Set up initial sliding window
			while (numZeros < k && i < nums.Length) {
				if (nums[i++] == 0) {
					numZeros++;
				}
			}

			int numOnes = i;
			int maxOnes = i;
			for (int j = i; j < nums.Length; j++) {
				if (nums[j] == 1) {
					numOnes++;
					maxOnes = Math.Max(numOnes, maxOnes);
				}
				else { //nums[j] == 0
					if (nums[j - numOnes] == 1) {
						int l = j - numOnes;
						while (nums[l++] == 1) {
							numOnes--;
						}
					}
				}
			}

			return maxOnes;
		}

		// #1493. Longest Subarray of 1's After Deleting One Element
		// Time: O(n)
		// Space: O(1)
		public int LongestSubarray(int[] nums) {
			int left = 0;
			int right;
			int numZeroInWindow = 0;

			for (right = 0; right < nums.Length; right++) {
				if (nums[right] == 0) {
					numZeroInWindow++;
				}

				if (numZeroInWindow > 1) {
					if (nums[left] == 0) {
						numZeroInWindow--;
					}
					left++; // trick here is to not to reduce size of window even when window is invalid
				}
			}

			return right - left - 1;
		}
	}
}
