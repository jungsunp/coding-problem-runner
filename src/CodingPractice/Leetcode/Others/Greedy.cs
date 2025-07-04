using System;
using System.Collections.Generic;
using System.Text;

namespace CodingPractice.Leetcode.Others
{
	public class Greedy
	{

		// #2434. Using a Robot to Print the Lexicographically Smallest String
		// Time: O(n) - updating min char tracker is constant since only 26 english characters
		// Space: O(n)
		// Note: Greedy + Stack. Pretty difficult
		public string RobotWithString(string s)
		{
			int[] hash = new int[26]; // stores count of character
			foreach (char c in s)
			{
				hash[c - 'a']++;
			}

			StringBuilder builder = new();
			Stack<char> tStack = new();
			char currentMinChar = 'a';

			// For each character perform greedy
			foreach (char c in s)
			{
				tStack.Push(c);
				hash[c - 'a']--;

				// Find current minimum char
				while (currentMinChar != 'z' && hash[currentMinChar - 'a'] == 0)
				{
					currentMinChar = (char)(currentMinChar + 1); // you can also do currentMinChar++
				}

				// Reverse from current min using stack
				while (tStack.Count > 0 && tStack.Peek().CompareTo(currentMinChar) <= 0)
				{
					builder.Append(tStack.Pop());
				}
			}

			return builder.ToString();
		}

		// #670. Maximum Swap
		// Time: O(n)
		// Space: O(n)
		public int MaximumSwap(int num)
		{
			string numStr = num.ToString();
			char[] charArr = numStr.ToCharArray();
			int[] rightIndices = new int[10]; // store right most index of each number 0..9
			for (int i = 0; i < charArr.Length; i++)
			{
				int digit = charArr[i] - '0';
				rightIndices[digit] = i;
			}

			for (int j = 0; j < charArr.Length; j++)
			{
				int digit = charArr[j] - '0';
				for (int k = 9; k > digit; k--)
				{
					int rightIndex = rightIndices[k];
					if (rightIndex > j)
					{
						(charArr[j], charArr[rightIndex]) = (charArr[rightIndex], charArr[j]);
						return int.Parse(new String(charArr));
					}
				}
			}

			return num; // no swap needed
		}
	}
}
