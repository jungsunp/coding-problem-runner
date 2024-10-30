using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodingPractice.Leetcode
{
	public class ArrayString {

		// #1768. Merge Strings Alternately
		// Time: O(m + n)
		// Space: O(m + n)
		public string MergeAlternately(string word1, string word2) {
			int minLength = Math.Min(word1.Length, word2.Length);
			var builder = new StringBuilder();

			for (int i = 0; i < minLength; i++)
			{
				builder.Append(word1[i]);
				builder.Append(word2[i]);
			}

			if (minLength < word1.Length)
			{
				builder.Append(word1.Substring(minLength));
			}
			else if (minLength < word2.Length)
			{
				builder.Append(word2.Substring(minLength));
			}

			return builder.ToString();
		}


		// #1071. Greatest Common Divisor of Strings
		// Time: O(m + n)
		// Space: O(1)
		public string GcdOfStrings(string str1, string str2) {
			if (str1 + str2 != str2 + str1) {
				return "";
			}

			int gcd = Gcd(str1.Length, str2.Length);

			return gcd > 0 ? str1.Substring(0, gcd) : "";
		}

		public int Gcd(int j, int k) {
			int min = Math.Min(j, k);

			for (int i = min; i > 0; i--) {
				if (j % i == 0 && k % i == 0) {
					return i;
				}
			}

			return -1; // gcd not found;
		}

		// #151. Reverse Words in a String
		// Time: O(n)
		// Space: O(n)
		public string ReverseWords(string s) {
			StringBuilder retBuilder = new StringBuilder();
			StringBuilder wordBuilder = new StringBuilder();

			for (int i = s.Length - 1; i >= 0; i--) {
				if (s[i] != ' ') {
					wordBuilder.Insert(0, s[i]);
				} else {
					if (wordBuilder.Length != 0) {
						string word = wordBuilder.ToString();
						if (retBuilder.Length > 0) {
							retBuilder.Append($" {word}");
						} else {
							retBuilder.Append(word);
						}
						wordBuilder.Clear();
					}
				}
			}

			if (wordBuilder.Length == 0) {
				return retBuilder.ToString();
			} else {
				return retBuilder.Length > 0 ?  $"{retBuilder} {wordBuilder}" : wordBuilder.ToString();
			}
		}

		// #1431. Kids With the Greatest Number of Candies
		// Time: O(n)
		// Space: O(1)
		public IList<bool> KidsWithCandies(int[] candies, int extraCandies) {
			List<bool> ret = new List<bool>();
			int max = candies.Max();
			foreach (int candy in candies) {
				ret.Add(candy + extraCandies >= max);
			}
			return ret;
		}
	}
}
