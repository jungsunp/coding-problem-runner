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
	}
}
