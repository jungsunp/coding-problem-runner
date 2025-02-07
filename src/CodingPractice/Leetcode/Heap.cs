using System;
using System.Collections.Generic;
using System.Linq; // Add this using directive for the PriorityQueue class

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

		// #2542. Maximum Subsequence Score
		// Time: O(n log n)
		// Space: O(n)
		public long MaxScore(int[] nums1, int[] nums2, int k) {
			// Sort both arrays based on nums2 value in desc order
			int[] indices = Enumerable.Range(0, nums1.Length).ToArray();
			Array.Sort(indices, (a, b) => nums2[b] - nums2[a]); // desc order
			nums1 = indices.Select(i => nums1[i]).ToArray();
			nums2 = indices.Select(i => nums2[i]).ToArray();

			// Calculate initial max using sorted array
			long sum = 0;
			PriorityQueue<int, int> minHeap = new PriorityQueue<int, int>();
			for (int i = 0; i < k; i++) {
				minHeap.Enqueue(nums1[i], nums1[i]);
				sum += nums1[i];
			}
			long max = sum * nums2[k - 1];

			// Iterate nums2 and maintain k largest value in min-heap
			for (int i = k; i < nums1.Length; i ++) {
				int min = minHeap.Dequeue();
				minHeap.Enqueue(nums1[i], nums1[i]);
				sum += nums1[i] - min;
				max = Math.Max(max, sum * nums2[i]); // nums2[i] is min since nums2 is sorted desc
			}

			return max;
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
