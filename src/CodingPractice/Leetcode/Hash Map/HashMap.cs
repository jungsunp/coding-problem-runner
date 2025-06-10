using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodingPractice.Leetcode
{
	public class HashMap
	{
		// #2215. Find the Difference of Two Arrays
		// Time: O(m + n)
		// Space: O(m + n)
		public IList<IList<int>> FindDifference(int[] nums1, int[] nums2)
		{
			var set1 = new HashSet<int>(nums1);
			var set2 = new HashSet<int>(nums2);

			var result1 = new List<int>(set1.Count);
			var result2 = new List<int>(set2.Count);

			foreach (var num in set1)
			{
				if (!set2.Contains(num))
				{
					result1.Add(num);
				}
			}

			foreach (var num in set2)
			{
				if (!set1.Contains(num))
				{
					result2.Add(num);
				}
			}

			return new List<IList<int>> { result1, result2 };
		}

		// #1657. Determine if Two Strings Are Close
		// Time: O(n)
		// Space: O(1)
		public bool CloseStrings(string word1, string word2)
		{
			if (word1.Length != word2.Length)
			{
				return false;
			}

			// Build character count hash map for both strings
			var dict1 = BuildCharDict(word1);
			var dict2 = BuildCharDict(word2);

			// Check if 2 strings have same set of unique characters
			var charArr1 = dict1.Keys.ToArray();
			var charArr2 = dict2.Keys.ToArray();
			if (charArr1.Length != charArr2.Length || charArr1.Except(charArr2).Any())
			{
				return false;
			}

			// Check if 2 strings have same counts of each unique characters
			var charCountArr1 = dict1.Values.ToArray();
			var charCountArr2 = dict2.Values.ToArray();
			Array.Sort(charCountArr1);
			Array.Sort(charCountArr2);
			for (int i = 0; i < charCountArr1.Length; i++)
			{
				if (charCountArr1[i] != charCountArr2[i])
				{
					return false;
				}
			}

			return true;
		}

		private Dictionary<char, int> BuildCharDict(string word)
		{
			var dict = new Dictionary<char, int>();

			foreach (char character in word)
			{
				if (!dict.ContainsKey(character))
				{
					dict.Add(character, 1);
				}
				else
				{
					dict[character]++;
				}
			}

			return dict;
		}

		// #1207. Unique Number of Occurrences
		// Time: O(n)
		// Space: O(n)
		public bool UniqueOccurrences(int[] arr)
		{
			// Build hash map
			var dict = new Dictionary<int, int>();
			foreach (int num in arr)
			{
				if (!dict.ContainsKey(num))
				{
					dict.Add(num, 1);
				}
				else
				{
					dict[num]++;
				}
			}

			// Compare length with hash set of occurences
			var set = new HashSet<int>(dict.Values);
			return dict.Values.Count == set.Count;
		}

		// #2352. Equal Row and Column Pairs
		// Time: O(n ^ 2)
		// Space: O(n ^ 2)
		public int EqualPairs(int[][] grid)
		{

			// Build a hash map with row value
			var rowDict = new Dictionary<string, int>();
			foreach (int[] row in grid)
			{
				var str = String.Join(",", row);
				rowDict[str] = rowDict.TryGetValue(str, out int cnt) ? cnt + 1 : 1;
			}

			// Use the hash map to compare with col value
			var retCount = 0;
			for (int col = 0; col < grid.Length; col++)
			{
				var builder = new StringBuilder(grid[0][col].ToString());
				for (int row = 1; row < grid.Length; row++)
				{
					builder.Append("," + grid[row][col]);
				}

				retCount += rowDict.TryGetValue(builder.ToString(), out var cnt) ? cnt : 0;
			}

			return retCount;
		}

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

		// #49. Group Anagrams
		// Time: O(n * k)
		// Space: O(n * k) - k is max length of string
		public IList<IList<string>> GroupAnagrams(string[] strs)
		{
			Dictionary<string, List<string>> hash = new();
			foreach (string str in strs)
			{

				// Less optimal solution using Sort
				// char[] chArr = str.ToCharArray();
				// Array.Sort(chArr);
				// string key = new String(chArr);

				int[] countArr = new int[26]; // lower case english
				foreach (char ch in str)
				{
					countArr[ch - 'a']++;
				}
				string key = String.Join(',', countArr);

				if (!hash.ContainsKey(key))
				{
					hash.Add(key, new List<string>());
				}

				hash[key].Add(str);
			}

			IList<IList<string>> ret = new List<IList<string>>();
			foreach (List<string> strList in hash.Values)
			{
				ret.Add(strList);
			}

			return ret;
		}

		// #3572. Maximize Y‑Sum by Picking a Triplet of Distinct X‑Values
		// Time: O(n)
		// Space: O(n)
		public int MaxSumDistinctTriplet(int[] x, int[] y)
		{
			// Store to hash with x value as a key & max y as value
			Dictionary<int, int> hash = new();
			for (int i = 0; i < x.Length; i++)
			{
				if (!hash.ContainsKey(x[i]))
				{
					hash[x[i]] = y[i];
				}
				else
				{
					hash[x[i]] = Math.Max(hash[x[i]], y[i]);
				}
			}

			if (hash.Values.Count < 3)
			{
				return -1; // return valid
			}

			// Iterate hash values for max triplet (you can also use PQ)
			int first = int.MinValue;
			int second = int.MinValue;
			int third = int.MinValue;

			foreach (int yVal in hash.Values)
			{
				if (yVal > first)
				{
					(second, third) = (first, second);
					first = yVal;
				}
				else if (yVal > second)
				{
					third = second;
					second = yVal;
				}
				else if (yVal > third)
				{
					third = yVal;
				}
			}

			return first + second + third;
		}
	}
}
