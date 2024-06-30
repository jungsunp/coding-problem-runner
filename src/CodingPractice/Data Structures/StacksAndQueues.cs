using System;
using System.Collections.Generic;
using System.ComponentModel;

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

	// #3.3
	public class SetOfStacks<T> : Stack<T>
	{
		private readonly int maxPlates;
		private readonly Stack<Stack<T>> stacks;

		public new int Count {
			get { return stacks.Count; }
		}

		public SetOfStacks(int maxPlates)
		{
			this.maxPlates = maxPlates;
			this.stacks = new Stack<Stack<T>>();
			stacks.Push(new Stack<T>());
		}

		public new void Push(T value)
		{
			var topStack = stacks.Peek();
			if (topStack.Count < this.maxPlates)
			{
				topStack.Push(value);
			}
			else
			{
				var stack = new Stack<T>();
				stack.Push(value);
				this.stacks.Push(stack);
			}
		}

		public new T Pop()
		{
			var topStack = this.stacks.Peek();
			var ret = topStack.Pop();

			if (topStack.Count < 1)
			{
				this.stacks.Pop();
			}

			return ret;
		}

		// Time: O(n)
		// Space: O(n)
		// Note: index of stacks (not index of element)
		public T PopAt(int index)
		{
			if (index < 0 || index > this.Count)
			{
				throw new InvalidOperationException();
			}

			// Temporarily move stacks to temp stack
			var tempStacks = new Stack<Stack<T>>();
			int numStacks = this.Count;
			for (int i = 0; i < numStacks - index - 1; i++)
			{
				var topStack = this.stacks.Pop();
				var newStack = new Stack<T>();
				while (topStack.Count > 0)
				{
					newStack.Push(topStack.Pop());
				}

				tempStacks.Push(newStack);
			}

			var ret = this.Pop();

			// Move all elements back to stack
			while(tempStacks.Count > 0)
			{
				var stack = tempStacks.Pop();
				while (stack.Count > 0)
				{
					this.Push(stack.Pop());
				}
			}

			return ret;
		}
	}
}
