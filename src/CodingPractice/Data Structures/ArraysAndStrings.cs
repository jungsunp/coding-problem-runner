using System;

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
	}
}
