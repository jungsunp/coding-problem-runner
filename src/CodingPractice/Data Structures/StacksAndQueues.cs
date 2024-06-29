using System;

namespace CodingPractice
{
	public class StacksAndQueues
	{

	}


	// #3.2
	// Time: O(1) for all push, pop, min
	public class StackMin
	{
		private class StackMinNode
		{
			public int Value;
			public StackMinNode Next;
			public StackMinNode SubStackMinNode;

			public StackMinNode(int value)
			{
				this.Value = value;
			}
		}

		private StackMinNode top;

		private StackMinNode minNode;

		public void Push(int value)
		{
			var node = new StackMinNode(value);

			if (top != null)
			{
				node.Next = top;
			}

			if (minNode != null)
			{
				if (minNode.Value > node.Value)
				{
					minNode = node;
				}
			}
			else
			{
				minNode = node;
			}

			node.SubStackMinNode = minNode; // each node stores min of substack

			top = node;
		}

		public int Pop()
		{
			if (top == null)
			{
				throw new InvalidOperationException("Empty Stack");
			}

			if (top.Value == this.minNode.Value)
			{
				this.minNode = top.Next.SubStackMinNode;
			}

			var ret = top;
			top = top.Next;
			return ret.Value;
		}

		public int Min()
		{
			if (minNode == null)
			{
				throw new InvalidOperationException("Stack is empty");
			}
			return this.minNode.Value;
		}
	}
}
