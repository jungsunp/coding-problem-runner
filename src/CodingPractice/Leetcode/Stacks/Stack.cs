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
	}

	// #155. Min Stack
	// Time: O(1) for all functions
	// Space: O(n)
	// Note: Duplicate problem in Data Structures/StacksAndQueues
	public class MinStack
	{
		private Stack<int> _baseStack;
		private Stack<int> _minStack;

		public MinStack()
		{
			_baseStack = new();
			_minStack = new();
		}

		public void Push(int val)
		{
			_baseStack.Push(val);
			if (_minStack.Count < 1 || _minStack.Peek() >= val)
			{
				_minStack.Push(val);
			}
		}

		public void Pop()
		{
			if (_baseStack.Peek() <= _minStack.Peek())
			{
				_minStack.Pop();
			}
			_baseStack.Pop();
		}

		public int Top()
		{
			return _baseStack.Peek();
		}

		public int GetMin()
		{
			return _minStack.Peek();
		}
	}
}
