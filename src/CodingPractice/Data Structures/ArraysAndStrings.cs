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

		// #1.7
		// Time: O(n^2)
		// Space: O(1)
		public static int[,] RotateMatrix(int[,] matrix)
		{
			if (matrix.GetLength(0) < 2)
			{
				return matrix; // don't need to rotate
			}

			return RotateMatrix(matrix, 0);
		}

		private static int[,] RotateMatrix(int[,] matrix, int layer)
		{
			int n = matrix.GetLength(0); // Assume n * n matrix
			if (layer >= n / 2)
			{
				return matrix; // we are done rotating
			}

			// rotate outer layer
			for (int i = layer; i < n-1-layer; i++)
			{
				int tempOne = matrix[n-2-i, n-1-layer];
				matrix[n-2-i, n-1-layer] = matrix[layer, i];

				int tempTwo = matrix[n-1-layer, n-1-i];
				matrix[n-1-layer, n-1-i] = tempOne;

				tempOne = matrix[n-1-i, layer];
				matrix[n-1-i, layer] = tempTwo;

				matrix[layer, i] = tempOne;
			}

			return RotateMatrix(matrix, layer + 1);
		}
	}
}
