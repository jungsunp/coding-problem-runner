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

		// #215. Kth Largest Element in an Array (counting sort)
		// Time: O(n + m), m = max - min + 1
		// Space: O(m)
		public int FindKthLargestOptimized(int[] nums, int k) {
			int min = int.MaxValue;
			int max = int.MinValue;

			foreach(int i in nums)
			{
				min = Math.Min(min, i);
				max = Math.Max(max, i);
			}

			int[] countArr = new int[ max - min + 1 ];
			foreach(int i in nums)
			{
				countArr[i - min]++;
			}

			int remain = k;
			for (int i = countArr.Length - 1; i >= 0; i--)
			{
				remain -= countArr[i];
				if (remain <= 0)
				{
					return i + min;
				}
			}

			throw new Exception("Unepxected has occured!");
		}

		// #2462. Total Cost to Hire K Workers
		// Time: O( (k + c) * log c)  - c: number of canidates
		// Space: O(c)
		public long TotalCost(int[] costs, int k, int candidates) {
			// Put candidates in min-heap
			Comparer<int> comparer = Comparer<int>.Create((x,y) => {
				if (costs[x] == costs[y]) return x - y;
				return costs[x] - costs[y];
			});
			PriorityQueue<int, int> heap = new PriorityQueue<int, int>(comparer);
			int left = candidates - 1;
			int right = Math.Max(candidates, costs.Length - candidates);
			for (int i = 0; i <= left; i ++) {
				heap.Enqueue(i, i);
			}
			for (int i = right; i < costs.Length; i++) {
				heap.Enqueue(i, i);
			}

			// Run sessions and calculate total cost
			long totalCost = 0;
			for (int i = 0; i < k; i++) {
				int idx = heap.Dequeue();
				totalCost += costs[idx];

				// Insert more candidates
				if (left + 1 < right) {
					if (idx <= left) {
						left++;
						heap.Enqueue(left, left);
					}
					else {
						right--;
						heap.Enqueue(right, right);
					}
				}
			}

			return totalCost;
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
