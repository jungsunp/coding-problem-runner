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

		// #739. Daily Temperatures (Monotonic stack - descreasing order)
		// Time: O(n)
		// Space: O(n)
		public int[] DailyTemperatures(int[] temperatures)
		{
			int[] ans = new int[ temperatures.Length ];
			Stack<int> indices = new Stack<int>();
			indices.Push(0);

			for (int i = 1; i < temperatures.Length; i++)
			{
				while (indices.Count > 0 && temperatures[indices.Peek()] < temperatures[i])
				{
					int index = indices.Pop();
					ans[index] = i - index;
				}
				
				indices.Push(i);
			}

			return ans;
		}
	}
}
