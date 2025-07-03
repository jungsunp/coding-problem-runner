using System;
using System.Collections.Generic;
using System.Text;

namespace CodingPractice.Leetcode
{
	public class MonotonicStack
	{

		// #739. Daily Temperatures (Monotonic stack - descreasing order)
		// Time: O(n)
		// Space: O(n)
		public int[] DailyTemperatures(int[] temperatures)
		{
			int[] ans = new int[temperatures.Length];
			Stack<int> indices = new();
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

		// #1762. Buildings With an Ocean View
		// Time: O(n)
		// Space: O(n)
		public int[] FindBuildings(int[] heights)
		{
			int max = 0;
			Stack<int> stack = new();
			for (int i = heights.Length - 1; i >= 0; i--)
			{
				if (heights[i] > max)
					stack.Push(i);
				max = Math.Max(max, heights[i]);
			}

			int[] res = new int[stack.Count];
			int idx = 0;
			while (stack.Count > 0)
			{
				res[idx++] = stack.Pop();
			}
			return res;
		}
	}

	// #901. Online Stock Span
	// Time: O(1)
	// Space: O(n)
	public class StockSpanner
	{
		private int day;
		private Stack<Stock> monoStack;

		public StockSpanner()
		{
			this.day = 0;
			this.monoStack = new Stack<Stock>();

			// Set up day 0 stock
			var stock = new Stock(0, int.MaxValue);
			this.monoStack.Push(stock);
		}

		public int Next(int price)
		{

			// Keep decreasing order mono stack
			while (this.monoStack.Peek().price <= price)
			{
				this.monoStack.Pop();
			}

			int ret = ++this.day - this.monoStack.Peek().day;
			this.monoStack.Push(new Stock(this.day, price));
			return ret;
		}

		private class Stock
		{
			public int day
			{
				get; private set;
			}
			public int price
			{
				get; private set;
			}

			public Stock(int day, int price)
			{
				this.day = day;
				this.price = price;
			}
		}
	}
}
