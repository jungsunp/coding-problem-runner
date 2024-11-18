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

		// #605. Can Place Flowers
		// Time: O(n)
		// Space: O(1)
		public bool CanPlaceFlowers(int[] flowerbed, int n) {
			 if (n == 0) {
				return true;
			}

			int maxFlowerCnt = 0;
			bool canPlace = true;
 
			for (int i = 0; i < flowerbed.Length; i++) {
				if (flowerbed[i] == 0) {
					if (canPlace) {
						if ((i + 1 == flowerbed.Length) || flowerbed[i + 1] == 0) {
							if (++maxFlowerCnt >= n) { // we are done looking
								return true;
							} 
							canPlace = false;
						}
					}
					else {
						canPlace = true;
					}
				}
				else {  // spot == 1
					canPlace = false;
				}
			}

			return false;
		}

		// #345. Reverse Vowels of a String
		// Time: O(n)
		// Space: O(1)
		public string ReverseVowels(string s) {
			char[] vowels = ['a', 'e', 'i', 'o', 'u'];

			StringBuilder sb = new StringBuilder(s);
			int left = 0;
			int right = sb.Length - 1;

			while (left < right) {
				while (left < sb.Length && !vowels.Contains(Char.ToLower(sb[left]))) {
					left++;
				}

				while (right > 0 && !vowels.Contains(Char.ToLower(sb[right]))) {
					right--;
				}

				if (left >= right) { break; }

				char tmp = sb[left];
				sb[left] = sb[right];
				sb[right] = tmp;

				left++;
				right--;
			}

			return sb.ToString();
		}
	}
}
