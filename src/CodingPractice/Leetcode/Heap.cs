using System;
using System.Collections.Generic; // Add this using directive for the PriorityQueue class

namespace CodingPractice.Leetcode
{
	public class Heap
	{
		// #215. Kth Largest Element in an Array
		// Time: O(n log k)
		// Space: O(k)
		public int FindKthLargest(int[] nums, int k) {

			// Build Min Heap With Size k
			var heap = new PriorityQueue<int, int>();
			foreach(var i in nums)
			{
				heap.Enqueue(i, i);
				if (heap.Count > k)
				{
					heap.Dequeue();
				}
			}

			return heap.Peek();
		}
	}
}
