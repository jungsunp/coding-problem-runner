using System;
using System.Collections.Generic;
using System.Text;

namespace CodingPractice.Leetcode
{
	public class Stack
	{

		// #2390. Removing Stars From a String
		// Time: O(n)
		// Space: O(n)
		// Note: not stack question?
		public string RemoveStars(string s)
		{
			var builder = new StringBuilder();
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] == '*')
				{
					builder.Remove(builder.Length - 1, 1);
				}
				else
				{
					builder.Append(s[i]);
				}
			}

			return builder.ToString();
		}

		// #735. Asteroid Collision
		// Time: O(n)
		// Space: O(n)
		public int[] AsteroidCollision(int[] asteroids)
		{
			var stack = new Stack<int>();

			// Simulate Collision
			for (int i = 0; i < asteroids.Length; i++)
			{

				if (stack.Count > 0 && stack.Peek() > 0 && asteroids[i] < 0)
				{ // collide
					while (stack.Count > 0 && stack.Peek() > 0 && stack.Peek() < Math.Abs(asteroids[i]))
					{
						stack.Pop();
					}

					if (stack.Count == 0 || stack.Peek() < 0)
					{
						stack.Push(asteroids[i]);
					}
					else if (stack.Peek() == Math.Abs(asteroids[i]))
					{ // same size collision
						stack.Pop();
					}
				}
				else
				{
					stack.Push(asteroids[i]);
				}
			}

			// Move remaining asteroids in stack to array
			var ret = new int[stack.Count];
			for (int i = stack.Count - 1; i >= 0; i--)
			{
				ret[i] = stack.Pop();
			}

			return ret;
		}

		// #394. Decode String
		// Time: O(n)
		// Space: O(n)
		// Note: Check Leetcode for different view on time & space complexityies
		public string DecodeString(string s)
		{

			// Use stack to decode all repeats
			var stack = new Stack<string>();
			foreach (var ch in s)
			{
				if (ch != ']')
				{
					stack.Push(ch.ToString());
				}
				else
				{
					var strBuilder = new StringBuilder();
					while (stack.Peek()[0] != '[')
					{
						strBuilder.Insert(0, stack.Pop());
					}
					var subStr = strBuilder.ToString();

					stack.Pop(); // Remove '[' from stack

					var cntBuilder = new StringBuilder();
					while (stack.Count > 0 && Char.IsNumber(stack.Peek()[0]))
					{
						cntBuilder.Insert(0, stack.Pop());
					}
					int cnt = Int32.Parse(cntBuilder.ToString());

					for (int i = 0; i < cnt - 1; i++)
					{
						strBuilder.Append(subStr);
					}

					stack.Push(strBuilder.ToString());
				}
			}

			// Use stack to generate return string
			var builder = new StringBuilder();
			while (stack.Count > 0)
			{
				builder.Insert(0, stack.Pop());
			}

			return builder.ToString();
		}

		// #20. Valid Parentheses
		// Time: O(n)
		// Space: O(n)
		public bool IsValid(string s)
		{
			Stack<char> stack = new Stack<char>();
			foreach (char bracket in s)
			{
				if (bracket == '(' || bracket == '{' || bracket == '[')
				{
					stack.Push(bracket);
				}
				else
				{
					if (stack.Count < 1)
					{
						return false;
					}

					char openBracket = stack.Pop();
					if ((bracket == ')' && openBracket != '(') ||
						(bracket == '}' && openBracket != '{') ||
						(bracket == ']' && openBracket != '['))
					{
						return false; //invalid bracket match
					}
				}
			}

			return stack.Count > 0 ? false : true;
		}

		// #227. Basic Calculator II
		// Time: O(n)
		// Space: O(n)
		// Note: you can further optimize space using O(1)
		//  Review this!!
		public int Calculate(string s)
		{
			Stack<int> stack = new();
			int currentNum = 0;
			char operation = '+';

			// Iterate string and put numbers to stack
			for (int i = 0; i < s.Length; i++)
			{
				if (char.IsDigit(s[i]))
				{
					currentNum = currentNum * 10 + (s[i] - '0');
				}

				if (i == s.Length - 1 || (!char.IsDigit(s[i]) && s[i] != ' '))
				{
					if (operation == '+')
					{
						stack.Push(currentNum);
					}
					else if (operation == '-')
					{
						stack.Push(-currentNum);
					}
					else if (operation == '*')
					{
						stack.Push(stack.Pop() * currentNum);
					}
					else if (operation == '/')
					{
						stack.Push(stack.Pop() / currentNum);
					}

					operation = s[i]; // reset to next operation
					currentNum = 0;
				}
			}

			// Run additions from stack
			int res = 0;
			while (stack.Count > 0)
			{
				res += stack.Pop();
			}

			return res;
		}

		// #71. Simplify Path
		// Time: O(n)
		// Space: O(n)
		public string SimplifyPath(string path)
		{
			string[] names = path.Split('/');
			Stack<string> stack = new();

			foreach (string name in names)
			{
				if (string.IsNullOrEmpty(name))
					continue;

				if (name == ".")
				{
					// do nothing
				}
				else if (name == "..")
				{
					if (stack.Count > 0)
					{
						stack.Pop();
					}
				}
				else
				{
					stack.Push(name);
				}
			}

			if (stack.Count == 0)
				return "/";

			StringBuilder res = new();
			while (stack.Count > 0)
			{
				res.Insert(0, $"/{stack.Pop()}");
			}

			return res.ToString();
		}

		// #636. Exclusive Time of Functions
		// Time: O(n)
		// Space: O(n)
		public int[] ExclusiveTime(int n, IList<string> logs)
		{
			Stack<int> callStack = new(); // function Id
			int[] res = new int[n];
			int prev = -1;

			// Read & parse logs
			foreach (string log in logs)
			{
				string[] logArr = log.Split(":");
				int functionId = int.Parse(logArr[0]);
				bool isStart = logArr[1] == "start";
				int time = int.Parse(logArr[2]);

				if (prev < 0)
				{ // first log
					callStack.Push(functionId);
					prev = time;
					continue;
				}

				if (isStart)
				{
					if (callStack.Count > 0)
					{
						int prevFuncId = callStack.Peek();
						res[prevFuncId] += time - prev;
					}
					callStack.Push(functionId);
					prev = time;
				}
				else
				{ // end
					res[functionId] += time - prev + 1;
					callStack.Pop();
					prev = time + 1;
				}
			}

			return res;
		}
	}
}
