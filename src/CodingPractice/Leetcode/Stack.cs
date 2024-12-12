using System;
using System.Collections.Generic;
using System.Text;

namespace CodingPractice.Leetcode
{
	public class Stack {

		// #2390. Removing Stars From a String
		// Time: O(n)
		// Space: O(n)
		// Note: not stack question?
		public string RemoveStars(string s) {
			var builder = new StringBuilder();
			for (int i = 0; i < s.Length; i++) {
				if (s[i] == '*') {
					builder.Remove(builder.Length - 1, 1);
				}
				else {
					builder.Append(s[i]);
				}
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

		// #735. Asteroid Collision
		// Time: O(n)
		// Space: O(n)
		public int[] AsteroidCollision(int[] asteroids) {
			var stack = new Stack<int>();

			// Simulate Collision
			for(int i = 0; i < asteroids.Length; i++) {

				if (stack.Count > 0 && stack.Peek() > 0 && asteroids[i] < 0) { // collide
					while (stack.Count > 0 && stack.Peek() > 0 && stack.Peek() < Math.Abs(asteroids[i])) {
						stack.Pop();
					}

					if (stack.Count == 0 || stack.Peek() < 0) {
						stack.Push(asteroids[i]);
					}
					else if (stack.Peek() == Math.Abs(asteroids[i])) { // same size collision
						stack.Pop();
					}
				}
				else {
					stack.Push(asteroids[i]);
				}
			}

			// Move remaining asteroids in stack to array
			var ret = new int[stack.Count];
			for(int i = stack.Count - 1; i >= 0; i--) {
				ret[i] = stack.Pop();
			}

			return ret;
		}

		// #394. Decode String
		// Time: O(n)
		// Space: O(n)
		// Note: Check Leetcode for different view on time & space complexityies
		public string DecodeString(string s) {

			// Use stack to decode all repeats
			var stack = new Stack<string>();
			foreach (var ch in s) {
				if (ch != ']') {
					stack.Push(ch.ToString());
				}
				else {
					var strBuilder = new StringBuilder();
					while (stack.Peek()[0] != '[') {
						strBuilder.Insert(0, stack.Pop());
					}
					var subStr = strBuilder.ToString();

					stack.Pop(); // Remove '[' from stack

					var cntBuilder = new StringBuilder();
					while (stack.Count > 0 && Char.IsNumber(stack.Peek()[0])) {
						cntBuilder.Insert(0, stack.Pop());
					}
					int cnt = Int32.Parse(cntBuilder.ToString());

					for (int i = 0; i < cnt - 1; i++) {
						strBuilder.Append(subStr);
					}

					stack.Push(strBuilder.ToString());
				}
			}

			// Use stack to generate return string
			var builder = new StringBuilder();
			while (stack.Count > 0) {
				builder.Insert(0, stack.Pop());
			}

			return builder.ToString();
		}
	}
}
