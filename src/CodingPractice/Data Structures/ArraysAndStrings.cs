using System;
using System.Collections.Generic;

namespace CodingPractice
{
	public static class ArraysAndStrings
	{

		// #1.1
		//Implement an algorithm to determine if a string has all unique characters. What if you cannot use additional data structures?
		// Time: O(n log n)
		// Space: O(1) - depends on sorting algorithm
		public static bool IsUnique(string str)
		{
			if (String.IsNullOrEmpty(str))
			{
				return false;
			}

			var strArr = str.ToCharArray(); // O(n)
			Array.Sort(strArr);  // O(n log n)

			for (int i = 0; i < strArr.Length - 1; i++) // O(n)
			{
				if (strArr[i] == strArr[i + 1])
				{
					return false;
				}
			}
			return true;
		}

		// #1.2
		// Time: O(n)
		// Space: O(n)
		// To reduce speed, use sorting algorithm which will have time O(n log n)
		public static bool CheckPermutation(string strOne, string strTwo)
		{
			if (strOne.Length != strTwo.Length)
			{
				return false;
			}

			var dictOne = ConvertStringToDict(strOne);
			var dictTwo = ConvertStringToDict(strTwo);

			foreach(char character in dictOne.Keys)
			{
				if (!dictTwo.ContainsKey(character) || dictOne[character] != dictTwo[character])
				{
					return false;
				}
			}

			return true;
		}

		// #1.4
		// Time: O(n)
		// Space: O(n)
		public static bool PalindromePermutation(string input){
			var dict = ConvertStringToDict(input);

			int numOddChar = 0;
			foreach(int num in dict.Values)
			{
				if (num % 2 == 1) {
					numOddChar++;

					if (numOddChar > 1) {
						return false;
					}
				}
			}

			return true;
		}

		private static Dictionary<char, int> ConvertStringToDict (string str)
			{
				var dict = new Dictionary<char, int>();
				for (int i = 0; i < str.Length; i++)
				{
					char character = str[i];
					if (Char.IsWhiteSpace(character)) {
						continue;
					}
					if (!dict.ContainsKey(character)) {
						dict[character] = 1;
					} else {
						dict[character]++;
					}
				}

				return dict;
			}
	}
}
