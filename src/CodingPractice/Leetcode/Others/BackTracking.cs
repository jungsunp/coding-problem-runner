using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingPractice.Leetcode
{
	public class BackTracking
	{
		private readonly Dictionary<char, string[]> numberMap = new() {
			{ '2' , new string[3] {"a", "b", "c"} },
			{ '3' , new string[3] {"d", "e", "f"} },
			{ '4' , new string[3] {"g", "h", "i"} },
			{ '5' , new string[3] {"j", "k", "l"} },
			{ '6' , new string[3] {"m", "n", "o"} },
			{ '7' , new string[4] {"p", "q", "r", "s"} },
			{ '8' , new string[3] {"t", "u", "v"} },
			{ '9' , new string[4] {"w", "x", "y", "z"} }
		};
		private readonly IList<string> combinations = [];

		// #17. Letter Combinations of a Phone Number
		// Time: O(n * 4^n)
		// Space: O(n)
		public IList<string> LetterCombinations(string digits)
		{
			if (digits.Length > 0)
			{
				Backtrack(0, digits, new StringBuilder());
			}

			return this.combinations;
		}

		private void Backtrack(int index, string digits, StringBuilder path)
		{
			// Reached at the end of string
			if (path.Length == digits.Length)
			{
				this.combinations.Add(path.ToString());
				return;
			}

			// Run backtracking
			var digit = digits[index];
			foreach (string letter in this.numberMap[digit])
			{
				path.Append(letter);
				Backtrack(index + 1, digits, path);
				path.Remove(path.Length - 1, 1);
			}
		}

		// #216. Combination Sum III
		// Time: O(K * C(9,K))
		// Space: O(k)
		public IList<IList<int>> CombinationSum3(int k, int n)
		{
			List<IList<int>> ret = new List<IList<int>>();
			CombinationSum3Helper(k, n, 1, new List<int>(), ret); // Backtracking
			return ret;
		}

		private static void CombinationSum3Helper(int k, int n, int min, IList<int> current, IList<IList<int>> combList)
		{
			if (n < 0 || min > 10 || current.Count > k)
			{
				return;
			}

			if (n == 0 && current.Count == k)
			{
				combList.Add(current.ToList()); // creates deep copy
				return;
			}

			for (int i = min; i < 10; i++)
			{
				current.Add(i);
				CombinationSum3Helper(k, n - i, i + 1, current, combList);
				current.RemoveAt(current.Count - 1);
			}
		}

		// #22. Generate Parentheses
		// Time: O(4^n / √n) - Catalan number (1/(n+1)) * (2n choose n) ≈ 4^n / (√π * n^(3/2))
		// Space: O(n)
		public IList<string> GenerateParenthesis(int n)
		{
			List<string> comb = new();
			this.ParenthesisBacktrack(n, comb, new StringBuilder(), 0, 0);
			return comb;
		}

		private void ParenthesisBacktrack(int n, IList<string> comb,
			StringBuilder strBuilder, int leftCnt, int rightCnt)
		{

			// Generated valid 2n length parenthesis
			if (strBuilder.Length == 2 * n)
			{
				comb.Add(strBuilder.ToString());
				return;
			}

			// Condtion 1 - left ( can be added
			if (leftCnt < n)
			{
				strBuilder.Append("(");
				ParenthesisBacktrack(n, comb, strBuilder, leftCnt + 1, rightCnt);
				strBuilder.Remove(strBuilder.Length - 1, 1);
			}

			// Condition 2 - right ) can be added
			if (leftCnt > rightCnt)
			{
				strBuilder.Append(")");
				ParenthesisBacktrack(n, comb, strBuilder, leftCnt, rightCnt + 1);
				strBuilder.Remove(strBuilder.Length - 1, 1);
			}
		}
	}
}
