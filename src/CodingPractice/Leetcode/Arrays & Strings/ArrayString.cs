using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodingPractice.Leetcode
{
	public class ArrayString
	{

		// #1768. Merge Strings Alternately
		// Time: O(m + n)
		// Space: O(m + n)
		public string MergeAlternately(string word1, string word2)
		{
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
		public string GcdOfStrings(string str1, string str2)
		{
			if (str1 + str2 != str2 + str1)
			{
				return "";
			}

			int gcd = Gcd(str1.Length, str2.Length);

			return gcd > 0 ? str1.Substring(0, gcd) : "";
		}

		public int Gcd(int j, int k)
		{
			int min = Math.Min(j, k);

			for (int i = min; i > 0; i--)
			{
				if (j % i == 0 && k % i == 0)
				{
					return i;
				}
			}

			return -1; // gcd not found;
		}

		// #151. Reverse Words in a String
		// Time: O(n)
		// Space: O(n)
		public string ReverseWords(string s)
		{
			s = s.Trim(); // remove leading + trailing spaces
			string[] words = s.Split(' ');
			Array.Reverse(words);
			StringBuilder res = new();
			foreach (string word in words)
			{
				if (word.Length == 0) continue; // handle empty word
				if (res.Length > 0)
				{
					res.Append(" ");
				}
				res.Append(word);
			}
			return res.ToString();
		}

		// #1431. Kids With the Greatest Number of Candies
		// Time: O(n)
		// Space: O(1)
		public IList<bool> KidsWithCandies(int[] candies, int extraCandies)
		{
			List<bool> ret = new List<bool>();
			int max = candies.Max();
			foreach (int candy in candies)
			{
				ret.Add(candy + extraCandies >= max);
			}
			return ret;
		}

		// #605. Can Place Flowers
		// Time: O(n)
		// Space: O(1)
		public bool CanPlaceFlowers(int[] flowerbed, int n)
		{
			if (n == 0)
			{
				return true;
			}

			int maxFlowerCnt = 0;
			bool canPlace = true;

			for (int i = 0; i < flowerbed.Length; i++)
			{
				if (flowerbed[i] == 0)
				{
					if (canPlace)
					{
						if ((i + 1 == flowerbed.Length) || flowerbed[i + 1] == 0)
						{
							if (++maxFlowerCnt >= n)
							{ // we are done looking
								return true;
							}
							canPlace = false;
						}
					}
					else
					{
						canPlace = true;
					}
				}
				else
				{  // spot == 1
					canPlace = false;
				}
			}

			return false;
		}

		// #345. Reverse Vowels of a String
		// Time: O(n)
		// Space: O(1)
		public string ReverseVowels(string s)
		{
			char[] vowels = ['a', 'e', 'i', 'o', 'u'];

			StringBuilder sb = new StringBuilder(s);
			int left = 0;
			int right = sb.Length - 1;

			while (left < right)
			{
				while (left < sb.Length && !vowels.Contains(Char.ToLower(sb[left])))
				{
					left++;
				}

				while (right > 0 && !vowels.Contains(Char.ToLower(sb[right])))
				{
					right--;
				}

				if (left >= right)
				{
					break;
				}

				char tmp = sb[left];
				sb[left] = sb[right];
				sb[right] = tmp;

				left++;
				right--;
			}

			return sb.ToString();
		}

		// #334. Increasing Triplet Subsequence
		// Time: O(n)
		// Space: O(1)
		public bool IncreasingTriplet(int[] nums)
		{
			int min = int.MaxValue;
			int secondMin = int.MaxValue;

			foreach (int i in nums)
			{
				if (i <= min)
				{
					min = i;
				}
				else if (i <= secondMin)
				{
					secondMin = i;
				}
				else
				{
					return true;
				}
			}

			return false;
		}

		// #443. String Compression
		// Time: O(n)
		// Space: O(1)
		public int Compress(char[] chars)
		{
			char currentChar = chars[0];
			int counter = 1;
			int pointer = 1; // pointer to update chars

			for (int i = 1; i < chars.Length; i++)
			{
				if (chars[i] != currentChar)
				{
					if (counter > 1)
					{
						foreach (char c in counter.ToString())
						{
							chars[pointer++] = c;
						}
					}

					chars[pointer++] = chars[i];
					currentChar = chars[i];
					counter = 1;
				}
				else
				{
					counter++;
				}
			}

			if (counter > 1)
			{
				foreach (char c in counter.ToString())
				{
					chars[pointer++] = c;
				}
			}

			return pointer;
		}

		// #48. Rotate Image
		// Time: O(n ^ 2)
		// Space: O(1)
		// Note: This is more elgant solution. Easier approach is to rotate by layer
		public void Rotate(int[][] matrix)
		{
			// Transpose Matrix (reverse around main diagnol)
			int n = matrix.Length;
			for (int i = 0; i < n; i++)
			{
				for (int j = i + 1; j < n; j++)
				{
					int tmp = matrix[j][i];
					matrix[j][i] = matrix[i][j];
					matrix[i][j] = tmp;
				}
			}

			// Reverse each row
			for (int i = 0; i < n; i++)
			{
				Array.Reverse(matrix[i]);
			}
		}

		// 8. String to Integer (atoi)
		// Time: O(n)
		// Space: O(1)
		public int MyAtoi(string s)
		{
			long res = 0;
			int pos = 0;
			int n = s.Length;

			// Step 1 - white space
			while (pos < n && s[pos] == ' ')
			{
				pos++;
			}

			if (pos == n)
			{
				return (int)res;
			}

			// Step 2 - + or -
			bool isPositive = true;
			if (s[pos] == '+')
			{
				pos++;
			}
			else if (s[pos] == '-')
			{
				isPositive = false;
				pos++;
			}

			// Step 3 - read digits
			while (pos < n && char.IsDigit(s[pos]))
			{
				res = res * 10 + s[pos] - '0';
				if (isPositive && res > int.MaxValue)
				{
					return int.MaxValue;
				}
				else if (!isPositive && -res < int.MinValue)
				{
					return int.MinValue;
				}
				pos++;
			}

			return isPositive ? (int)res : (int)-res;
		}
	}
}
