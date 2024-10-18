using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingPractice.Leetcode
{
	public class BackTracking
	{
		private readonly Dictionary<char, string[]> numberMap = new () {
			{ '2' , new string[3] {"a", "b", "c"} },
			{ '3' , new string[3] {"d", "e", "f"} },
			{ '4' , new string[3] {"g", "h", "i"} },
			{ '5' , new string[3] {"j", "k", "l"} },
			{ '6' , new string[3] {"m", "n", "o"} },
			{ '7' , new string[4] {"p", "q", "r", "s"} },
			{ '8' , new string[3] {"t", "u", "v"} },
			{ '9' , new string[4] {"w", "x", "y", "z"} }
		};

		// #17. Letter Combinations of a Phone Number
		// Time: O(n * 4 ^ n)
		// Space: O(n)
		public IList<string> LetterCombinations(string digits) {
			var ret = new List<string>();

			if (digits.Length == 0) {
				return ret;
			}

			if (digits.Length == 1) {
				foreach (var str in this.numberMap[digits[0]]) {
					ret.Add(str);
				}
			}
			else {
				var list = LetterCombinations(digits.Substring(1));
				foreach (var subStr in list) {
					foreach (var str in this.numberMap[digits[0]]) {
						ret.Add(str + subStr);
					}
				}
			}

			return ret;
		}
	}
}
