using System.Collections.Generic;

namespace CodingPractice.Leetcode.Stacks.Custom
{
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
