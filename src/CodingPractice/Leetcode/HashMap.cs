using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingPractice.Leetcode
{
	public class HashMap
	{
		// #2215. Find the Difference of Two Arrays
		// Time: O(m + n)
		// Space: O(m + n)
		public IList<IList<int>> FindDifference(int[] nums1, int[] nums2) {
			var set1 = new HashSet<int>(nums1);
			var set2 = new HashSet<int>(nums2);

			var result1 = new List<int>(set1.Count);
			var result2 = new List<int>(set2.Count);

			foreach (var num in set1) {
				if (!set2.Contains(num)) {
					result1.Add(num);
				}
			}

			foreach (var num in set2) {
				if (!set1.Contains(num)) {
					result2.Add(num);
				}
			}

			return new List<IList<int>> { result1, result2 };
		}

		// #1657. Determine if Two Strings Are Close
		// Time: O(n)
		// Space: O(1)
		public bool CloseStrings(string word1, string word2) {
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

			foreach(char character in word)
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
	}
}
