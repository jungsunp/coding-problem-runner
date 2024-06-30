using System;
using System.Collections.Generic;

namespace CodingPractice
{
	public class StacksAndQueues
	{

	}

	// #3.2
	// Time: O(1) for all push, pop, min
	public class StackMin : Stack<int>
	{
		private Stack<int> subMinStack;

		public StackMin()
		{
			subMinStack = new Stack<int>();
		}

		public new void Push(int value)
		{
			if (value < this.Min())
			{
				subMinStack.Push(value);
			}

			base.Push(value);
		}

		public new int Pop()
		{
			if (this.Peek() <= this.Min())
			{
				subMinStack.Pop();
			}

			return base.Pop();
		}

		public int Min()
		{
			if (subMinStack.Count < 1)
			{
				return int.MaxValue;
			}

			return subMinStack.Peek();
		}
	}
}
