using System;
using System.Collections.Generic;
using System.Text;

namespace CodingPractice.Leetcode
{
	public class Stack {

		// #2390. Removing Stars From a String
		// Time: O(n)
		// Space: O(n)
		public string RemoveStars(string s) {
			int i = s.Length - 1;
			var stack = new Stack<char>();

			while (i >= 0)
			{
				if (s[i] == '*')
				{
					int starCount = 1;
					i--;
					while (i >= 0 && s[i] == '*')
					{
						starCount++;
						i--;
					}

					while (starCount > 0)
					{
						if (s[i] == '*')
						{
							starCount++;
						}
						else
						{
							starCount--;
						}
						i--;
					}

				}
				else
				{
					stack.Push(s[i]);
					i--;
				}
			}

			var builder = new StringBuilder();
			while(stack.Count > 0)
			{
				builder.Append(stack.Pop());
			}

			return builder.ToString();
		}
	}
}
