using System;
using System.Collections.Generic;
using System.Text;

namespace CodingPractice.Concepts
{
	public static class Recursion
	{
		// #8.1
		// Time: O(n)
		// Space: O(n) (Call stack: O(n))
		public static int TripleStep(int n)
		{
			return TripleStep(n, new int[n + 1]);
		}

		private static int TripleStep(int n, int[] memo)
		{
			if (n <= 0) { return 0; }
			if (n == 1) { return 1; }
			if (n == 2) { return 2; }
			if (n == 3) { return 4; }

			if (n > 36)
			{
				throw new OverflowException();
			}

			if (memo[n] == 0)
			{
				memo[n] = TripleStep(n - 1, memo) + TripleStep(n - 2, memo) + TripleStep(n - 3, memo);
			}

			return memo[n];
		}

		// #8.3
		// Time: O(log n)
		// Space: O(log n)
		// Assume: arr is sorted (ints may not be unique)
		public static int? MagicIndex(int[] arr)
		{
			if (arr.Length < 1)
			{
				return null;
			}

			return MagicIndex(arr, 0, arr.Length - 1);
		}


		private static int? MagicIndex(int[] arr, int start, int end)
		{
			int mid = (start + end) / 2;

			if (start > end)
			{
				return null; // magic index not found
			}

			if (arr[mid] == mid)
			{
				return mid; // found magic index
			}

			if (arr[mid] > mid)
			{
				int? leftHalfMagicIndex = MagicIndex(arr, start, mid -1);
				if (leftHalfMagicIndex != null)
				{
					return leftHalfMagicIndex;
				}

				return MagicIndex(arr, arr[mid], end);
			}
			else
			{
				int? rightHalfMagicIndex = MagicIndex(arr, mid + 1, end);
				if (rightHalfMagicIndex != null)
				{
					return rightHalfMagicIndex;
				}

				return MagicIndex(arr, start, arr[mid]);
			}
		}

		// #8.8
		// Time: O(n!)
		// Space: O(n!)
		public static List<string> PermutationsWithDups(string str)
		{
			var result = new List<string>();
			if (String.IsNullOrEmpty(str))
			{
				return result;
			}

			var charDict = new Dictionary<char, int>();
			foreach (char c in str)
			{
				if (charDict.ContainsKey(c))
				{
					charDict[c]++;
				}
				else
				{
					charDict.Add(c, 1);
				}
			}

			GetSubStringPermutation("", charDict, result);
			return result;
		}

		private static void GetSubStringPermutation(string prefix, Dictionary<char, int> charDict, List<string> result)
		{
			if (charDict.Count == 0)
			{
				result.Add(prefix);
				return;
			}

			foreach(char c in new List<char>(charDict.Keys)) // Create a copy of the keys to iterate over
			{
				if (charDict[c] == 1)
				{
					charDict.Remove(c);
				}
				else
				{
					charDict[c]--;
				}

				GetSubStringPermutation($"{prefix}{c}", charDict, result);

				if (!charDict.ContainsKey(c))
				{
					charDict.Add(c, 1);
				}
				else
				{
					charDict[c]++;
				}
			}

		}
	}
}
