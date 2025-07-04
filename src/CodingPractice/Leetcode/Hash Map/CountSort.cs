
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodingPractice.Leetcode.Hash_Map
{
	public class CountSort
	{

		// #215. Kth Largest Element in an Array (counting sort)
		// Time: O(n + m), m = max - min + 1
		// Space: O(m)
		public int FindKthLargestOptimized(int[] nums, int k)
		{
			int min = int.MaxValue;
			int max = int.MinValue;

			foreach (int i in nums)
			{
				min = Math.Min(min, i);
				max = Math.Max(max, i);
			}

			int[] countArr = new int[max - min + 1];
			foreach (int i in nums)
			{
				countArr[i - min]++;
			}

			int remain = k;
			for (int i = countArr.Length - 1; i >= 0; i--)
			{
				remain -= countArr[i];
				if (remain <= 0)
				{
					return i + min;
				}
			}

			throw new Exception("Unepxected has occured!");
		}

		// #791. Custom Sort String
		// Time: O(n)
		// Space: O(n)
		// Note: can also solve with sorting on character order with O(n log n)
		public string CustomSortString(string order, string s)
		{
			// Hash map for each char's frequency
			int[] freqArr = new int[26]; // lowercase English chars;
			for (int i = 0; i < s.Length; i++)
			{
				freqArr[s[i] - 'a']++;
			}

			// Iterate order and put into string builder
			StringBuilder res = new();
			foreach (char ch in order)
			{
				int cnt = freqArr[ch - 'a'];
				for (int i = 0; i < cnt; i++)
				{
					res.Append(ch);
				}
				freqArr[ch - 'a'] = 0;
			}

			// Remaing chars not in order
			for (int i = 0; i < freqArr.Length; i++)
			{
				if (freqArr[i] > 0)
				{
					for (int j = 0; j < freqArr[i]; j++)
					{
						res.Append((char)('a' + i));
					}
				}
			}

			return res.ToString();
		}
	}
}
