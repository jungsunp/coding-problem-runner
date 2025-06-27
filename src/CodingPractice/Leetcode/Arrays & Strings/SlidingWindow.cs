using System;
using System.Collections.Generic;

namespace CodingPractice.Leetcode
{
	public class SlidingWindow
	{
		// #3. Longest Substring Without Repeating Characters
		// Time: O(n)
		// Space: O(k) - k: number of unique chars
		public int LengthOfLongestSubstring(string s)
		{
			if (s.Length <= 1) { return s.Length; }

			int left = 0;
			int right = 0;
			int maxLength = 0;
			Dictionary<char, int> hash = new Dictionary<char, int>();

			while (right < s.Length)
			{
				if (hash.ContainsKey(s[right]) && hash[s[right]] >= left)
				{
					left = hash[s[right]] + 1; // move left to one next to repeating char
				}

				hash[s[right]] = right;
				maxLength = Math.Max(maxLength, right - left + 1);
				right++;
			}

			return maxLength;
		}

		// #643. Maximum Average Subarray I
		// Time: O(n)
		// Space: O(1)
		public double FindMaxAverage(int[] nums, int k)
		{
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

			return (double)max / k;
		}

		// #1456. Maximum Number of Vowels in a Substring of Given Length
		// Time: O(n)
		// Space: O(1)
		private readonly HashSet<char> vowelHash = new HashSet<char>() { 'a', 'e', 'i', 'o', 'u' };

		public int MaxVowels(string s, int k)
		{
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
				numVowels -= vowelHash.Contains(s[i - k]) ? 1 : 0;
				maxVowels = Math.Max(numVowels, maxVowels);
			}

			return maxVowels;
		}

		// #1004. Max Consecutive Ones III
		// Time: O(n)
		// Space: O(1)
		public int LongestOnes(int[] nums, int k)
		{
			int i = 0;
			int numZeros = 0;

			// Set up initial sliding window
			while (numZeros < k && i < nums.Length)
			{
				if (nums[i++] == 0)
				{
					numZeros++;
				}
			}

			int numOnes = i;
			int maxOnes = i;
			for (int j = i; j < nums.Length; j++)
			{
				if (nums[j] == 1)
				{
					numOnes++;
					maxOnes = Math.Max(numOnes, maxOnes);
				}
				else
				{ //nums[j] == 0
					if (nums[j - numOnes] == 1)
					{
						int l = j - numOnes;
						while (nums[l++] == 1)
						{
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
		public int LongestSubarray(int[] nums)
		{
			int left = 0;
			int right;
			int numZeroInWindow = 0;

			for (right = 0; right < nums.Length; right++)
			{
				if (nums[right] == 0)
				{
					numZeroInWindow++;
				}

				if (numZeroInWindow > 1)
				{
					if (nums[left] == 0)
					{
						numZeroInWindow--;
					}
					left++; // trick here is to not to reduce size of window even when window is invalid
				}
			}

			return right - left - 1;
		}

		// #424. Longest Repeating Character Replacement
		// Time: O(n)
		// Space: O(1) - hash length of 26 is constant
		public int CharacterReplacement(string s, int k)
		{
			int[] hash = new int[26]; // upper case english Character
			char mostFreqChar = s[0];

			// Sliding window (expand-contract style)
			int left = 0;
			int right = 0;
			while (right < s.Length)
			{
				if (++hash[s[right] - 'A'] > hash[mostFreqChar - 'A'])
				{
					mostFreqChar = s[right];
				}

				// Check if window is invalid
				if (right - left + 1 - hash[mostFreqChar - 'A'] > k)
				{
					--hash[s[left] - 'A']; // Note: most frequent char can be stale but it's ok
					left++; // trick: move left pointer but don't ever shrink window size
				}

				right++;
			}

			return right - left;
		}

		// #3234. Count the Number of Substrings With Dominant Ones
		// Time: O(n * sqrt(n))
		// Space: O(n)
		// Note: pretty difficult skipping logic
		public int NumberOfSubstrings(string s)
		{
			int n = s.Length;
			int[] prefix = new int[n]; // count num of 1s in 0 ... k
			prefix[0] = s[0] == '1' ? 1 : 0;
			for (int i = 1; i < n; i++)
			{
				prefix[i] = s[i] == '1' ? prefix[i - 1] + 1 : prefix[i - 1];
			}

			int res = 0;
			for (int i = 0; i < n; i++)
			{
				int j = i;
				while (j < n)
				{
					int numOne = i == 0 ? prefix[j] : prefix[j] - prefix[i - 1];
					int numZero = (j - i + 1) - numOne;

					if (numOne < numZero * numZero)
					{ // non-dominant
						j += numZero * numZero - numOne; // skip ahead by difference
					}
					else if (numOne == numZero * numZero)
					{ // exact match dominant
						res++;
						j++;
					}
					else
					{ // dominant
						int diff = (int)Math.Sqrt(numOne) - numZero;

						if (j + diff < n)
						{
							res += diff + 1;
						}
						else
						{
							res += n - j;
						}

						j += diff + 1;
					}
				}
			}

			return res;
		}
	}
}
