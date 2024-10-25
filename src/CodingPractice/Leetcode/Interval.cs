using System;

namespace CodingPractice.Leetcode
{
	public class Interval
	{
		// #435. Non-overlapping Intervals
		// Time: O(n log n)
		// Space: O(log n) - assume quick sort form c#
		public int EraseOverlapIntervals(int[][] intervals) {

			// greedy algorithm (interval scheduling prob)
			Array.Sort(intervals, (int[] a, int[] b) => a[1] - b[1]); // sort by end time

			int count = 0;
			int k = int.MinValue; // most recent end time
			foreach(int[] interval in intervals) {
				if (interval[0] >= k) { // does not overlap
					k = interval[1];
				}
				else { // overlaps
					count++;
				}
			}

			return count;
		}
	}
}
