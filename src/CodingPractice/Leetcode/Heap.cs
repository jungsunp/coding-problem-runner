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

	// #2336. Smallest Number in Infinite Set
	public class SmallestInfiniteSet {
		private PriorityQueue<int, int> heap; // min-heap that stores re-added numbers
		private HashSet<int> hash; // tracks re-added numbers;
		private int currentMin;

		// Time: O(1)
		// Space: O(1)
		public SmallestInfiniteSet() {
			this.heap = new PriorityQueue<int, int>();
			this.hash = new HashSet<int>();
			this.currentMin = 1;
		}
		
		// Time: O(log n)
		public int PopSmallest() {
		
		if (this.hash.Count == 0) {
			return this.currentMin++;
		}

			int min = this.heap.Dequeue();
			this.hash.Remove(min);
			return min;
		}

		// Time: O(log n)
		public void AddBack(int num) {
			if (this.currentMin <= num || hash.Contains(num)) {
				return;
			}

			this.heap.Enqueue(num, num);
			this.hash.Add(num);
		}
	}
}
