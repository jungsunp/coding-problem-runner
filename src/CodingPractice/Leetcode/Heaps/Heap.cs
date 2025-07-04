using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; // Add this using directive for the PriorityQueue class

namespace CodingPractice.Leetcode
{
	public class Heap
	{
		// #215. Kth Largest Element in an Array
		// Time: O(n log k)
		// Space: O(k)
		public int FindKthLargest(int[] nums, int k)
		{

			// Build Min Heap With Size k
			var heap = new PriorityQueue<int, int>();
			foreach (var i in nums)
			{
				heap.Enqueue(i, i);
				if (heap.Count > k)
				{
					heap.Dequeue();
				}
			}

			return heap.Peek();
		}

		// #2542. Maximum Subsequence Score
		// Time: O(n log n)
		// Space: O(n)
		public long MaxScore(int[] nums1, int[] nums2, int k)
		{
			// Sort both arrays based on nums2 value in desc order
			int[] indices = Enumerable.Range(0, nums1.Length).ToArray();
			Array.Sort(indices, (a, b) => nums2[b] - nums2[a]); // desc order
			nums1 = indices.Select(i => nums1[i]).ToArray();
			nums2 = indices.Select(i => nums2[i]).ToArray();

			// Calculate initial max using sorted array
			long sum = 0;
			PriorityQueue<int, int> minHeap = new PriorityQueue<int, int>();
			for (int i = 0; i < k; i++)
			{
				minHeap.Enqueue(nums1[i], nums1[i]);
				sum += nums1[i];
			}
			long max = sum * nums2[k - 1];

			// Iterate nums2 and maintain k largest value in min-heap
			for (int i = k; i < nums1.Length; i++)
			{
				int min = minHeap.Dequeue();
				minHeap.Enqueue(nums1[i], nums1[i]);
				sum += nums1[i] - min;
				max = Math.Max(max, sum * nums2[i]); // nums2[i] is min since nums2 is sorted desc
			}

			return max;
		}

		// #2462. Total Cost to Hire K Workers
		// Time: O( (k + c) * log c)  - c: number of canidates
		// Space: O(c)
		public long TotalCost(int[] costs, int k, int candidates)
		{
			// Put candidates in min-heap
			Comparer<int> comparer = Comparer<int>.Create((x, y) =>
			{
				if (costs[x] == costs[y])
					return x - y;
				return costs[x] - costs[y];
			});
			PriorityQueue<int, int> heap = new PriorityQueue<int, int>(comparer);
			int left = candidates - 1;
			int right = Math.Max(candidates, costs.Length - candidates);
			for (int i = 0; i <= left; i++)
			{
				heap.Enqueue(i, i);
			}
			for (int i = right; i < costs.Length; i++)
			{
				heap.Enqueue(i, i);
			}

			// Run sessions and calculate total cost
			long totalCost = 0;
			for (int i = 0; i < k; i++)
			{
				int idx = heap.Dequeue();
				totalCost += costs[idx];

				// Insert more candidates
				if (left + 1 < right)
				{
					if (idx <= left)
					{
						left++;
						heap.Enqueue(left, left);
					}
					else
					{
						right--;
						heap.Enqueue(right, right);
					}
				}
			}

			return totalCost;
		}

		// #767. Reorganize String
		// Time: O(n log k)
		// Space: O(k) = O(1) - k: unique characters <=26
		public string ReorganizeString(string s)
		{

			Dictionary<char, int> hash = new Dictionary<char, int>();
			int maxCnt = 1;

			foreach (char c in s)
			{
				if (!hash.ContainsKey(c))
				{
					hash.Add(c, 1);
				}
				else
				{
					hash[c]++;
					maxCnt = Math.Max(maxCnt, hash[c]);
				}
			}

			if (maxCnt == 1)
			{
				// all separate characters. no need to rearrange
				return s;
			}

			if (maxCnt > (s.Length + 1) / 2)
			{
				// most common character is more than half of string => return impossible
				return "";
			}

			// Create max heap to track the next best character to place
			PriorityQueue<char, int> heap = new PriorityQueue<char, int>();
			foreach (char c in hash.Keys)
			{
				heap.Enqueue(c, -hash[c]); // character with high count will have the highest priority
			}

			// Build output string using max heap
			StringBuilder builder = new StringBuilder();
			while (heap.Count > 1)
			{
				char c1 = heap.Dequeue(); // most common char
				char c2 = heap.Dequeue(); // second most char

				builder.Append(c1);
				builder.Append(c2);

				hash[c1]--;
				hash[c2]--;

				if (hash[c1] > 0)
				{
					heap.Enqueue(c1, -hash[c1]);
				}

				if (hash[c2] > 0)
				{
					heap.Enqueue(c2, -hash[c2]);
				}
			}

			if (heap.Count > 0)
			{ // if last 1 remaining
				char c = heap.Dequeue();
				builder.Append(c); // assuming last character left
			}

			return builder.ToString();
		}

		// #347. Top K Frequent Elements
		// Time: O(n log k)
		// Space: O(n)
		public int[] TopKFrequent(int[] nums, int k)
		{
			// count frequency of element in hash map
			Dictionary<int, int> hash = new Dictionary<int, int>();
			foreach (int num in nums)
			{
				if (!hash.ContainsKey(num))
				{
					hash[num] = 0;
				}
				hash[num]++;
			}

			// use priority queue to insert element based on its frequency
			PriorityQueue<int, int> heap = new PriorityQueue<int, int>();
			foreach (int num in hash.Keys)
			{
				heap.Enqueue(num, hash[num]);
				if (heap.Count > k)
				{
					heap.Dequeue(); // keep heap to size k
				}
			}

			int[] ret = new int[k];
			for (int i = k - 1; i >= 0; i--)
			{
				ret[i] = heap.Dequeue();
			}

			return ret;
		}
	}

	// #2336. Smallest Number in Infinite Set
	public class SmallestInfiniteSet
	{
		private PriorityQueue<int, int> heap; // min-heap that stores re-added numbers
		private HashSet<int> hash; // tracks re-added numbers;
		private int currentMin;

		// Time: O(1)
		// Space: O(1)
		public SmallestInfiniteSet()
		{
			this.heap = new PriorityQueue<int, int>();
			this.hash = new HashSet<int>();
			this.currentMin = 1;
		}

		// Time: O(log n)
		public int PopSmallest()
		{
			if (this.hash.Count == 0)
			{
				return this.currentMin++;
			}

			int min = this.heap.Dequeue();
			this.hash.Remove(min);
			return min;
		}

		// Time: O(log n)
		public void AddBack(int num)
		{
			if (this.currentMin <= num || hash.Contains(num))
			{
				return;
			}

			this.heap.Enqueue(num, num);
			this.hash.Add(num);
		}

		// #253. Meeting Rooms II
		// Time: O(n log n)
		// Space: O(n)
		public int MinMeetingRooms(int[][] intervals)
		{
			// Sort by start time (interval scheduling prob)
			Array.Sort(intervals, (a, b) => a[0].CompareTo(b[0]));

			PriorityQueue<int, int> heap = new(); // min-heap
			foreach (int[] interval in intervals)
			{
				if (heap.Count > 0 && interval[0] >= heap.Peek())
				{ // does not overlap
					heap.Dequeue();
				}

				heap.Enqueue(interval[1], interval[1]);

			}

			return heap.Count;
		}
	}
}
